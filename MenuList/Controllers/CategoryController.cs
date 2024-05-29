using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace MenuList.Controllers
{
	public class CategoryController : Controller
	{
		CategoryManager cm = new CategoryManager(new EFCategoryDal());
        private readonly IFileProvider _fileProvider;

        public CategoryController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

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
		public IActionResult Add(Category category, IFormFile file)
		{
            var root = _fileProvider.GetDirectoryContents("wwwroot/Images");
            var images = root.First(x => x.Name == "Category");
            var randomImageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(images.PhysicalPath, randomImageName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
			category.CategoryImage = randomImageName;

            cm.TAdd(category);
			return Json(new { IsSuccess = "true" });
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var category = cm.TGetById(id);

            var root = _fileProvider.GetDirectoryContents("wwwroot/Images");
            var images = root.First(x => x.Name == "Category");
            var path = Path.Combine(images.PhysicalPath, category.CategoryImage);
            System.IO.File.Delete(path);

            cm.TDelete(category);
            return Json(new { IsSuccess = "true" });
		}

		[HttpPost]
		public IActionResult Update(Category category, IFormFile file)
		{
			var value = cm.TGetById(category.CategoryId);
			value.CategoryName = category.CategoryName;

            if (file != null)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot/Images");
                var images = root.First(x => x.Name == "Category");

                var delete_path = Path.Combine(images.PhysicalPath, value.CategoryImage);
                System.IO.File.Delete(delete_path);

                var randomImageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(images.PhysicalPath, randomImageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                value.CategoryImage = randomImageName;
            }

            cm.TUpdate(value);
            return Json(new { IsSuccess = "true" });
        }

		[HttpGet]
		public IActionResult GetById(int id)
		{
			return Json(cm.TGetById(id));
		}
	}
}
