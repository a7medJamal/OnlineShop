using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Product Datails",
                url: "Category/{metatitle}-{Id}",
                defaults: new { controller = "Products", action = "Datails", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
            );


            routes.MapRoute(
           name: "Product Category",
           url: "ProductDetails/{metatitle}-{catId}",
           defaults: new { controller = "Products", action = "Category", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
       );


            routes.MapRoute(
          name: "About",
          url: "Menu/{Link}",
          defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
      );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces:new[] { "OnlineShop.Controllers" }
            );


          
        }
    }
}
