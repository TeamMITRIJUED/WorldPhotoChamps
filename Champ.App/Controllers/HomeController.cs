namespace Champ.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using Models.ContestModels;
    using Models.PhotoModels;
    using Models.HomeModels;
    using Models.NotificationModels;


    public class HomeController : BaseController
    {
        private const int PageSize = 5;

        public ActionResult Index(int id = 1)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            if (loggedUserId != null)
            {
                var contests = this.Data.Contests.All()
                .Where(c => c.Participants.Any(u => u.Id == loggedUserId))
                .ToList();

                var paged = new HomeViewModel
                {
                    PageCount = contests.Count % PageSize == 0 
                            ? contests.Count / PageSize 
                            : contests.Count / PageSize + 1,
                    PageSize = PageSize,
                    CurrentPage = id,
                    Contests = contests
                        .OrderByDescending(c => c.CreatenOn)
                        .Skip((id - 1) * PageSize)
                        .Take(PageSize)
                        .Select(c => new ContestParticipantViewModel
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Creator = c.Creator.UserName,
                            CurrentLeader = c.CurrentLeader == null ? "No leader yet" : c.CurrentLeader.UserName,
                            Pictures = c.Pictures
                                .Take(4)
                                .Select(p => new PhotoViewModel()
                                {
                                    Id = p.Id,
                                    Author = p.Author.UserName,
                                    ContestId = c.Id,
                                    Location = p.LocationPath,
                                    Votes = p.Votes.Count,
                                    HasVoted = p.Votes.Any(v => v.VoterId == loggedUserId)
                                })
                                .ToList()
                        }).ToList()
                };

                return View(paged);
            }


            var contestsForAnonymUser = this.Data.Contests.All()
                .OrderByDescending(c => c.CreatenOn)
                .ToList();


            var result = new HomeViewModel
            {
                PageCount = contestsForAnonymUser.Count % PageSize == 0
                            ? contestsForAnonymUser.Count / PageSize
                            : contestsForAnonymUser.Count / PageSize + 1,
                PageSize = PageSize,
                CurrentPage = id,
                Contests = contestsForAnonymUser
                    .Skip((id - 1) * PageSize)
                    .Take(PageSize)
                    .Select(c => new ContestParticipantViewModel
                    {
                        Title = c.Title,
                        CurrentLeader = c.CurrentLeader == null ? "No leader yet" : c.CurrentLeader.UserName,
                        Pictures = c.Pictures
                            .Take(3)
                            .Select(p => new PhotoViewModel
                            {
                                Location = p.LocationPath,
                                Author = p.Author.UserName,
                                Votes = p.Votes.Count
                            }).ToList(),
                        Description = c.Description
                    }).ToList()
            };

            return View(result);
        }

        [Authorize]
        public ActionResult MyContests()
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var userContests = this.Data.Contests.All()
                .Where(c => c.CreatorId == loggedUserId)
                .OrderByDescending(c => c.ClosesOn)
                .ProjectTo<ContestViewModel>()
                .ToList();

            return this.View(userContests);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPhotos()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var photos = this.Data.Pictures.All()
                .Where(u => u.AuthorId == loggedUserId)
                .Take(10)
                .Select(p => new PhotoViewModel
                {
                    Id = p.Id,
                    Location = p.LocationPath,
                    Author = p.Author.UserName,
                    HasVoted = p.Votes.Any(v => v.VoterId == loggedUserId),
                    Votes = p.Votes.Count
                })
                .ToList();

            return View(photos);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Notifications()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var model = new BrowseNotificationsViewModel
            {
                Notifications = this.Data.Notifications.All()
                    .OrderByDescending(n => n.DateSent)
                    .Where(n => n.ReceiverId == loggedUserId)
                    .Select(NotificationViewModel.Create)
                    .ToList()
            };

            return this.View(model);
        }
    }
}