using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;

namespace HybridNTierECommerce.Controllers
{
    public class RegisterController : Controller
    {
        IAppUserManager _uMan;
        IAppUserProfileManager _uProfMan;
        public RegisterController(IAppUserManager uMan, IAppUserProfileManager uProfMan)
        {
            _uMan = uMan;
            _uProfMan = uProfMan;
        }
        public IActionResult RegisterNow()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterNow(AppUserVM appUser, AppUserProfileVM profile)
        {
            if (_uMan.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce kullanılmıştır.Lütfen başka bir kullanıcı ismi deneyin";
                return View();
                //appusera ait manager ile domain entiteste yer alan user isimlerini karşılaştırdım eğer varsa bu isimde tekrar bir kullanıcı yaratmak istedim.
            }
            else if (_uProfMan.Any(x => x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Email adresi daha önce bir üyelik oluşturmuştur.";
            }

            appUser.Password = DantexCrypt.Crypt(appUser.Password);
            AppUser domain = new AppUser
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
                Email = appUser.Email
            };
            // daha önce yazılmış olan dantext cyrpti kullandım. identity araştır.
            //todo indetity nasıl yapılıyor iyice öğren
            _uMan.Add(domain);
            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) || !string.IsNullOrEmpty(profile.LastName.Trim()))
            {
                AppUserProfile domainProfile = new AppUserProfile
                {
                    ID = domain.ID,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                };
            }
            return View("RegisterOK");
        }
        public IActionResult RegisterOK()
        {
            return View();
        }
    }
}
