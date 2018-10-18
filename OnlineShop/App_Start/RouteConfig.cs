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


            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });


            routes.MapRoute(
                name: "Product Datails",
                // url: "chi-tiet/{metatitle}-{id}",
                url: "ProductDetails/{metatitle}-{Id}",
                defaults: new { controller = "Products", action = "Datails", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
            );


            routes.MapRoute(
           name: "Product Category",
           // url: "san-pham/{metatitle}-{cateId}",
           url: "Category/{metatitle}-{catId}",
           defaults: new { controller = "Products", action = "Category", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
       );


            routes.MapRoute(
            name: "About",
            url: "Menu/{Link}",
            defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
        );
            routes.MapRoute(
        name: "Contact",
        url: "Contacts",
        defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
    );
            routes.MapRoute(
            name: "Cart",
            url: "CartItems",
            defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
        );


            routes.MapRoute(
           name: "Payment",
           url: "Payment",
           defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
       );

            routes.MapRoute(
      name: "Register",
      url: "Register",
      defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
  );

            routes.MapRoute(
           name: "Login",
           url: "Login",
           defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
           );

            routes.MapRoute(
          name: "Payment Success",
          url: "Success",
          defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
      );




            routes.MapRoute(
             name: "Add Cart",
             url: "AddToCart",
             defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "OnlineShop.Controllers" }
            );



        }
    }
}
