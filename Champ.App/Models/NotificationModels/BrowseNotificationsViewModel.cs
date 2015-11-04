namespace Champ.App.Models.NotificationModels
{
    using System.Collections.Generic;

    public class BrowseNotificationsViewModel
    {
        public ICollection<NotificationViewModel> Notifications { get; set; } 
    }
}