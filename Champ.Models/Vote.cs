namespace Champ.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        public int Id { get; set; }

        public DateTime VotedOn { get; set; }

        [Required]
        public string VoterId { get; set; }

        public virtual User Voter { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
