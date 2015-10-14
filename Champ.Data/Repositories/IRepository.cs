using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Champ.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);
    }
}
