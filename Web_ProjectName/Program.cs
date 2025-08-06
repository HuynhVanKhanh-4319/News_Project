using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Net.Http.Headers;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Models.Common;
using Web_ProjectName.Services;

var builder = WebApplication.CreateBuilder(args);

void GetDefaultHttpClient(IServiceProvider serviceProvider, HttpClient httpClient, string hostUri)
{
    if (!string.IsNullOrEmpty(hostUri))
        httpClient.BaseAddress = new Uri(hostUri);
    //client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
    httpClient.Timeout = TimeSpan.FromMinutes(1);
    httpClient.DefaultRequestHeaders.Clear();
    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}

HttpClientHandler GetDefaultHttpClientHandler()
{
    return new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        UseCookies = false,
        AllowAutoRedirect = false,
        UseDefaultCredentials = true,
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true,
    };
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie = new CookieBuilder
    {
        //Domain = "cms.labadalat.com", //Releases in active
        Name = "AuthCMS",
        HttpOnly = true,
        Path = "/",
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always
    };
    options.LoginPath = new PathString("/Account/SignIn");
    options.LogoutPath = new PathString("/Account/SignOut");
    options.AccessDeniedPath = new PathString("/Error/403");
    options.SlidingExpiration = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSession(options =>
{
    //options.Cookie.Domain = ".koolselling.com"; //Releases in active
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly); //AutoMapperProfile
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient("base")
    .ConfigureHttpClient((serviceProvider, httpClient) => GetDefaultHttpClient(serviceProvider, httpClient, builder.Configuration.GetSection("ApiSettings:UrlApi").Value))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Default is 2 min
    .ConfigurePrimaryHttpMessageHandler(x => GetDefaultHttpClientHandler());

builder.Services.AddHttpClient("custom")
    .ConfigureHttpClient((serviceProvider, httpClient) => GetDefaultHttpClient(serviceProvider, httpClient, string.Empty))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Default is 2 min
    .ConfigurePrimaryHttpMessageHandler(x => GetDefaultHttpClientHandler());

builder.Services.AddSingleton<IBase_CallApi, Base_CallApi>();
builder.Services.AddSingleton<ICallBaseApi, CallBaseApi>();
builder.Services.AddSingleton<ICallApi, CallApi>();
builder.Services.AddSingleton<ICallExternalApi, CallExternalApi>();

builder.Services.AddSingleton<IS_Image, S_Image>();

builder.Services.AddSingleton<IS_Address, S_Address>();
builder.Services.AddSingleton<IS_Contact, S_Contact>();
builder.Services.AddSingleton<IS_Supplier, S_Supplier>();
builder.Services.AddSingleton<IS_Product, S_Product>();
builder.Services.AddSingleton<IS_ProductCategory, S_ProductCategory>();
builder.Services.AddSingleton<IS_News, S_News>();
builder.Services.AddSingleton<IS_NewsCategory, S_NewsCategory>();
builder.Services.AddSingleton<IS_PartnerList, S_PartnerList>();
builder.Services.AddSingleton<IS_Banner, S_Banner>();
builder.Services.AddSingleton<IS_GoogleReCAPTCHA, S_GoogleReCAPTCHA>();


builder.Services.Configure<Config_ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<Config_MetaSEO>(builder.Configuration.GetSection("MetaSEO"));
builder.Services.Configure<ReCAPTCHASettings>(builder.Configuration.GetSection("GooglereCAPTCHA"));
builder.Services.Configure<Config_Supplier>(builder.Configuration.GetSection("Supplier"));

// Add services to the container.
builder.Services.AddControllersWithViews();
object value = builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 7 * 60 * 60 * 24; //7 days
        ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});

app.UseCookiePolicy(); ;

app.UseSession();

app.UseRouting();

/*app.UseAuthorization();*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Introduce IsHot",
        pattern: "gioi-thieu",
        defaults: new { controller = "Introduce", action = "Index" });

    endpoints.MapControllerRoute(
       name: "Product List",
       pattern: "san-pham",
       defaults: new { controller = "Product", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Product Detail",
        pattern: "san-pham/{metaUrl}",
        defaults: new { controller = "Product", action = "ViewDetail" });

    endpoints.MapControllerRoute(
       name: "Error page",
       pattern: "error/{code}",
       defaults: new { controller = "Error", action = "Index" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
