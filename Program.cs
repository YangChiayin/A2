using Chiayin_Yang_Assignment2.Entities;
using Chiayin_Yang_Assignment2.Middleware;
using Chiayin_Yang_Assignment2.Services;
using Microsoft.EntityFrameworkCore;

namespace Chiayin_Yang_Assignment2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession(); 
            builder.Services.AddControllersWithViews();

            string connStr = builder.Configuration.GetConnectionString("EnrollmentDB");
            builder.Services.AddDbContext<EnrollmentContext>(options => options.UseSqlServer(connStr));

            // register the service
            builder.Services.AddScoped<IEmailService, EmailService>();

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

            app.UseSession(); 

            app.UseRouting();

            app.UseAuthorization();

            app.UseFirstVisitMiddleware(); //middleware

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
