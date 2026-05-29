using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PW_Museo.Components;
using PW_Museo.Components.Account;
using PW_Museo.Data;
using PW_Museo.Endpoints;
using PW_Museo.Services;
using PW_Museo.Client.Services.Visits;
using PW_Museo.Client.Services.Tickets;
using PW_Museo.Client.Services.Shows;
using PW_Museo.Client.Services.Operas;
using PW_Museo.Client.Services.Artists;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<IGuidedVisitsDA, GuidedVisitsDA>();
builder.Services.AddScoped<IOperasDA, OperasDA>();
builder.Services.AddScoped<ITicketsDA, TicketsDA>();
builder.Services.AddScoped<IShowsDA, ShowsDA>();
builder.Services.AddScoped<IVisitsDA, VisitsDA>();
builder.Services.AddScoped<IArtistsDA, ArtistsDA>();

// Client services per il prerendering Blazor WebAssembly Server-Side
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IOperaService, OperaService>();
builder.Services.AddScoped<IArtistService, ArtistService>();

builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

// Optional: add Swagger, but we'll stick to minimum requirements for mapping endpoints.
var app = builder.Build();

app.MapOperasEndpoints();
app.MapShowsEndpoints();
app.MapGuidedVisitsEndpoints();
app.MapTicketsEndpoints();
app.MapVisitsEndpoints();
app.MapArtistsEndpoints();

app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PW_Museo.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
