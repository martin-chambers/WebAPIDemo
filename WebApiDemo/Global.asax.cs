using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;



using WebApiDemo.Models;

namespace WebApiDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            
            //var container = new Container();
            //container.Register<IValuesRepository, ValuesRepository>(Lifestyle.Transient);

            //DependencyResolver.SetResolver(
                //new SimpleInjectorDependencyResolver(container));

        }
    }
}
