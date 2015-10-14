using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Champ.Data.Repositories;
using Champ.Models;

namespace Champ.Data.UnitOfWork
{
    public interface IPhotoData
    {
        IRepository<Contest> Contests { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<User> Users { get; }

        IRepository<Vote> Votes { get; }

        int SaveChanges();

    }
}
