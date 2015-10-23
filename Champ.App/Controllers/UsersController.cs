using System.Linq;
using System.Web;
using Champ.App.Models;
using Microsoft.AspNet.Identity;

namespace Champ.App.Controllers
{
    using System.Web.Mvc;

    public class UsersController : BaseController
    {
        [Authorize]
        public ActionResult ViewProfile()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var userProfile = this.Data.Users.All()
                .Where(u => u.Id == loggedUserId)
                .Select(UserProfileViewModel.Create)
                .FirstOrDefault();
            
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ApplyToContest(int id)
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var loggedUser = this.Data.Users.Find(loggedUserId);
            var contest = this.Data.Contests.Find(id);

            if (contest.Participants.Any(p => p.Id == loggedUserId))
            {
                throw new HttpException();
            }

            contest.Participants.Add(loggedUser);
            this.Data.SaveChanges();

            return RedirectToAction("ViewContests", "Contest");
        }

        public ActionResult InviteToContest()
        {
            
        }
    }
}