using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System;

namespace MenuList.Controllers
{
    public class UserController : Controller
    {
        UserManager um = new UserManager(new EFUserDal());

        public IActionResult Admin()
        {
            return View();
        }
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
                return RedirectToAction("Admin", "Category");
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
            um.TAdd(user);

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(User user, string oldPassword)
        {
            var value = um.TGetList().FirstOrDefault(x => x.UserName == user.UserName && BCrypt.Net.BCrypt.Verify(oldPassword, x.Password));
            if(value != null)
            {
                value.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                um.TUpdate(value);
                return RedirectToAction("Login");
            }
            return RedirectToAction("Update");
        }
    }
}