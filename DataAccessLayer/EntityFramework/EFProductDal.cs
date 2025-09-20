using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.EntityFramework
{
    public class EFProductDal : GenericRepository<Product>, IProductDal
    {
        public new List<Product> GetList()
        {
			using (var c = new Context())
			{
				return c.Products.Include(x => x.Categories).ToList();
			}
		}

        public new Product Get(Expression<Func<Product, bool>> filter)
        {
            using (var c = new Context())
            {
                return c.Products.Include(x => x.Categories).SingleOrDefault(filter);
            }
        }
    }
}
