using HybridNTierECommerce.Areas.Admin.Data.AdminPageVMs;
using HybridNTierECommerce.Areas.Admin.Data.AdminPureVms;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;

namespace HybridNTierECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ICategoryManager _catMan;
        public CategoryController(ICategoryManager catMan)
        {
            _catMan = catMan;
        }

        public IActionResult ListCategories(int? id)
        {
            List<AdminCategoryPureVM> categories;
            if (id == null)
            {
                categories = _catMan.Select(x => new AdminCategoryPureVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    DeletedDate = x.DeletedDate,
                    UpdatedDate = x.UpdatedDate,
                    CreatedDate = x.CreatedDate,
                    DataStatus = x.DataStatus.ToString(),

                }).ToList();

            }
            else
            {
                categories = _catMan.Select(x => new AdminCategoryPureVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    DeletedDate = x.DeletedDate,
                    UpdatedDate = x.UpdatedDate,
                    CreatedDate = x.CreatedDate,
                    DataStatus = x.DataStatus.ToString(),

                }).ToList();
            }
            AdminListCategoryPageVM alcpm = new AdminListCategoryPageVM
            {
                Categories = categories
            };
            return View(alcpm);
        }
        // altaki Add metoduyla kategori eklemek için get yapıyoruz. Hemen altındaki [HttpPost] işlemi sayesinde ise girdiğimiz kategori bilgilerini post yaparak yeni bir kategoriyi veri tabanımıza eklemiş oluyoruz.

        //Eğer bir işlemde bir güncelleme ekleme ve ya silme yaparsan returnden sonra redirecttoaction demelisin ki ayptığın işlemleri gözlemleyebil.
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(AdminCategoryPureVM categories)
        {
            Category c = new Category
            {
                CategoryName = categories.CategoryName,
                Description = categories.Description,
                ID = categories.ID,
                CreatedDate = DateTime.Now,

            };
            _catMan.Add(c);
            return RedirectToAction("ListCategories");

        }
        public IActionResult UpdateCategory(int id)
        {
            Category selected = _catMan.Find(id);
            AdminAddUpdateCategoryPageVM acpm = new AdminAddUpdateCategoryPageVM
            {
                Categories= new AdminCategoryPureVM
                {
                    ID = selected.ID,
                    CategoryName = selected.CategoryName,
                    Description = selected.Description,
                    UpdatedDate = DateTime.Now,
                }
            };
            return View(acpm);

        }
        [HttpPost]
        public IActionResult UpdateCategory(AdminCategoryPureVM categories)
        {
            Category toBeUpdated = _catMan.Find(categories.ID);
            toBeUpdated.CategoryName = categories.CategoryName;
            toBeUpdated.Description = categories.Description;
           
            _catMan.Update(toBeUpdated);
            return RedirectToAction("ListCategories");
        }
        public IActionResult DeleteCategory(int id)
        {
            _catMan.Delete(_catMan.Find(id));
            return RedirectToAction("ListCategories");
        }



    }
}
