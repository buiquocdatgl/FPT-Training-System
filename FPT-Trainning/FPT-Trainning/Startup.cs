using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPT_Trainning.Startup))]
namespace FPT_Trainning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
