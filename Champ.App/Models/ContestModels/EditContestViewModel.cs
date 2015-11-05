using System.ComponentModel.DataAnnotations;

namespace Champ.App.Models.ContestModels
{
    using System;
    using System.Linq.Expressions;
    using Champ.Models;
    using Champ.Models.Enums;

    public class EditContestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description length should be less than 100")]
        public string Description { get; set; }

        public DateTime? ClosesOn { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public int? NumberOfAllowedParticipants { get; set; }

        public static Expression<Func<Contest, EditContestViewModel>> Create
        {
            get
            {
                return c => new EditContestViewModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    ClosesOn = c.ClosesOn,
                    NumberOfAllowedParticipants = c.NumberOfAllowedParticipants,
                    VotingStrategy = c.VotingStrategy
                };
            }
        }

    }
}