using System;
using System.Linq.Expressions;
using Champ.Models;
using Champ.Models.Enums;

namespace Champ.App.Models.ContestModels
{
    public class EditContestViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime? ClosesOn { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        //public RewardStrategy RewardStrategy { get; set; }

        //public ParticipationStrategy ParticipationStrategy { get; set; }

        //public DeadlineStrategy DeadlineStrategy { get; set; }

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
                    //DeadlineStrategy = c.DeadlineStrategy,
                    NumberOfAllowedParticipants = c.NumberOfAllowedParticipants,
                    //ParticipationStrategy = c.ParticipationStrategy,
                    //RewardStrategy = c.RewardStrategy,
                    VotingStrategy = c.VotingStrategy
                };
            }
        }

    }
}