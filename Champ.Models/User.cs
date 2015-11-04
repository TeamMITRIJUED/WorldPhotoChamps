namespace Champ.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Enums;

    public class User : IdentityUser
    {
        private ICollection<Contest> participatedIn;
        private ICollection<Contest> createdContests;
        private ICollection<Contest> evaluatedContests;
        private ICollection<Contest> wonContests;
        private ICollection<Contest> leadingContests; 
        private ICollection<Picture> uploadedPictures;
        private ICollection<Vote> votes;

        public User()
        {
            this.participatedIn = new HashSet<Contest>();
            this.createdContests = new HashSet<Contest>();
            this.evaluatedContests = new HashSet<Contest>();
            this.uploadedPictures = new HashSet<Picture>();
            this.leadingContests = new HashSet<Contest>();
            this.votes = new HashSet<Vote>();
            this.wonContests = new HashSet<Contest>();
        }

        public Role Role { get; set; }

        public virtual ICollection<Contest> WonContests
        {
            get { return this.wonContests; }
            set { this.wonContests = value; }
        }

        public virtual ICollection<Contest> ParticipatedIn
        {
            get { return this.participatedIn; }
            set { this.participatedIn = value; }
        }

        public virtual ICollection<Contest> CreatedContests
        {
            get { return this.createdContests; }
            set { this.createdContests = value; }
        }

        public virtual ICollection<Contest> LeadingContests
        {
            get { return this.leadingContests; }
            set { this.leadingContests = value; }
        } 

        public virtual ICollection<Contest> EvaluatedContests
        {
            get { return this.evaluatedContests; }
            set { this.evaluatedContests = value; }
        }

        public virtual ICollection<Picture> UploadedPictures
        {
            get { return this.uploadedPictures; }
            set { this.uploadedPictures = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
