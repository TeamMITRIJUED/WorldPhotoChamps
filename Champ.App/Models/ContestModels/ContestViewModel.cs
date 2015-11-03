namespace Champ.App.Models.ContestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Champ.Models;
    using Champ.Models.Enums;

    public class ContestViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description length should be less than 100")]
        public string Description { get; set; }

        public DateTime CreatenOn { get; set; }

        [Required(ErrorMessage = "Voting Strategy is required.")]
        public VotingStrategy VotingStrategy { get; set; }

        [Required(ErrorMessage = "Reward Strategy is required.")]
        public RewardStrategy RewardStrategy { get; set; }

        [Required(ErrorMessage = "Participation Strategy is required.")]
        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DateTime? ClosesOn { get; set; }

        [Range(5, 100)]
        public int? NumberOfAllowedParticipants { get; set; }

        [Required(ErrorMessage = "Deadline Strategy is required.")]
        public DeadlineStrategy DeadlineStrategy { get; set; }

        public int CountOfParticipants { get; set; }

        public bool HasParticipated { get; set; }

        public bool IsDismissed { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contest, ContestViewModel>()
                .ForMember(c => c.CountOfParticipants, opt => opt.MapFrom(contest => contest.Participants.Count));

        }
    }
}