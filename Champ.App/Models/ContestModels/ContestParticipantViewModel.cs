namespace Champ.App.Models.ContestModels
{
    using System;

    public class ContestParticipantViewModel : ContestViewModel
    {
        public string Creator { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Pictures { get; set; }

        public bool HasAddedPhoto { get; set; }
    }
}