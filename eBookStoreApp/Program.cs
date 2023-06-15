using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    NullValueHandling = NullValueHandling.Ignore
};
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.LoginPath = "/User/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(50));
    });
builder.Services.AddAutoMapper(Assembly.Load("Mapper"));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();