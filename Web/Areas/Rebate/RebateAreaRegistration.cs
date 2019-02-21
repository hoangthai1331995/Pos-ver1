using System.Web.Mvc;

namespace eTraining.Web.Areas.Rebate
{
    public class RebateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Rebate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Rebate_default",
               "Rebate/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "Rebate.Controllers" }
           );
        }
    }
}