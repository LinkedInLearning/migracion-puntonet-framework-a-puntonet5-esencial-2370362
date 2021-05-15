using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityWithMvc5.Startup))]
namespace IdentityWithMvc5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
