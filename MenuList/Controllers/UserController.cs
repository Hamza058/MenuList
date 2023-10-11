using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System;
using MenuList.Authenticate;
using Microsoft.AspNetCore.Authorization;

namespace MenuList.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        UserManager um = new UserManager(new EFUserDal());

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                var token = Created("", new CreateToken(_configuration).Token(value));
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