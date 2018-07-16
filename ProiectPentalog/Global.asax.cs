﻿using ProiectPentalog.Database;
using ProiectPentalog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProiectPentalog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            using (RoomsDbContext ctx = new RoomsDbContext())
            {
                ctx.Rooms.Add(new Room() { Name = "Test" });
                ctx.SaveChanges();
            }

            using (RoomsDbContext ctx = new RoomsDbContext())
            {
                int x = ctx.Rooms.Count();
                int y = 0;
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}