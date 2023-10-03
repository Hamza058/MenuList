using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Runtime.InteropServices;

namespace MenuList.Controllers
{
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EFProductDal());
		private readonly IFileProvider _fileProvider;

		public ProductController(IFileProvider fileProvider)
		{
			_fileProvider = fileProvider;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Get(string name)
		{
			if (name == null)
				name = "";
			var products = pm.TGetCategory().Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList();
			return Json(products);
		}

		public IActionResult Admin()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Product product, IFormFile file)
		{
			var root = _fileProvider.GetDirectoryContents("wwwroot");
			var images = root.First(x => x.Name == "img");
			var randomImageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
			var path = Path.Combine(images.PhysicalPath, randomImageName);
			using (var stream = new FileStream(path, FileMode.Create))
			{
				file.CopyTo(stream);
			}
			product.CategoryId = 1;
			product.Image = randomImageName;

			pm.TAdd(product);
			return RedirectToAction("Admin");
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var product = pm.TGetById(id);
			var root = _fileProvider.GetDirectoryContents("wwwroot");
			var images = root.First(x => x.Name == "img");
			var path = Path.Combine(images.PhysicalPath, product.Image);
			System.IO.File.Delete(path);
			pm.TDelete(product);
			return Json(new { IsSuccess = "true" });
		}
	}
}
