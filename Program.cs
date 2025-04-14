using System.Globalization;
using whats_csharp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using whats_csharp.Services;
using whats_csharp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:7264");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7264;
});

builder.Services.AddScoped<IEmailService, EmailService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login/Login"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; 
    });

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
