using EasyArchitectCore.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebPagesLoginLab6;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
#pragma warning disable ASP5001 // 類型或成員已經過時
#pragma warning disable CS0618 // 類型或成員已經過時
builder.Services.AddMvc()
    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
    .AddRazorOptions(options =>
    {
        options.AreaPageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
    });
#pragma warning restore CS0618 // 類型或成員已經過時
#pragma warning restore ASP5001 // 類型或成員已經過時
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(configure =>
{
    configure.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configure.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = builder.Configuration.GetValue<string>("AppSettings:LoginPage");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("AppSettings:TimeoutMinutes"));
    options.Cookie.HttpOnly = true;
    options.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToReturnUrl = async (context) =>
        {
            context.HttpContext.Response.Cookies.Delete("_LOGIN_USER_INFO");
        }
    };
});

//IConfigurationSection configurationRoot = builder.Configuration.GetSection("AppSettings");
//builder.Services.Configure<AppSetting>()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCustomConfigurationManager();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
