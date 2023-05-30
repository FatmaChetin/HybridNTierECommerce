using Project.DAL.Context;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(MyContext db):base(db)
        {

        }
        public void SpecialCategoryCreation(Category category)
        {
            List<Product> products = new List<Product>
        {
            new Product
            {
                ProductName="tadelle",
                
            },
            new Product
            {
                ProductName="Jelibon",
                
            }
        };
            category.Products = products;
            Add(category);
        }

    }
}
