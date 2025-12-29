using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Spectre.Docs.Playground;
using Spectre.Docs.Playground.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => httpClient);

// Register the shared workspace service as singleton to share references across services
builder.Services.AddSingleton(sp => new WorkspaceService(httpClient));
builder.Services.AddScoped<CompilationService>();
builder.Services.AddScoped<CompletionService>();
builder.Services.AddScoped<ExecutionService>();

await builder.Build().RunAsync();
