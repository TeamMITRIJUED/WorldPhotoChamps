namespace Champ.App.Areas.Dashboard.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    using Models;
    using App.Controllers;

    [Authorize(Roles = "Admin")]
    public class ContestManagementController : BaseController
    {
        public ActionResult Get()
        {
            var contests = this.Data.Contests.All()
                .Where(c => c.ClosesOn > DateTime.Now)
                .OrderByDescending(c => c.ClosesOn)
                .Take(10)
                .ProjectTo<ContestViewModel>()
                .ToList();

            return this.PartialView("_ShowContestsPartial", contests);
        }
	}
}