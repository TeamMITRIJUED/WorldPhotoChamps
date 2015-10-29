namespace Champ.App.Models.SearchModels
{
    using System.Collections.Generic;
    using UserModels;

    public class SearchViewModel
    {
        public ICollection<UserInvitationViewModel> Users { get; set; } 
    }
}