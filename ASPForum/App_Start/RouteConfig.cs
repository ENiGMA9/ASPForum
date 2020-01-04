﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPForum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "ShowSubject",
                url: "Subject/Show/{categoryId}/{subjectId}",
                defaults: new { controller = "Subject", action = "Show", categoryid = 0, subjectId = 0}
            );

            routes.MapRoute(
                name: "ShowThread",
                url: "Thread/Show/{categoryId}/{subjectId}/{threadId}",
                defaults: new { controller = "Thread", action = "Show", categoryid = 0, subjectId = 0, threadId = 0}
            );

            routes.MapRoute(
                name: "ExtraId",
                url: "{controller}/{action}/{id}/{id2}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
