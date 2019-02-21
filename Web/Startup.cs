using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eTraining.Web.Startup))]
namespace eTraining.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
