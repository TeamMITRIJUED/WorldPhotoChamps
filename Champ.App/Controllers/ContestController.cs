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

        private const int PageSize = 5;

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
                        Id = p.ContestId,
                        Author = p.Author.UserName,
                        Location = p.LocationPath,
                        Votes = p.Votes.Count
                    }).ToList(),
                    HasAddedPhoto = contest.Pictures.Any(p => p.AuthorId == loggedUserId),
                    IsDismissed = contest.IsDismissed
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

        [Authorize]
        public ActionResult ViewContests(int id = 1)
        {
            var loggedUserId = User.Identity.GetUserId();

            var contests = this.Data.Contests.All()
                .Where(c => c.ClosesOn > DateTime.Now && c.Participants.Count(p => p.Id == loggedUserId) == 0)
                .ToList();

            var model = new BrowseContestsViewModel
            {
                CurrentPage = id,
                PageSize = PageSize,
                PageCount = contests.Count % PageSize == 0
                    ? contests.Count / PageSize
                    : contests.Count / PageSize + 1,
                Contests = contests.OrderByDescending(c => c.CreatenOn)
                    .Skip((id - 1) * PageSize)
                    .Take(PageSize)
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
                    }).ToList()
            };
            
            return View(model);
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