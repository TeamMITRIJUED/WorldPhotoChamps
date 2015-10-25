namespace Champ.App.Controllers
{
    using System;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;

    using Models;
    using Champ.Models;

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
	}
}