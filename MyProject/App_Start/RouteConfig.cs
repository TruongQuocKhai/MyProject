using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
         name: "",
         url: "rss",
         defaults: new { controller = "Blog", action = "ReadRSS" },
          namespaces: new[] { "MyProject.Controllers" }
     );

            //         routes.MapRoute(
            //    name: "ContentCategory",
            //    url: "rss",
            //    defaults: new { controller = "Blog", action = "Index" },
            //     namespaces: new[] { "MyProject.Controllers" }
            //);

            routes.MapRoute(
           name: "PostFeed",
           url: "Feed/{type}",
           defaults: new { controller = "Blog", action = "PostFeed", type = "" },
            namespaces: new[] { "MyProject.Controllers" }
       );

            routes.MapRoute(
            name: "Search",
            url: "tim-kiem",
            defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
             namespaces: new[] { "MyProject.Controllers" }
        );

            routes.MapRoute(
             name: "View all product",
             url: "tat-ca-san-pham",
             defaults: new { controller = "Product", action = "AllProducts", id = UrlParameter.Optional },
              namespaces: new[] { "MyProject.Controllers" }
         );

            routes.MapRoute(
            name: "Products by category",
            url: "san-pham/{metatitle}-{cateId}",
            defaults: new { controller = "ProductCategory", action = "ProductsByCategory", id = UrlParameter.Optional },
             namespaces: new[] { "MyProject.Controllers" }
        );

            routes.MapRoute(
             name: "SignUp",
             url: "dang-ky",
             defaults: new { controller = "User", action = "SignUp", id = UrlParameter.Optional },
              namespaces: new[] { "MyProject.Controllers" }
         );

            routes.MapRoute(
              name: "SignIn",
              url: "dang-nhap",
              defaults: new { controller = "User", action = "SignIn", id = UrlParameter.Optional },
               namespaces: new[] { "MyProject.Controllers" }
          );

            routes.MapRoute(
            name: "SignOut",
            url: "dang-xuat",
            defaults: new { controller = "User", action = "SignOut", id = UrlParameter.Optional },
             namespaces: new[] { "MyProject.Controllers" }
        );

            routes.MapRoute(
              name: "Order notification successful ",
              url: "thanh-cong",
              defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
               namespaces: new[] { "MyProject.Controllers" }
          );

            routes.MapRoute(
              name: "Add to cart",
              url: "them-vao-gio-hang",
              defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
               namespaces: new[] { "MyProject.Controllers" }
          );

            routes.MapRoute(
             name: "Order",
             url: "dat-hang",
             defaults: new { controller = "Cart", action = "Order", id = UrlParameter.Optional },
              namespaces: new[] { "MyProject.Controllers" }
         );

            routes.MapRoute(
             name: "Cart",
             url: "gio-hang",
             defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "MyProject.Controllers" }
         );

            routes.MapRoute(
               name: "Product Detail",
               url: "{chi-tiet}/{metatitle}-{id}",
               defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
                namespaces: new[] { "MyProject.Controllers" }
           );

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyProject.Controllers" }
            );
        }
    }
}
