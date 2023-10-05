using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Google.Protobuf.WellKnownTypes;
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

		[HttpGet]
		public IActionResult GetById(int id)
		{
			var products = pm.TGetById(id);
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
            return Json(new { IsSuccess = "true" });
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

		[HttpPost]
		public IActionResult Update(Product product, IFormFile file)
		{
			var value = pm.TGetById(product.ProductId);
			value.ProductName = product.ProductName;
			value.Description = product.Description;
			value.Price = product.Price;
			if (file != null)
			{
				var root = _fileProvider.GetDirectoryContents("wwwroot");
				var images = root.First(x => x.Name == "img");

				var delete_path = Path.Combine(images.PhysicalPath, value.Image);
				System.IO.File.Delete(delete_path);

				var randomImageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
				var path = Path.Combine(images.PhysicalPath, randomImageName);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					file.CopyTo(stream);
				}
				value.Image = randomImageName;
			}
			pm.TUpdate(value);
            return Json(new { IsSuccess = "true" });
        }
    }
}
