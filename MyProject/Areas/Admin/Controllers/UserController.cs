using CommonModels.ADO;
using CommonModels.EF;
using MyProject.Areas.Admin.Models;
using MyProject.CommonS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(int page = 1, int pageSize = 4)
        {
            int totalRecord = 0;
            var model = new UserADO().GetListUsers(ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 4;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult AddUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var ado = new UserADO();
                if (ado.CheckEmail(userModel.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else
                {
                    var encryptedMd5Pas = Encryption.MD5Hash(userModel.Password);
                    userModel.Password = encryptedMd5Pas;
                    var user = new User();
                    user.Name = userModel.Name;
                    user.Password = userModel.Password;
                    user.Phone = userModel.Phone;
                    user.Address = userModel.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = userModel.Status;

                    long id = ado.Insert(user);
                    if (id > 0)
                    {
                        SetAlert("Thêm user thành công!", "success");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm user không thành công!");
                    }
                }
            }
            SetAlert("Thêm user không thành công!", "error");
            return View(userModel);
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult EditUser(int id)
        {
            var ado = new UserADO().ViewDetail(id);
            return View(ado);
        }

        [HttpPost]
        [HasCredential(RoleID ="EDIT_USER")]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var ado = new UserADO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryption.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }
                var result = ado.Update(user);
                // resutl == true update success
                if (result)
                {
                    SetAlert("Cập nhật User thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại!");
                }
            }
            return View("Index");
        }

        [HasCredential(RoleID = "DELETE_USER")]
        public JsonResult Delete(int id)
        {
            try
            {
                var user = new UserADO();
                user.DeleteUser(id);
                return Json(new
                {
                    message = 1
                });
            }
            catch (Exception)
            {

                return Json(new
                {
                    message = 0
                });
            }
        }

        public ActionResult SignOut()
        {
            Session["USER_SESSION"] = null;
            return Redirect("/Admin/SignIn/");
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserADO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}