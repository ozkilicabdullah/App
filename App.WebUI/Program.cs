using App.WebUI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/Auth/Login";
                 options.AccessDeniedPath = "/Auth/AccessDenied";
             });
builder.Services.AddHttpClient<UserApiService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});
builder.Services.AddHttpClient<AuthApiService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
