using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.ENTITIES.Models;
using Project.MAP.Configuraions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext:IdentityDbContext<AppUser,IdentityRole<int>,int>
    { 
        //session oluşturmak istediğim için context ctorun içerisine yazmalıyım.
        // program.cs içerisine session oluşturdum. configure kısmında app.USeSession tanıttım sonraki aşama ise burası 
       
        public MyContext(DbContextOptions<MyContext>options):base (options)
        {
         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppUserProfileConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserProfile> AppUserProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order>  Orders{ get; set; }
        public DbSet<Category> Categories { get; set; }

       
    }
}
