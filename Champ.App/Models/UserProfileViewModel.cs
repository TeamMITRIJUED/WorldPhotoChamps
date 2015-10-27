namespace Champ.App.Models
{
    using System;
    using System.Linq.Expressions;
    using Champ.Models;

    public class UserProfileViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public int OwnContest { get; set; }

        public int ParticipatedInContests { get; set; }

        public int WonContests { get; set; }

        public int UploadedPhotos { get; set; }    

        public static Expression<Func<User, UserProfileViewModel>> Create
        {
            get
            {
                return u => new UserProfileViewModel
                {
                    UserId = u.Id,
                    Username = u.UserName,
                    OwnContest = u.CreatedContests.Count,
                    ParticipatedInContests = u.ParticipatedIn.Count,
                    WonContests = u.WonContests.Count,
                    UploadedPhotos = u.UploadedPictures.Count
                };
            }
        }
    }
}