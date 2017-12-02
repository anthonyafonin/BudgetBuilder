using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetBuilder.Startup))]
namespace BudgetBuilder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
