using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using Weterzynarze.DAL;

[assembly: OwinStartupAttribute(typeof(Weterzynarze.Startup))]
namespace Weterzynarze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new DataInitializer());
        }
    }
}
