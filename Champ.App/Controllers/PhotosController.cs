namespace Champ.App.Controllers
{
    using System;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using System.Linq;

    using Champ.Models;
    using Models.PhotoModels;

    public class PhotosController : BaseController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Add(PhotoViewModel model)
        {
            if (!this.ModelState.IsValid || model == null)
            {
                return RedirectToAction("ViewContests", "Contest");
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var contest = this.Data.Contests.Find(model.ContestId);

            var photo = new Picture()
            {
                AuthorId = loggedUserId,
                ContestId = model.ContestId,
                CreatedOn = DateTime.Now,
                LocationPath = model.Location
            };

            this.Data.Pictures.Add(photo);
            contest.Pictures.Add(photo);
            this.Data.SaveChanges();

            return RedirectToAction("ViewContests", "Contest");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetPhotos()
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var ownPhotos = this.Data.Pictures.All()
                .OrderByDescending(p => p.Votes.Count)
                .Where(p => p.Author.Id == loggedUserId)
                .Take(10)
                .Select(p => new PhotoViewModel
                {
                    Location = p.LocationPath,
                    Author = p.Author.UserName
                })
                .ToList();

            return View(ownPhotos);
        }
	}
}