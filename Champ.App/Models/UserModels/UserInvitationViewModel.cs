namespace Champ.App.Models.UserModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserInvitationViewModel
    {
        [Required]
        public string UserId { get; set; }

        public int ContestId { get; set; }

        public string Username { get; set; }
    }
}