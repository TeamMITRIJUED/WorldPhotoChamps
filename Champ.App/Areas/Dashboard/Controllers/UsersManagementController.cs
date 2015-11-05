namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using App.Controllers;
    using Models;
    using App.Models.ContestModels;

    [Authorize(Roles = "Admin")]
    public class UsersManagementController : BaseController
    {
        public ActionResult Get()
        {
            var users = this.Data.Users.All()
                .Take(10)
                .Select(u => new UserViewModel
                {
                    UserId = u.Id,
                    Username = u.UserName,
                    OwnContest = u.CreatedContests.Count,
                    ParticipatedInContests = u.ParticipatedIn.Count,
                    UploadedPhotos = u.UploadedPictures.Count,
                    Contests = u.ParticipatedIn        
                        .OrderByDescending(c => c.CreatenOn)
                        .Select(c => new ContestViewModel
                        {
                            Id = c.Id,
                            Title = c.Title
                        })
                        .ToList(),
                    OwnContests = u.CreatedContests
                        .OrderByDescending(c => c.CreatenOn)
                            .Select(c => new ContestViewModel
                            {
                                Id = c.Id,
                                Title = c.Title
                            })
                            .ToList()
                })
                .ToList();

            return this.PartialView("_ShowUsersPartial", users);
        }

        public ActionResult Remove(int userId, int contestId)
        {
            var user = this.Data.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            var contest = this.Data.Contests.Find(contestId);

            if (contest == null)
            {
                return HttpNotFound();
            }

            contest.Participants.Remove(user);
            user.ParticipatedIn.Remove(contest);
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}