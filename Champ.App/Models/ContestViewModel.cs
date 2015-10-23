namespace Champ.App.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using Champ.Models;
    using Champ.Models.Enums;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatenOn { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DateTime? ClosesOn { get; set; }

        public int? NumberOfAllowedParticipants { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }

        public int CountOfParticipants { get; set; }

        public bool HasParticipated { get; set; }
    }
}