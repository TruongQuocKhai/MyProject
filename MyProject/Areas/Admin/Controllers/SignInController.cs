using MyProject.Areas.Admin.Models;
using MyProject.CommonS;
using CommonModels.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Areas.Admin.Controllers
{
    public class SignInController : Controller
    {
        // GET: Admin/SignIn
        public ActionResult Index(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var ado = new UserADO();
                var result = ado.SignIn(model.Email, Encryption.MD5Hash(model.Password));

                if (result == 1)
                {
                    var user = ado.GetByEmail(model.Email);
                    var userSession = new UserSignIn();
                    userSession.UserEmail = user.Email;
                    userSession.UserName = user.Name;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    Session["User"] = user.ID;
                    var listCredentials = ado.GetListCredential(model.Email);

                    Session.Add(CommonConstSession.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstSession.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else if(result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!");
                }
            }
            return View();
        }
    }
}