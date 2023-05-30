using Microsoft.Extensions.DependencyInjection;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.DAL.Repositories.Abstarcts;
using Project.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    public static class RepManService
    {
        public static IServiceCollection AddRepManServices(this IServiceCollection services)
        {
            // biz burada scoped yöntemini tercih edeceğiz. yani bir parametre kümesine birden fazla veri girse bile instance'ın bir kere çalışıp request'in işi bittikten sonra garbage collector tarafından kaldırılır. Singleton pattern değildir.
           
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            // burada repositorylere addscope yapıyoruz
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<IAppUserProfileRepository,AppUserProfileRepository>();
            services.AddScoped<IAppUserRepository,AppUserRepository>();
           
            
            services.AddScoped(typeof(IManager<>),typeof(BaseManager<>));
            //Burada ise managerlara addscope yapıyoruz. 
            services.AddScoped<ICategoryManager,CategoryManager>();
            services.AddScoped<IProductManager,ProductManager>();
            services.AddScoped<IOrderDetailManager,OrderDetailManager>();
            services.AddScoped<IOrderManager,OrderManager>();
            services.AddScoped<IAppUserManager,AppUserManager>();
            services.AddScoped<IAppUserProfileManager,AppUserProfileManager>();
            return services;
        
        }
    }
}
