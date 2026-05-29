using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PW_Museo.Client.Services.Visits;
using PW_Museo.Client.Services.Tickets;
using PW_Museo.Client.Services.Shows;
using PW_Museo.Client.Services.Operas;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IOperaService, OperaService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

await builder.Build().RunAsync();
