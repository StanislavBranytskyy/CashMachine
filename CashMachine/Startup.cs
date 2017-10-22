using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CashMachine.Startup))]
namespace CashMachine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
