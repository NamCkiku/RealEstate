using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RealEstate.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "RoomDetail",
              url: "chi-tiet-phong/{alias}-{id}",
              defaults: new { controller = "RoomDetail", id = UrlParameter.Optional, action = "Index" }
            );
            routes.MapRoute(
            name: "Create",
            url: "dang-tin-mien-phi",
            defaults: new { controller = "RoomManage", id = UrlParameter.Optional, action = "Create" }
            );
            routes.MapRoute(
            name: "Map",
            url: "ban-do",
            defaults: new { controller = "Map", id = UrlParameter.Optional, action = "Index" }
            );

            routes.MapRoute(
           name: "Index",
           url: "danh-sach-phong",
           defaults: new { controller = "RoomList", id = UrlParameter.Optional, action = "Index" }
            );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
