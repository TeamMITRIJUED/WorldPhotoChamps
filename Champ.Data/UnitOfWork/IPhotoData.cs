namespace Champ.Data.UnitOfWork
{
    using Repositories;
    using Models;

    public interface IPhotoData
    {
        IRepository<Contest> Contests { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<User> Users { get; }

        IRepository<Vote> Votes { get; }

        IRepository<Notification> Notifications { get; }

        int SaveChanges();

    }
}
