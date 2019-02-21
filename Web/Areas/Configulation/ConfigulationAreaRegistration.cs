using System.Web.Mvc;

namespace eTraining.Web.Areas.Tests
{
    public class ConfigulationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Configulation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Configulation_default",
               "Configulation/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "Configulation.Controllers" }
           );
        }
    }
}