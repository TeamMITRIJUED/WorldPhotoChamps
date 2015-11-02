namespace Champ.App.Controllers
{
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using System.Net;
    using AutoMapper.QueryableExtensions;
    using Models.ContestModels;
    using Models.SearchModels;
    using Models.UserModels;

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

        [Authorize]
        [HttpPost]
        public ActionResult InviteToContest(UserInvitationViewModel model)
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var contest = this.Data.Contests.Find(model.ContestId);

            if (!this.ModelState.IsValid)
            {
                throw new HttpException();
            }

            if (loggedUserId != contest.CreatorId)
            {
                throw new HttpException();
            }

            var invitedUser = this.Data.Users.All().FirstOrDefault(u => u.Id == model.UserId);

            if (invitedUser == null)
            {
                throw new HttpException();
            }

            if (contest.Participants.Contains(invitedUser))
            {
                var failMessage = string.Format("User {0} already invited", invitedUser.UserName);
                return this.Content(failMessage, "text/html");
            }

            invitedUser.ParticipatedIn.Add(contest);
            contest.Participants.Add(invitedUser);
            this.Data.SaveChanges();

            var message = string.Format("Invitation to {0} sent", invitedUser.UserName);

            return this.Content(message, "text/html");

        }

        [Authorize]
        public ActionResult DismissContest(int id)
        {
            var contestDismissed = this.Data.Contests.Find(id);
            contestDismissed.IsDismissed = true;
            this.Data.SaveChanges();

            return RedirectToAction("MyContests", "Users");
        }

        public ActionResult GetUser(string username)
        {
            var user = this.Data.Users.All()
                .Where(u => u.UserName == username)
                .Select(UserProfileViewModel.Create)
                .FirstOrDefault();

            return View("_ViewUser", user);
        }

        public ActionResult GetAllUsers()
        {
            var users = this.Data.Users.All()
                .Take(10)
                .Select(UserProfileViewModel.Create)
                .ToList();

            return View(users);
        }

        [Authorize]
        public ActionResult MyContests()
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var userContests = this.Data.Contests.All()
                .Where(c => c.CreatorId == loggedUserId)
                .Take(6)
                .OrderByDescending(c => c.ClosesOn)
                .ProjectTo<ContestViewModel>()
                .ToList();

            return View(userContests);
        }

        public ActionResult SearchUsers(string username, int contestId)
        {
            username = username.ToLower();
            var model = new SearchViewModel
            {
                Users = this.Data.Users.All()
                    .Where(u => u.UserName.ToLower().Contains(username))
                    .Select(u => new UserInvitationViewModel
                    {
                        ContestId = contestId,
                        UserId = u.Id,
                        Username = u.UserName
                    })
                    .ToList()
            };

            return this.PartialView("_UserSearchPartial", model);
        }

        [Authorize]
        public ActionResult EditContest(EditContestViewModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid message");
            }

            var editedContest = this.Data.Contests.Find(model.Id);

            if (editedContest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "nope.. not today");
            }

            if(model.ClosesOn != null)
            {
                editedContest.ClosesOn = model.ClosesOn; 
            }

            if (model.Description != null)
            {
                editedContest.Description = model.Description;
            }

            if (model.NumberOfAllowedParticipants != null)
            {
                editedContest.NumberOfAllowedParticipants = model.NumberOfAllowedParticipants;
            }
            
            editedContest.VotingStrategy = model.VotingStrategy;
            this.Data.SaveChanges();

            return RedirectToAction("MyContests", "Users");
        }
    }
}