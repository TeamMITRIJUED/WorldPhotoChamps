namespace Champ.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        private ICollection<Vote> votes;

        public Picture()
        {
            this.votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        public string LocationPath { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }
    }
}
