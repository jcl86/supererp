using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;

    //options.Events.OnSigningOut = async e =>
    //{
    //    await e.HttpContext.RevokeUserRefreshTokenAsync();
    //};
});

app.MapGet("/", () => "Hello World!");

app.Run();
