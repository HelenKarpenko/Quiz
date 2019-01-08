using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Quiz.Web.API
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      HttpConfiguration config = GlobalConfiguration.Configuration;
      config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
    }

    //protected void Application_BeginRequest(object sender, EventArgs e)
    //{
    //  if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
    //  {
    //    HttpContext.Current.Response.Flush();
    //  }
    //}
  }
}
