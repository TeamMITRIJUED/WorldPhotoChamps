namespace Champ.App.Models.ContestModels
{
    using System;
    using System.Collections.Generic;
    using PhotoModels;

    public class ContestParticipantViewModel : ContestViewModel
    {
        public string Creator { get; set; }

        public string CurrentLeader { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PhotoViewModel> Pictures { get; set; }

        public bool HasAddedPhoto { get; set; }
    }
}