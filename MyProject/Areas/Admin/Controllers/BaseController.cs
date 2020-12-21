using MyProject.CommonS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyProject.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonConstSession.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonConstSession.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstSession.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonConstSession.CurrentCulture] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }

        // changing culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session[CommonConstSession.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSignIn)Session[CommonConstSession.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "SignIn", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        // Inform
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
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