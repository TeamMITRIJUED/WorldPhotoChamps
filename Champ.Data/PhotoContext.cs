using Champ.Data.Migrations;
using Champ.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Champ.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotoContext : IdentityDbContext<User>
    {
        // Your context has been configured to use a 'PhotoContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Champ.Data.PhotoContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PhotoContext' 
        // connection string in the application configuration file.
        public PhotoContext()
            : base("name=PhotoContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContext, Configuration>());
        }

        public static PhotoContext Create()
        {
            return new PhotoContext();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}