namespace Champ.App.Areas.Dashboard
{
    using System.Web.Mvc;

    public class DashboardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Dashboard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Dashboard_default",
                "Dashboard/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Champ.App.Areas.Dashboard.Controllers" }
            );
        }
    }
}