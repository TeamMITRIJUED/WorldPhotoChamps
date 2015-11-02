namespace Champ.App.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System;

    using Models.ContestModels;
    using Models.PhotoModels;


    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var contests = this.Data.Contests.All()
                .Where(c => c.ClosesOn > DateTime.Now && c.Participants.Any(u => u.Id == loggedUserId))
                .OrderByDescending(c => c.CreatenOn)
                .Take(10)
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
                }).ToList();


            return View(contests);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}