﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuList.Controllers
{
	public class CategoryController : Controller
	{
		CategoryManager cm = new CategoryManager(new EFCategoryDal());

        [HttpGet]
		[AllowAnonymous]
		public IActionResult Get()
		{
			return Json(cm.TGetList());
		}

		public IActionResult Admin()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Category category)
		{
			cm.TAdd(category);
			return Json(new { IsSuccess = "true" });
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			cm.TDelete(cm.TGetById(id));
			return Json(new { IsSuccess = "true" });
		}

		[HttpPost]
		public IActionResult Update(Category category)
		{
			var value = cm.TGetById(category.CategoryId);
			value.CategoryName = category.CategoryName;
			cm.TUpdate(value);
			return RedirectToAction("Admin");
		}

		[HttpGet]
		public IActionResult GetById(int id)
		{
			return Json(cm.TGetById(id));
		}
	}
}
