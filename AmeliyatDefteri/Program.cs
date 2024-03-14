using System.Globalization;
using AmeliyatDefteri.Entity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");
    options.UseSqlite(connectionString);
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("tr-TR"); // Türkçe için tr-TR kullanabilirsiniz
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("tr-TR") };
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("tr-TR") };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

SeedData.TestVerileriniDoldur(app);

/*app.MapControllerRoute(
    name: "datetime",
    pattern: "Ameliyat/Gecmis/{time:datetime}",
    defaults: new { controller = "Ameliyat", action = "Gecmis" }
);*/
app.MapControllerRoute(
    name: "datetime",
    pattern: "history/butun/sabit/{anestezi}",
    defaults: new { controller = "Ameliyat", action = "Gecmis" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
