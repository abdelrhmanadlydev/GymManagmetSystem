using GymManagmetDAL.Data.Context;
using GymManagmetDAL.Repositories.Classes;
using GymManagmetDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmetPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure DbContext with SQL Server by GymDBContext
            builder.Services.AddDbContext<GymDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Configure Generic Repository for Dependency Injection
            builder.Services.AddScoped(typeof(IGenericRepository<>) ,typeof(GenericRepository<>));

            // Configure Plane Repository for Dependency Injection
            builder.Services.AddScoped<IPlaneRepository, PlaneRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
