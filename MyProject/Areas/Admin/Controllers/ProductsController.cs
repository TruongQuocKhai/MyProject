using CommonModels.ADO;
using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;


namespace MyProject.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Admin/ProductsList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListProducts(int page =1, int pageSize = 4)
        {
            int totalRecord = 0;
            var model = new ProductADO().GetAllProducts(ref totalRecord, page, pageSize);
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

        public void SetViewBag(long? selectedId = null)
        {
            var model = new ProductCategoryADO();
            ViewBag.CategoryID = new SelectList(model.GetLocationDisplay(), "ID", "Name", selectedId);
        }

        [HttpGet]
        public ActionResult AddProducts()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult AddProducts(Product model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                var result = new ProductADO().AddProducts(model);
                if (result>0)
                {
                    SetAlert("Thêm mới sản phẩm thành công!", "success");
                    return Redirect("ListProducts");
                }
                else
                {
                    SetAlert("Sản phẩm chưa được thêm mới!", "");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult EditProducts(long id)
        {
            using (MyProjectDb db = new MyProjectDb())
            {
                ViewBag.GetDropDown = (from a in db.ProductCategories select a).ToList();
                var product = new ProductADO().ProductDetail(id);
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult EditProducts(Product model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                var result = new ProductADO().Update(model);
                if (result>0)
                {
                    SetAlert("Cập nhật sản phẩm thành công!", "success");
                    return RedirectToAction("ListProducts");
                }
                else
                {
                    SetAlert("Sản phẩm chưa được cập nhật!", "error");
                    return RedirectToAction("EditProducts");
                }
            }
            SetViewBag();
            return View();
        }

        public JsonResult Delete(int id)
        {
            try
            {
                var model = new ProductADO();
                model.Delete(id);
                return Json(new
                {
                    message = 1
                });
            }
            catch
            {
                return Json(new
                {
                    message = 0
                });
            }
           
        }

        public ActionResult ProductsDetail(int id)
        {
            var model = new ProductADO().ProductDetail(id);
            return View(model);
        }
    }
}