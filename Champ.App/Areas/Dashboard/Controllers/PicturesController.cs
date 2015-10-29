using Champ.App.Models.PhotoModels;

namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using App.Controllers;
    using Models;

    [Authorize(Roles = "Admin")]
    public class PicturesController : BaseController
    {
        public ActionResult Get()
        {
            var photos = this.Data.Pictures.All()
                .Take(10)
                .Select(p => new PhotoViewModel
                {
                    Location = p.LocationPath,
                    Author = p.Author.UserName
                })
                .ToList();

            return this.PartialView("_ShowPhotosPartial", photos);
        }
	}
}