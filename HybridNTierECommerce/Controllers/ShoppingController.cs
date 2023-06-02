using HybridNTierECommerce.Models.PageVMs;
using HybridNTierECommerce.Models.ShoppingTools;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PagedList;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System.Text;

namespace HybridNTierECommerce.Controllers
{
 
    public class ShoppingController : Controller
    {
        IOrderManager _orderMan;
        IOrderDetailManager _odMan;
        ICategoryManager _catMan;
        IProductManager _prMan;
        IAppUserManager _aMan;
        public ShoppingController(IOrderManager orderMan, IOrderDetailManager odMan, ICategoryManager catMan, IProductManager prMan, IHttpContextAccessor session, IAppUserManager aMan)
        {
            _orderMan = orderMan;
            _odMan = odMan;
            _catMan = catMan;
            _prMan = prMan;
            _session = session;
            _aMan = aMan;
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
        private readonly IHttpContextAccessor _session;

        public IActionResult AddToCart(int id)
        {
            
            ISession session = _session.HttpContext.Session;
            Cart c=new Cart();
            session.SetObject("scart",c);
            Product eklenecekUrun = _prMan.Find(id);
            CartItem ek = new CartItem 
            {
                ID= eklenecekUrun.ID,
                Name = eklenecekUrun.ProductName,
                Price=eklenecekUrun.UnitPrice

            };
            foreach (CartItem item in c.Sepetim)
            {
                if (eklenecekUrun.UnitInStock -item.Amount<=0)
                {
                    TempData["outOfStockMessage"] = "Üzgünüz, Bu ürün üçün stok yeterlı değildir";
                    return Redirect(HttpContext.Request.Headers["Referer"].ToString());
                }

            }
            c.SepetEkle(ek);
           Cart cart= session.GetObject<Cart>("scart");
                

            
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
            
        }
       
        public IActionResult CartPage()
        {
            Cart c = HttpContext.Session.GetObject<Cart>("scart");
             
            if (c!= null)
            {
               CartPageVM cpm= new CartPageVM();
                {
                    c = c;
                };
                return View(cpm);

            }
            TempData["bos"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("ShoppingList");
        }

        public IActionResult DeleteFromCart(int id)
        {
            Cart c = HttpContext.Session.GetObject<Cart>("scart");
            if (c!= null) 
            {
                c.SepettenCikar(id);
                if (c.Sepetim.Count==0)
                {
                    HttpContext.Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizdeki tüm ürünler cıkarılmıstır";
                    return RedirectToAction("ShoppingList");
                }
              
            }
            return RedirectToAction("CartPage");

        }

        public IActionResult ConfirmOrder()
        {
            AppUser user = null;
            if (HttpContext.Session.TryGetValue("member", out byte[]memberBytes))//ilk önce member byte dizisi yarattık. ardından bu byte dizisini memberstringe dönüştürdüm.ve _aman üzerinden yaparak usera değişken atadım.
            {
                string memberString=Encoding.UTF8.GetString(memberBytes);
                user = _aMan.FirstOrDefault(x=> x.UserName==memberString);
            }

        
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmOrder(OrderPageVM ovm)
        {
            bool sonuc;
            Cart sepet = HttpContext.Session.GetObject<Cart>("scart");
            ovm.Order.TotalPrice = ovm.PaymentRM.ShoppingPrice = sepet.TotalPrice;
          
            #region APISection
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5017/api");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", ovm.PaymentRM);
                
                HttpResponseMessage result;
                try
                {
                    result = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka bağlantıyı reddetti";
                    return RedirectToAction("ShoppingList");
                    
                }

                if (result.IsSuccessStatusCode) sonuc = true;
                else sonuc = false;
               
                if (sonuc)
                {
                    // session ile member kontrolünü yapacağız 
                    if (HttpContext.Session.GetString("member") != null)
                    // sessiondaki member null değil ise
                    {
                        string memberID = HttpContext.Session.GetString("member");
                        //session member memberıd içerisinde tut.
                        int userID;
                        // burada bir tane id tanımladı ki bunun üzerinden işlem gerçekleştirebilelim.
                        if (int.TryParse(memberID, out userID))
                        // tryparse ile dönüşüm yaptık bu sayede başarız bir işlem olursa false başarılı bir işlem gerçekleştirirsek true dönecek.Parse kullansaydım başarız olursa format exp. alacaktım. 
                        //memberID ile elde ettiğim değer userID atanır.
                        {
                            AppUser kullanici = _aMan.FirstOrDefault(x => x.ID == userID);
                            if (kullanici != null)
                            {
                                ovm.Order.AppUserID = kullanici.ID;
                            }
                            // burada kullanıcıya first ve default değer döndürdük ıd ile user ıdsi eşlenen bundan soran kullanıcı null değilse dedik kullanıcın idsini oorder üzerinden çağırdık ki ordera kullanıcı id eklensin.
                            
                        }


                    }
                    //todo  _orderMan.Add(ovm.Order);
                    foreach (CartItem item in sepet.Sepetim)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = ovm.Order.ID;
                        od.ProductID = item.ID;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odMan.Add(od);
                        Product dusulecek = _prMan.Find(item.ID);
                        dusulecek.UnitInStock -= item.Amount;
                        _prMan.Update(dusulecek);
                    }
                    TempData["odeme"] = "siparişniz bize ulaşmıştır. Teşekkür ederiz";
                }
                return RedirectToAction("ShoppingList");
            }
            #endregion

        }


    }
}