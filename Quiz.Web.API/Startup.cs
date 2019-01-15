using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Quiz.Web.API.Startup))]

namespace Quiz.Web.API
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
      ConfigureAuth(app);
    }
  }
}
