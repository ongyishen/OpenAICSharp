using OpenAIDemo.Core;
using OpenAIDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
GlobalContext.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
GlobalContext.SystemConfig.ContentRootPath = builder.Environment.ContentRootPath;


//Disable CamelCasing in JSON Response
builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});


var app = builder.Build();

if (!string.IsNullOrEmpty(GlobalContext.SystemConfig.VirtualDirectory))
{
    app.UsePathBase(new PathString(GlobalContext.SystemConfig.VirtualDirectory));
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
