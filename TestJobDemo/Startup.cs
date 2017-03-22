using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestJobDemo.Startup))]
namespace TestJobDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
