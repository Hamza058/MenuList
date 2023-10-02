using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MenuList.Controllers
{
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EFProductDal());

        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Get()
		{
			return Json(pm.TGetList());
		}

		[HttpPost]
		public IActionResult Add(Product product)
		{
            pm.TAdd(product);
			return Json(new { IsSuccess = "true" });
		}
	}
}
