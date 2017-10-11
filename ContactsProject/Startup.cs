using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactsProject.Startup))]
namespace ContactsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
        }
    }
}
