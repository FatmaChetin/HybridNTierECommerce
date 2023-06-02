using Project.BLL.ServiceInjections;

namespace HybridNTierECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddDbContextService();
            builder.Services.AddIdentityServices();
            builder.Services.AddRepManServices();
           
            //--------------------------------
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                /*  endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}",
                   defaults: new { controller = "Home", action = "Index" 
                   });
                */
                endpoints.MapAreaControllerRoute(name:"Area",
                    areaName:"Admin",
                    pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}