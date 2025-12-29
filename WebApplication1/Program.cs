using MongoDB.Driver;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Получаем настройки MongoDB
       //  Зарегистрировать mongoSettings в DI
       var mongoSettings = builder.Configuration.GetSection("BookStoreDatabase")
            .Get<BookStoreDatabaseSettings>();
       
       builder.Services.AddSingleton<BookStoreDatabaseSettings>(mongoSettings!);
        
        // Регистрируем настройки (если нужно для других сервисов)
        builder.Services.Configure<BookStoreDatabaseSettings>(
            builder.Configuration.GetSection("BookStoreDatabase"));
        
        // Регистрируем сервисы
        builder.Services.AddSingleton<BookService>();
        builder.Services.AddSingleton<UserService>();
        
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddControllers() .AddJsonOptions(
            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=User}/{action=Index}/{id?}");

        app.Run();
    }
}