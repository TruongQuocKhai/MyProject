using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonModels.ADO
{
    public class ProductCategoryADO
    {
        MyProjectDb db = null;
        public ProductCategoryADO()
        {
            db = new MyProjectDb();
        }

        public List<ProductCategory> GetCategory()
        {
            return db.ProductCategories.ToList();
        }

        public ProductCategory ViewDetail(long cateId)
        {
            return db.ProductCategories.Find(cateId);
        }

        

        public List<ProductCategoryJoinModel> GetInforProductCategoryJoinModel(ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.Products.Count();
            var model = (from p in db.Products
                         join c in db.ProductCategories
                         on p.CategoryID equals c.ID
                         select new
                         {
                             CateName = c.Name,
                             CateMetaTitle = c.MetaTitle,
                             Name = p.Name,
                             ID = p.ID,
                             Images = p.Image,
                             MetaTitle = p.MetaTitle,
                             Price = p.Price,
                             CreatedDate = p.CreatedDate,
                             Status = p.Status,
                         }).AsEnumerable().Select(x => new ProductCategoryJoinModel()
                         {
                             CateName = x.Name,
                             CateMetaTitle = x.MetaTitle,
                             Name = x.Name,
                             ID = x.ID,
                             Images = x.Images,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             CreatedDate = x.CreatedDate,
                             Status = x.Status,
                         });
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
        }

        public List<ProductCategory> GetLocationDisplay()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<ProductCategory> GetAllCategory(ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.Products.Count();
            return db.ProductCategories.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}