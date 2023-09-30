using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {
        void Insert(Task t);
        void Delete(Task t);
        void Update(Task t);
        List<T> GetList();
        T Get(Expression<Func<T, bool>> filter);
    }
}
