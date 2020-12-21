using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonModels.ADO;

namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetail(long id)
        {
            var product = new ProductADO().ProductDetail(id);
            var relateproduct = new ProductADO().ListRelateProducts(id);
            return View(product);
        }

        [ChildActionOnly]
        public PartialViewResult Partial_ListNewProducts()
        {
            ViewBag.ListNewProducts = new ProductADO().ListNewProducts(8);
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Partial_ListHotProducts()
        {
            ViewBag.ListHotProducts = new ProductADO().ListHotProducts(8);
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Partial_ListRelateProducts(long id)
        {
            ViewBag.ListRelateProducts = new ProductADO().ListRelateProducts(id);
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Partial_ListSuggestionsProducts()
        {
            ViewBag.ListSuggestionsProducts = new ProductADO().ListNewProducts(8);
            return PartialView();
        }

        public ActionResult AllProducts(int page = 1, int pagesize = 4)
            {
            
            int totalRecord = 0;
            var model = new ProductADO().ListAllProducts(ref totalRecord, page, pagesize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 4;
            int totalPage = 0; 
            totalPage = (int)Math.Ceiling((double)(totalRecord / pagesize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 4)
        {
            int totalRecord = 0;
            var model = new ProductADO().Search(keyword, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;

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