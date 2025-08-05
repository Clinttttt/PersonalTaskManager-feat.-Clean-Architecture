using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PTMPersonalTaskManager.Client.Components;
using PTMPersonalTaskManager.Client.Services;
using PTMPersonalTaskManager.Infrastructure;
using PTMPersonalTaskManager.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<PageState>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ProtectedLocalStorage>();

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7003");
});
builder.Services.AddHttpClient<AuthApiServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7003");
});


builder.Services.AddHttpClient<TaskApiServices>(client =>
{
client.BaseAddress = new Uri("https://localhost:7003");
});

builder.Services.AddSingleton<TaskStateService>();



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
