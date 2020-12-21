using CommonModels.ADO;
using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            using (MyProjectDb db = new MyProjectDb())
            {
                int totalRecord = 0;
                var model = new ProductCategoryADO().GetAllCategory(ref totalRecord, page, pageSize);
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

                //var dao = new ProductCategoryADO();
                //var model = dao.ListAllPaging(searchString, page, pageSize);
                //ViewBag.SearchString = searchString;
                //ViewBag.GetAllCategory = (from a in db.ProductCategories select a).ToList();
                //return View(model);
            }
        }


        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
            using (MyProjectDb db = new MyProjectDb())
            {
                var query = (from a in db.ProductCategories where a.Name == category.Name select a).ToList();
                if (query.Count >= 1)
                {
                    return Json(new { mess_ = 0 });
                }
                else
                {
                    category.CreatedDate = DateTime.Now;
                    db.ProductCategories.Add(category);
                    db.SaveChanges();
                    return Json(new { mess_ = 1 });
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (MyProjectDb db = new MyProjectDb())
            {
                var query = (from a in db.ProductCategories where a.ID == id select a).FirstOrDefault();
                if (query != null)
                {
                    ProductCategory category = db.ProductCategories.Find(id);
                    db.ProductCategories.Remove(category);
                    db.SaveChanges();
                    return Json(new { mess_ = 1 });
                }
                else
                {
                    return Json(new { mess_ = 0 });
                }
            }
        }

        [HttpPost]
        public ActionResult Update(ProductCategory product)
        {
            using (MyProjectDb db = new MyProjectDb())
            {
                try
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { mess_ = 1 });
                }
                catch (Exception ex)
                {
                    return Json(new { mess_ = 0 });
                }

            }
        }

        public class CategoryProduct
        {
            public long ID { get; set; }
            public string Name { get; set; }
            public bool? Status { get; set; }
            public bool? ShowOnHome { get; set; }
            public string MetaTitle { get; set; }
            public int? DisplayOrder { get; set; }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            using (MyProjectDb db = new MyProjectDb())
            {
                var data = (from a in db.ProductCategories
                            where a.ID == id
                            select new CategoryProduct
                            {
                                ID = a.ID,
                                Name = a.Name,
                                DisplayOrder = a.DisplayOrder,
                                MetaTitle = a.MetaTitle,
                                ShowOnHome = a.ShowOnHome,
                                Status = a.Status
                            }).FirstOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}