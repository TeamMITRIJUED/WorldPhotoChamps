namespace Champ.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using Champ.Models;
    using Models.PhotoModels;
    using Models.ContestModels;
    
    public class ContestController : BaseController
    {
        // GET: Contest
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddContest()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContest(ContestViewModel contest)
        {
            var loggedUserId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == loggedUserId);
            if (user != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(contest);
                }

                if (contest == null)
                {
                    return View(contest);
                }

                var newContest = new Contest()
                {
                    Title = contest.Title,
                    Description = contest.Description,
                    CreatorId = loggedUserId,
                    CreatenOn = DateTime.Now,
                    ClosesOn = contest.ClosesOn,
                    NumberOfAllowedParticipants = contest.NumberOfAllowedParticipants,
                    ParticipationStrategy = contest.ParticipationStrategy,
                    DeadlineStrategy = contest.DeadlineStrategy,
                    RewardStrategy = contest.RewardStrategy,
                    VotingStrategy = contest.VotingStrategy,
                };

                user.CreatedContests.Add(newContest);
                this.Data.SaveChanges();
            }

            TempData["contest"] = contest.Title;
            return View();
        }


        public ActionResult GetContest(int id)
        {
            var loggedUserId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == loggedUserId);
            var contest = this.Data.Contests.Find(id);

            if (loggedUserId != null && contest.Participants.Any(p => p.Id == loggedUserId))
            {
                var contestToReturn = new ContestParticipantViewModel()
                {
                    Id = contest.Id,
                    Title = contest.Title,
                    Description = contest.Description,
                    Creator = contest.Creator.UserName,
                    CreatedOn = contest.CreatenOn,
                    ClosesOn = contest.ClosesOn,
                    Pictures = contest.Pictures.Take(10).Select(p => new PhotoViewModel
                    {
                        Author = p.Author.UserName,
                        Location = p.LocationPath
                    }).ToList(),
                    HasAddedPhoto = contest.Pictures.Any(p => p.AuthorId == loggedUserId)
                };
                return View("ViewContestParticipatingUser", contestToReturn);
            }


            var searchedContest = this.Data.Contests.All()
                .Where(c => c.Id == id)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            if (searchedContest == null)
            {
                return View("Error");
            }

            if (user != null)
            {
                return View("GetContestToLoggedUser", searchedContest);
            }

            return View("GetContestToAnonymousUser", searchedContest);
        }

        public ActionResult ViewContests()
        {

            var loggedUserId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == loggedUserId);


            if (user == null)
            {
                var contests = this.Data.Contests.All()
                 .Where(c => c.ClosesOn > DateTime.Now)
                 .OrderByDescending(c => c.CreatenOn)
                 .Take(10)
                 .Select(c => new ContestViewModel()
                 {
                     Id = c.Id,
                     Title = c.Title,
                     Description = c.Description,

                 }).ToList();

                return View("ViewAnonymousUser", contests);
            }
            else
            {
                var contests = this.Data.Contests.All()
                .Where(c => c.ClosesOn > DateTime.Now)
                .OrderByDescending(c => c.CreatenOn)
                .Take(10)
                .Select(c => new ContestViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CountOfParticipants = c.Participants.Count,
                    ClosesOn = c.ClosesOn,
                    NumberOfAllowedParticipants = c.NumberOfAllowedParticipants,
                    ParticipationStrategy = c.ParticipationStrategy,
                    HasParticipated = c.Participants.Any(p => p.Id == loggedUserId)
                }).ToList();
                return View("ViewLoggedUser", contests);
            }
        }

        public ActionResult PastContests()
        {
            var contests = this.Data.Contests.All()
                .Where(c => c.ClosesOn <= DateTime.Now)
                .OrderByDescending(c => c.ClosesOn)
                .ProjectTo<ContestViewModel>()
                .ToList();

            return View(contests);
        }
    }
}