namespace Champ.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Migrations;
    using Models;

    public class PhotoContext : IdentityDbContext<User>
    {
        public PhotoContext()
            : base("name=PhotoContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContext, Configuration>());
        }

        public static PhotoContext Create()
        {
            return new PhotoContext();
        }

        public virtual IDbSet<Contest> Contests { get; set; }

        public virtual IDbSet<Picture> Pictures { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public virtual IDbSet<Notification> Notification { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contest>()
                .HasMany(c => c.Participants)
                .WithMany(u => u.ParticipatedIn)
                .Map(m =>
                {
                    m.MapLeftKey("ContestId");
                    m.MapRightKey("ParticipantId");
                    m.ToTable("Contests_Participants");
                });

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.Comittee)
                .WithMany(u => u.EvaluatedContests)
                .Map(m =>
                {
                    m.MapLeftKey("ContestId");
                    m.MapRightKey("MemberId");
                    m.ToTable("Contests_Evaluators");
                });

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.Invited)
                .WithMany(u => u.InvitedContests)
                .Map(m =>
                {
                    m.MapLeftKey("ContestId");
                    m.MapRightKey("InvitedId");
                    m.ToTable("Contests_Invitations");
                });

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.Declined)
                .WithMany(u => u.DeclinedContests)
                .Map(m =>
                {
                    m.MapLeftKey("ContestId");
                    m.MapRightKey("DeclinedId");
                    m.ToTable("Contests_Declined");
                });

            modelBuilder.Entity<Contest>()
                .HasMany(c => c.Pictures)
                .WithRequired(p => p.Contest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedContests)
                .WithRequired(c => c.Creator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.WonContests)
                .WithOptional(c => c.Winner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.LeadingContests)
                .WithOptional(c => c.CurrentLeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UploadedPictures)
                .WithRequired(p => p.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Votes)
                .WithRequired(v => v.Voter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedNotifications)
                .WithOptional(n => n.Receiver)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentNotifications)
                .WithOptional(n => n.Sender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Picture>()
                .HasMany(p => p.Votes)
                .WithRequired(v => v.Picture)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}