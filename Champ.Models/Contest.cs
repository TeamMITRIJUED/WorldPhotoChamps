using System.Security.Permissions;

namespace Champ.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using Enums;

    public class Contest
    {
        private ICollection<User> participants;
        private ICollection<User> comittee; 
        private ICollection<Picture> pictures;

        public Contest()
        {
            this.participants = new HashSet<User>();
            this.pictures = new HashSet<Picture>();
            this.comittee = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public string WinnerId { get; set; }

        public virtual User Winner { get; set; }

        public DateTime CreatenOn { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DateTime? ClosesOn { get; set; }

        public int? NumberOfAllowedParticipants { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }

        public bool IsDismissed { get; set; }

        public virtual ICollection<User> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        public virtual ICollection<User> Comittee
        {
            get { return this.comittee; }
            set { this.comittee = value; }
        }
    }
}