using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
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
                return RedirectToAction("Register");
            }
            else
            {
                ViewBag.Message = "Lütfen girdiğiniz bilgileri kontrol ediniz";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            Console.WriteLine(user.Password);
            um.TAdd(user);

            return RedirectToAction("Login");
        }
    }
}
