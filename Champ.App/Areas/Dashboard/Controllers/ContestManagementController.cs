namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using App.Controllers;

    public class ContestManagementController : BaseController
    {

        [Authorize(Roles = "Admin")]
        public ActionResult Show()
        {
            return View();
        }
	}
}