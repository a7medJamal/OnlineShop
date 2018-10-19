using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // initilizing culture on controller initialization for add multi language 
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstants.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonConstants.CurrentCulture] = "ar";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ar");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
            }

            
        }

        //changing culture
        public ActionResult ChangeCulture (string ddCulture , string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddCulture);

            Session[CommonConstants.CurrentCulture] = ddCulture;
            return Redirect(returnUrl);
        }



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message ,string type)
        {
            TempData["AlertMessage"]=message;
            if (type =="success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}