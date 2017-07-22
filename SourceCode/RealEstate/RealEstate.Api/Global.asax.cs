﻿using RealEstate.Api.Mappings;
using RealEstate.Api.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RealEstate.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfiguration.Configure();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
            JobScheduler.Start();
        }
        protected void Application_PreSendRequestHeaders()

        {

            Response.Headers.Remove("Server");           //Remove Server Header  

            Response.Headers.Remove("X-AspNet-Version"); //Remove X-AspNet-Version Header

        }

    }
}
