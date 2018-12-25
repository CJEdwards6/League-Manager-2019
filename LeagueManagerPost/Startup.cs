using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeagueManagerPost.Startup))]
namespace LeagueManagerPost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
