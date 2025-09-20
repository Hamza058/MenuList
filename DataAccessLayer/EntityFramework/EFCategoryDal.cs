using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public new List<Category> GetList()
        {
            using (var c = new Context())
            {
                return c.Categories.Include(x => x.Products).ToList();
            }
        }
    }
}
