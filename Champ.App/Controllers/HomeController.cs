using AutoMapper.QueryableExtensions;

namespace Champ.App.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System;

    using Models.ContestModels;
    using Models.PhotoModels;
    using Models.HomeModels;

    public class HomeController : BaseController
    {
        private const int PageSize = 3;

        public ActionResult Index(int id = 1)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var contests = this.Data.Contests.All()
                .Where(c => c.ClosesOn > DateTime.Now && c.Participants.Any(u => u.Id == loggedUserId))
                .ToList();

            var paged = new HomeViewModel
            {
                PageCount = contests.Count / PageSize,
                PageSize = PageSize,
                CurrentPage = id,
                Contests = contests
                    .OrderByDescending(c => c.CreatenOn)
                    .Skip((id - 1) * PageSize)
                    .Take(PageSize)
                    .Select(c => new ContestParticipantViewModel()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Creator = c.Creator.UserName,
                        Photos = c.Pictures
                            .Take(4)
                            .Select(p => new PhotoViewModel()
                            {
                                Author = p.Author.UserName,
                                ContestId = c.Id,
                                Location = p.LocationPath
                            })
                            .ToList()
                    }).ToList()
            };


            return View(paged);
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
                    Location = p.LocationPath,
                    Author = p.Author.UserName
                })
                .ToList();

            return View(photos);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}