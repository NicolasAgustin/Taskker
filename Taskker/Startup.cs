using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Taskker.Startup))]
namespace Taskker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
