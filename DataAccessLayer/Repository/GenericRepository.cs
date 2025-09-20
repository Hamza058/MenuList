using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        Context c = new Context();
        DbSet<T> _object;

        public GenericRepository() 
        {
            _object = c.Set<T>();
        }

        public void Delete(T t)
        {
            _object.Remove(t);
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public List<T> GetList()
        {
            return _object.ToList();
        }

        public void Insert(T t)
        {
            _object.Add(t);
            c.SaveChanges();
        }

        public void Update(T t)
        {
            _object.Update(t);
            c.SaveChanges();
        }
    }
}
