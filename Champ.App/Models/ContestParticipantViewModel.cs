namespace Champ.App.Models
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