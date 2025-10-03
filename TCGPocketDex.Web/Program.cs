using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TCGPocketDex.Web;

using TCGPocketDex.SDK.Http;
using TCGPocketDex.SDK.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register SDK services for Web client
builder.Services.AddScoped<IApiClient, ApiClient>();
builder.Services.AddScoped<ICardService, CardService>();

await builder.Build().RunAsync();