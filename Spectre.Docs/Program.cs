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
        }
    });

var app = builder.Build();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>();

// this adds the route for styles.css which is generated dynamically based on the used
// CSS classes.
app.UseMonorailCss();

await app.RunOrBuildContent(args);