using CommonModels.ADO;
using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using MyProject.CommonS;

namespace MyProject.Controllers
{
    public class UserController : Controller
    {
        //Sign Up
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserADO();
                if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại!");
                }
                else
                {
                    var user = new User();
                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Password = Encryption.MD5Hash(model.Password);
                    user.CreatedDate = DateTime.Now;

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Bạn đã đăng ký thành công!";
                        model = new SignUpModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công!");
                    }
                }
            }
            return View(model);
        }

        //Login
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInModel model)
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
                    Session["User"] = user.ID;
                    Session.Add(CommonConstSession.USER_SESSION, userSession);

                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else if (result == -2)
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

        //Log Out
        public ActionResult SignOut()
        {
            Session[CommonConstSession.USER_SESSION] = null;
            return Redirect("/");
        }

        // User SignIn on header
        public PartialViewResult Partial_HeaderUserSignIn()
        {
            return PartialView();
        }


    }
}