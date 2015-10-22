using System.Linq;
using Champ.App.Models;
using Microsoft.AspNet.Identity;

namespace Champ.App.Controllers
{
    using System.Web.Mvc;

    public class UsersController : BaseController
    {
        [Authorize]
        public ActionResult ViewProfile()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var userProfile = this.Data.Users.All()
                .Where(u => u.Id == loggedUserId)
                .Select(UserProfileViewModel.Create)
                .FirstOrDefault();
            
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}