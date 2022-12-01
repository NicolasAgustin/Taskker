using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using Taskker.Models.Services;

[assembly: OwinStartupAttribute(typeof(Taskker.Startup))]
namespace Taskker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INotesService, NotesService>();
        }
    }
}
