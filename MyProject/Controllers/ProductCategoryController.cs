using CommonModels.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Partial_DropCategory()
        {
            var category = new ProductCategoryADO().GetLocationDisplay();
            return PartialView(category);
        }

        public ActionResult ProductsByCategory(long cateId, int page = 1, int pageSize = 4)
        {
            var category = new ProductCategoryADO().ViewDetail(cateId);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductADO().ListByCategoryId(cateId, ref totalRecord, page, pageSize);
            //var model = new ProductCategoryADO().GetInforProductCategoryJoinModel(ref totalRecord, page, pageSize);

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
    }
}