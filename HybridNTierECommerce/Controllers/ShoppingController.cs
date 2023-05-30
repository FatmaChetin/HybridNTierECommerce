using HybridNTierECommerce.Models.PageVMs;
using HybridNTierECommerce.Models.ShoppingTools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PagedList;
using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;

namespace HybridNTierECommerce.Controllers
{
    public class ShoppingController : Controller
    {
       IOrderManager _orderMan;
        IOrderDetailManager _odMan;
        ICategoryManager _catMan;
        IProductManager _prMan;
        public ShoppingController(IOrderManager orderMan, IOrderDetailManager odMan, ICategoryManager catMan, IProductManager prMan)
        {
            _orderMan = orderMan;
            _odMan = odMan;
            _catMan = catMan;
            _prMan = prMan;
        }
        public IActionResult ShoppingList(int? page, int? categoryID)
        {

            IPagedList<ProductVM> products = categoryID == null ? _prMan.Where(x => x.DataStatus != Project.ENTITIES.Enums.DataStatus.Deleted).Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                CategoryName = x.Category != null ? x.Category.CategoryName : "",
                DataStatus = x.DataStatus.ToString(),

            }).ToPagedList(page ?? 1, 9) : _prMan.Where(x => x.CategoryID == categoryID && x.DataStatus != Project.ENTITIES.Enums.DataStatus.Deleted ).Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                CategoryName = x.Category != null ? x.Category.CategoryName : "",
                CategoryID = x.CategoryID,
                DataStatus = x.DataStatus.ToString(),


            }).ToPagedList(page ?? 1, 9);
            PaginationVM pavm = new PaginationVM
            {
                PagedProducts = products,

                Categories = _catMan.Select(x => new CategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                }).ToList()
            };
            if (categoryID != null)
            {
                TempData["catID"] = categoryID;
            }

            return View(pavm);
        }
        public IActionResult AddToCart(int id)
        {
            List<CartItem> Sepetim = new Dictionary<int, CartItem>().Values.ToList();
            string value = HttpContext.Session.GetString("scart");
            var list=JsonConvert.DeserializeObject<List<CartItem>>(value);
            Cart c = HttpContext.Session.GetString("scart") == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(value);
          //  Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
          //  ProductVM eklenecekUrun = _prMan.Find(id);
          /*
            CartItem ci = new CartItem
            {
                ID = eklenecekUrun.ID,
                Name = eklenecekUrun.ProductName,
                Price = eklenecekUrun.UnitPrice,
                
            };
            c.SepetEkle(ci);*/
            
            return RedirectToAction("ShoppingList");

        }
        //Todo session işlemlerini tam olarak öğrendiğinde bu sayfadan devam edersin !!
        

    }
}
