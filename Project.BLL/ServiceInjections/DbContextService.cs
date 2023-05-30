using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    public static class DbContextService
    {
        //DbContextPool startupta dalden bir sınıfı kullanmaktan ziyade sadece bll'deki bu ifadelerle bir service entegrasyonu yapar.
        //middleware'da yapılan bu entergrasyonda neler mi var; Lazy loading kullanımı, singleton pattern,dependecies injection sunar(tidly couplelar losely couple hale gelir.)
        public static IServiceCollection AddDbContextService(this IServiceCollection services) 
        {
            ServiceProvider provider = services.BuildServiceProvider();

            IConfiguration? configuration = provider.GetService<IConfiguration?>();// IConfigration kullanılırken catle kğtğphanesini kullanmak yerine extension.configuration kullanılmaksı gerekmektedir.
          
            services.AddDbContextPool<MyContext>(options=> options.UseSqlServer(configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());
            
            return services;
        }
    }
}
