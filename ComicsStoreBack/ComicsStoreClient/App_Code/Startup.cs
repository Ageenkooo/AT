using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComicsStoreClient.Startup))]
namespace ComicsStoreClient
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
