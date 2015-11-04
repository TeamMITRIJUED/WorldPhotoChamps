namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using App.Controllers;
    using App.Models.PhotoModels;



    [Authorize(Roles = "Admin")]
    public class PicturesController : BaseController
    {
        public ActionResult Get()
        {
            var photos = this.Data.Pictures.All()
                .OrderByDescending(p => p.CreatedOn)
                .Select(p => new PhotoViewModel
                {
                    Id = p.Id,
                    Location = p.LocationPath,
                    Author = p.Author.UserName,
                    Votes = p.Votes.Count,
                    IsDeleted = p.IsDeleted
                })
                .ToList();

            return this.PartialView("_ShowPhotosPartial", photos);
        }

        public ActionResult Remove(int id)
        {
            var removedPhoto = this.Data.Pictures.Find(id);
            removedPhoto.IsDeleted = true;
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
	}
}