using Champ.App.Models.UserModels;

namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using Models;
    using App.Controllers;

    [Authorize(Roles = "Admin")]
    public class UsersManagementController : BaseController
    {
        public ActionResult Get()
        {
            var users = this.Data.Users.All()
                .Take(10)
                .Select(UserProfileViewModel.Create)
                .ToList();

            return this.PartialView("_ShowUsersPartial", users);
        }
    }
}