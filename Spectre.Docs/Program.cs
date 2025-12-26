using System.Collections.Immutable;
using Mdazor;
using MonorailCss.Parser.Custom;
using MonorailCss.Theme;
using Spectre.Console;
using MyLittleContentEngine;
using MyLittleContentEngine.MonorailCss;
using MyLittleContentEngine.Services.Content.CodeAnalysis.Configuration;
using MyLittleContentEngine.UI.Components;
using Spectre.Docs.Components;
using Spectre.Docs.Components.Layouts;
using Spectre.Docs.Components.Reference;
using Spectre.Docs.Components.Shared;
using Spectre.Docs.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();

// Register XML documentation service for API reference
builder.Services.AddSingleton<XmlDocumentationService>();

// configures site wide settings
builder.Services.AddContentEngineService(_ => new ContentEngineOptions
    {
        SiteTitle = "Spectre.Console Documentation",
        SiteDescription = "Beautiful console applications with Spectre.Console",
        ContentRootPath = "Content",
    })
    // Console documentation service
    .WithMarkdownContentService(_ => new MarkdownContentOptions<SpectreConsoleFrontMatter>
    {
        ContentPath = "Content/console",
        BasePageUrl = "/console",
        TableOfContentsSectionKey = "console",

    })
    // CLI documentation service
    .WithMarkdownContentService(_ => new MarkdownContentOptions<SpectreConsoleCliFrontMatter>
    {
        ContentPath = "Content/cli",
        BasePageUrl = "/cli",
        TableOfContentsSectionKey = "cli",
    })
    // Blog service
    .WithMarkdownContentService(_ => new MarkdownContentOptions<BlogFrontMatter>
    {
        ContentPath = "Content/blog",
        BasePageUrl = "/blog",
        ExcludeSubfolders = false,
        PostFilePattern = "*.md;*.mdx"
    })
    .WithConnectedRoslynSolution(_ => new CodeAnalysisOptions
    {
        SolutionPath = "../Spectre.Docs.slnx",
    })
    // this allows us to use blazor components within Markdown.
    // see https://phil-scott-78.github.io/MyLittleContentEngine/guides/markdown-extensions#blazor-within-markdown
    .AddMdazor()
    .AddMdazorComponent<Step>()
    .AddMdazorComponent<Steps>()
    .AddMdazorComponent<Screenshot>()
    .AddMdazorComponent<BoxBorderList>()
    .AddMdazorComponent<ColorList>()
    .AddMdazorComponent<EmojiList>()
    .AddMdazorComponent<SpinnerList>()
    .AddMdazorComponent<TableBorderList>()
    .AddMdazorComponent<TreeGuideList>()
    .AddMdazorComponent<WidgetApiReference>()
    .AddMdazorComponent<TwoColumn>()
    .AddMdazorComponent<Column>()
    .AddMonorailCss(_ => new MonorailCssOptions
    {
        ColorScheme = new AlgorithmicColorScheme()
        {
            PrimaryHue = 200,
            ColorSchemeGenerator = i => (i + 260, i + 15, i -15),
            BaseColorName = ColorNames.Neutral,
        },
        CustomCssFrameworkSettings = settings =>
        {
            return settings = settings with { CustomUtilities =  [
                new UtilityDefinition()
                {
                    Pattern = "scrollbar-thin",
                    Declarations = ImmutableList.Create(
                        new CssDeclaration("scrollbar-width", "thin")
                    )
                },
                new UtilityDefinition
                {
                    Pattern = "scrollbar-thumb-*",
                    IsWildcard = true,
                    Declarations = ImmutableList.Create(
                        new CssDeclaration("--tw-scrollbar-thumb-color", "--value(--color-*)")
                    )
                },
                new UtilityDefinition
                {
                    Pattern = "scrollbar-track-*",
                    IsWildcard = true,
                    Declarations = ImmutableList.Create(
                        new CssDeclaration("--tw-scrollbar-track-color", "--value(--color-*)")
                    )
                },
                new UtilityDefinition
                {
                    Pattern = "scrollbar-color",
                    Declarations = ImmutableList.Create(
                        new CssDeclaration("scrollbar-color", "var(--tw-scrollbar-thumb-color) var(--tw-scrollbar-track-color)")
                    )
                }
            ]};
        },
        // .net 10.0.101 has a bug flash grey on all content change and not removing it.
        // this hides empty error messages on hot reload
        ExtraStyles = """
                      #dotnet-compile-error:empty {
                          display: none;
                      }
                      """
    });

var app = builder.Build();
app.UseAntiforgery();

// Custom middleware to serve playground static files from source - must be before other static file middleware
var playgroundWwwroot = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "Spectre.Docs.Playground", "wwwroot"));
var playgroundBinWwwroot = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "Spectre.Docs.Playground", "bin", "Release", "net10.0", "wwwroot"));
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/playground", out var remaining))
    {
        // Add Cross-Origin headers required for SharedArrayBuffer (WASM threading)
        context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";
        context.Response.Headers["Cross-Origin-Embedder-Policy"] = "require-corp";

        string? filePath = null;

        // Handle /playground/index.html explicitly
        if (remaining.Value == "/index.html")
        {
            filePath = Path.Combine(playgroundWwwroot, "index.html");
        }
        // Handle _framework files from the build output
        else if (remaining.Value?.StartsWith("/_framework") == true)
        {
            var frameworkPath = remaining.Value!.TrimStart('/');
            filePath = Path.Combine(playgroundBinWwwroot, frameworkPath);

            // If file doesn't exist with exact name, try to find fingerprinted version
            if (!File.Exists(filePath))
            {
                var fileName = Path.GetFileName(frameworkPath);
                var frameworkDir = Path.Combine(playgroundBinWwwroot, "_framework");
                if (Directory.Exists(frameworkDir))
                {
                    var baseName = Path.GetFileNameWithoutExtension(fileName);
                    var ext = Path.GetExtension(fileName);
                    if (string.IsNullOrEmpty(ext)) ext = ".dll";

                    // Find files matching: {baseName}.{hash}{ext} where hash has no dots
                    var matchingFiles = Directory.GetFiles(frameworkDir)
                        .Where(f => {
                            var fn = Path.GetFileName(f);
                            if (!fn.StartsWith(baseName + ".") || !fn.EndsWith(ext) || fn.EndsWith(".gz"))
                                return false;
                            var middle = fn.Substring(baseName.Length + 1, fn.Length - baseName.Length - 1 - ext.Length);
                            return !middle.Contains('.') && middle.All(c => char.IsLetterOrDigit(c));
                        })
                        .ToArray();

                    if (matchingFiles.Length == 1)
                    {
                        filePath = matchingFiles[0];
                    }
                    else if (matchingFiles.Length == 0 && ext == ".wasm")
                    {
                        // Try .dll extension
                        matchingFiles = Directory.GetFiles(frameworkDir)
                            .Where(f => {
                                var fn = Path.GetFileName(f);
                                if (!fn.StartsWith(baseName + ".") || !fn.EndsWith(".dll") || fn.EndsWith(".gz"))
                                    return false;
                                var middle = fn.Substring(baseName.Length + 1, fn.Length - baseName.Length - 5);
                                return !middle.Contains('.') && middle.All(c => char.IsLetterOrDigit(c));
                            })
                            .ToArray();
                        if (matchingFiles.Length == 1)
                        {
                            filePath = matchingFiles[0];
                        }
                    }
                }
            }
        }
        // Serve BlazorMonaco content from NuGet package
        else if (remaining.Value?.StartsWith("/_content/BlazorMonaco") == true)
        {
            var blazorMonacoPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".nuget", "packages", "blazormonaco", "3.4.0", "staticwebassets",
                remaining.Value.Replace("/_content/BlazorMonaco/", ""));
            if (File.Exists(blazorMonacoPath))
            {
                var ext = Path.GetExtension(blazorMonacoPath).ToLowerInvariant();
                context.Response.ContentType = ext switch
                {
                    ".js" => "application/javascript",
                    ".css" => "text/css",
                    ".json" => "application/json",
                    ".ttf" => "font/ttf",
                    ".woff" => "font/woff",
                    ".woff2" => "font/woff2",
                    _ => "application/octet-stream"
                };
                var bytes = await File.ReadAllBytesAsync(blazorMonacoPath);
                context.Response.ContentLength = bytes.Length;
                await context.Response.Body.WriteAsync(bytes);
                return;
            }
        }
        // Serve empty module for other _content requests (hot reload modules not needed)
        else if (remaining.Value?.StartsWith("/_content") == true && remaining.Value.EndsWith(".js"))
        {
            context.Response.ContentType = "application/javascript";
            await context.Response.WriteAsync("export default {};");
            return;
        }
        // Handle other static files like js/terminal.js
        else if (!string.IsNullOrEmpty(remaining.Value) && remaining.Value != "/")
        {
            filePath = Path.Combine(playgroundWwwroot, remaining.Value!.TrimStart('/'));
        }

        if (filePath != null && File.Exists(filePath))
        {
            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            context.Response.ContentType = ext switch
            {
                ".js" => "application/javascript",
                ".mjs" => "application/javascript",
                ".css" => "text/css",
                ".html" => "text/html; charset=utf-8",
                ".json" => "application/json",
                ".wasm" => "application/wasm",
                ".dll" => "application/octet-stream",
                ".pdb" => "application/octet-stream",
                ".dat" => "application/octet-stream",
                ".blat" => "application/octet-stream",
                _ => "application/octet-stream"
            };

            // Read file bytes directly to avoid Content-Length mismatch issues
            var bytes = await File.ReadAllBytesAsync(filePath);
            context.Response.ContentLength = bytes.Length;
            await context.Response.Body.WriteAsync(bytes);
            return;
        }
    }
    await next();
});

// Serve static files (this handles the playground's wwwroot files)
app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorComponents<App>();

// this adds the route for styles.css which is generated dynamically based on the used
// CSS classes.
app.UseMonorailCss();

await app.RunOrBuildContent(args);