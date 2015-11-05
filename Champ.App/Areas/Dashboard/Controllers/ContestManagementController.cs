namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    using App.Controllers;
    using App.Models.ContestModels;

    [Authorize(Roles = "Admin")]
    public class ContestManagementController : BaseController
    {
        public ActionResult Get()
        {
            var contests = this.Data.Contests.All()
                .OrderByDescending(c => c.CreatenOn)
                .ProjectTo<ContestViewModel>()
                .ToList();

            return this.PartialView("_ShowContestsPartial", contests);
        }

        public ActionResult DismissContest(int id)
        {
            var contestDismissed = this.Data.Contests.Find(id);

            if (contestDismissed == null)
            {
                return HttpNotFound();
            }

            contestDismissed.IsDismissed = true;
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteContest(int id)
        {
            var contestDeleted = this.Data.Contests.Find(id);

            if (contestDeleted == null)
            {
                return HttpNotFound();
            }

            this.Data.Contests.Delete(contestDeleted);
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
	}
}