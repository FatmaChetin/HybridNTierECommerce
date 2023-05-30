using HybridNTierECommerce.Areas.Admin.Data.AdminPageVMs;
using HybridNTierECommerce.Areas.Admin.Data.AdminPureVms;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;

namespace Project.COREMVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ICategoryManager _catMan;
        IProductManager _proMan;
        public ProductController(ICategoryManager catMan, IProductManager proMan)
        {
            _catMan = catMan;
            _proMan = proMan;
        }
        private List<AdminProductPureVM> GetProduct()
        {
            return _proMan.Select(x => new AdminProductPureVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                CategoryID = x.CategoryID,
                CategoryName = x.Category.CategoryName,
                DeletedDate = x.DeletedDate,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                DataStatus=x.DataStatus.ToString(),
            }).ToList();
        }
        private List<AdminCategoryPureVM> GetCategory()
        {
            return _catMan.Select(x => new AdminCategoryPureVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description,
            }).ToList();
        }
        public IActionResult ListProducts(int? id)
        {
            List<AdminProductPureVM> pro = GetProduct();
            AdminListProductPageVM products = new AdminListProductPageVM
            {
                Products = pro,
            };
            _proMan.GetAll();
            return View(products);

        }
        public IActionResult AddProduct()
        {
            List<AdminCategoryPureVM> categories = GetCategory();
            AdminAddUpdateProductPageVM add = new AdminAddUpdateProductPageVM
            {
                Categories = categories
            };
            return View(add);
        }
        [HttpPost]
        public IActionResult AddProduct(AdminProductPureVM product)
        {
            Product pro = new Product
            {
                ProductName = product.ProductName,
                ID = product.ID,
                UnitPrice = product.UnitPrice,
                CategoryID = product.CategoryID,
                CreatedDate=DateTime.Now,
            };
            _proMan.Add(pro);
            return RedirectToAction("ListProducts");
        }
        public IActionResult UpdateProduct(int id) 
        {
            List<AdminCategoryPureVM> categories= GetCategory();
            AdminAddUpdateProductPageVM updatePro = new AdminAddUpdateProductPageVM
            {
                Categories = categories,
                Product = _proMan.Where(x => x.ID == id).Select(x => new AdminProductPureVM 
                {
                    ID=x.ID,
                    ProductName=x.ProductName,
                    UnitPrice=x.UnitPrice,
                    CategoryID=x.CategoryID,
                    UpdatedDate=DateTime.Now
                }).FirstOrDefault()
            };// seçtiğimiz id değerinden ilkini veya default değerini getir diyoruz ondan dolayı burada onu kullandık.
            return View(updatePro);
        }
        [HttpPost]
        public IActionResult UpdateProduct(AdminProductPureVM product)
        { 
            Product pro=_proMan.Find(product.ID);
            pro.ProductName = product.ProductName;
            pro.UnitPrice = product.UnitPrice;
            pro.CategoryID = product.CategoryID;
            _proMan.Update(pro);
            return RedirectToAction("ListProducts");
        }
        public IActionResult DeleteProduct(int id)
        {
            _proMan.Delete(_proMan.Find(id));
            return View("ListProducts");
            //Manager service de delete yani pasife almadan silme özelliği oluşturduk. Bundan dolayı direk destroy metodunu kullanamayız. 
        }
        

        

    }
}
