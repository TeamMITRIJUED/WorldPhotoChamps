namespace Champ.App.Areas.Dashboard.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using App.Models.ContestModels;

    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string Username { get; set; }

        public int OwnContest { get; set; }

        public int ParticipatedInContests { get; set; }

        public int WonContests { get; set; }

        public int UploadedPhotos { get; set; }

        public ICollection<ContestViewModel> Contests { get; set; }

        public ICollection<ContestViewModel> OwnContests { get; set; } 
    }
}