using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MenuList.Controllers
{
	public class CategoryController : Controller
	{
		CategoryManager cm = new CategoryManager(new EFCategoryDal());

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
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

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			cm.TDelete(cm.TGetById(id));
			return Json(new { IsSuccess = "true" });
		}
	}
}
