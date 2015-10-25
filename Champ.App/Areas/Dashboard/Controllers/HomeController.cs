namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}