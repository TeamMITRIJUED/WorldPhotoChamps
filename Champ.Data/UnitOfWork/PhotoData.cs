using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Champ.Data.Repositories;
using Champ.Models;

namespace Champ.Data.UnitOfWork
{
    public class PhotoData : IPhotoData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public PhotoData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Contest> Contests
        {
            get { return this.GetRepository<Contest>(); }
        }

        public IRepository<Picture> Pictures
        {
            get { return this.GetRepository<Picture>(); }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof (T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof (GenericRepository<T>);
                var repository = Activator.CreateInstance(
                    typeOfRepository, this.context);

                this.repositories.Add(type, repository);
            }

            return (IRepository<T>) this.repositories[type];
        }
    }
}
