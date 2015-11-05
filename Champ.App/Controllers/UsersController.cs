using System;
using Champ.App.Models.NotificationModels;

namespace Champ.App.Controllers
{
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using System.Net;
    using Models.ContestModels;
    using Models.SearchModels;
    using Champ.Models;
    using Models.UserModels;

    public class UsersController : BaseController
    {
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

            if (contest.Invited.Any(p => p.Id == model.UserId) || contest.Participants.Any(p => p.Id == model.UserId))
            {
                return this.Content("User already invited", "text/html");
            }

            var invitedUser = this.Data.Users.Find(model.UserId);
            var loggedUser = this.Data.Users.Find(loggedUserId);

            invitedUser.InvitedContests.Add(contest);
            contest.Invited.Add(invitedUser);

            var text = string.Format("Hello, {0}, I'd like you to join the contest {1}",
                invitedUser.UserName, contest.Title);

            var notification = new Notification
            {
                Text = text,
                DateSent = DateTime.Now,
                ReceiverId = model.UserId,
                Receiver = invitedUser,
                SenderId = loggedUserId,
                Sender = loggedUser,
                ContestId = contest.Id
            };

            this.Data.Notifications.Add(notification);
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

            return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
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

            return RedirectToAction("MyContests", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AcceptInvititation(ResolveNotificationBindingModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                throw new HttpException();
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var loggedUser = this.Data.Users.Find(loggedUserId);
            var notification = this.Data.Notifications.Find(model.NotificationId);
            var contest = this.Data.Contests.Find(model.ContestId);

            contest.Invited.Remove(loggedUser);
            contest.Participants.Add(loggedUser);
            notification.IsRead = true;
            this.Data.SaveChanges();

            return this.Content("Invitation accepted", "text/html");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeclineInvitation(ResolveNotificationBindingModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                throw new HttpException();
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var loggedUser = this.Data.Users.Find(loggedUserId);
            var notification = this.Data.Notifications.Find(model.NotificationId);
            var contest = this.Data.Contests.Find(model.ContestId);

            contest.Invited.Remove(loggedUser);
            contest.Declined.Add(loggedUser);
            notification.IsRead = true;
            this.Data.SaveChanges();

            return this.Content("Invitation declined", "text/html");
        }
    }
}