using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MenuList.Filter;
using Microsoft.AspNetCore.Mvc;

namespace MenuList.Controllers
{
    public class UserController : Controller
    {
        UserManager um = new UserManager(new EFUserDal());

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var users = um.TGetList();
            var value = users.FirstOrDefault(x=>x.UserName == user.UserName && BCrypt.Net.BCrypt.Verify(user.Password, x.Password));

            if (value != null)
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Admin", "Category");
            }
            else
            {
                ViewBag.Message = "Lütfen girdiğiniz bilgileri kontrol ediniz";
                return View();
            }
        }

        [SessionAuthorize]
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [SessionAuthorize]
        [HttpPost]
        public IActionResult Update(string password)
        {
            var value = um.TGetList().FirstOrDefault(x => x.UserName == HttpContext.Session.GetString("UserName"));
            value.Password = BCrypt.Net.BCrypt.HashPassword(password);
            um.TUpdate(value);
            ViewBag.Message = "Parolanız Değiştirilmiştir";
            return View();
        }

        public IActionResult ExitUser()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}