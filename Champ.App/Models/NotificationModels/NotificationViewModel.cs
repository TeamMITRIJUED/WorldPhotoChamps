namespace Champ.App.Models.NotificationModels
{
    using System;
    using System.Linq.Expressions;
    using Champ.Models;

    public class NotificationViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public static Expression<Func<Notification, NotificationViewModel>> Create
        {
            get
            {
                return n => new NotificationViewModel
                {
                    Id = n.Id,
                    Message = n.Text,
                    SenderId = n.SenderId,
                    SenderName = n.Sender.UserName
                };
            }
        }
    }
}