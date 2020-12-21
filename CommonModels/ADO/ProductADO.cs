using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels.ADO
{
    public class ProductADO
    {
        MyProjectDb db = null;
        public ProductADO()
        {
            db = new MyProjectDb();
        }

        public List<Product> ListNewProducts(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x=>x.Status==true).Take(top).ToList();
        }

        public List<Product> ListHotProducts(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).Take(top).ToList();
        }

        public List<Product> ListRelateProducts(long id)
        {
            var product = db.Products.Find(id);
            return db.Products.Where(x => x.ID != id && x.CategoryID == product.CategoryID).ToList();
        }

        public List<Product> ListAllProducts(ref int totalRecord, int page, int pageSize)
        {
            // return db.Product.OrderByDescending(x => x.CreatedDate).ToPagedList(page,pageSize);
            totalRecord = db.Products.Count();
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x=>x.Status==true).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public Product ProductDetail(long id)
        {
            return db.Products.Find(id);
        }

        public long Update(Product product)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return product.ID;
        }

        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Product> GetAllProducts(ref int totalRecord, int page, int pageSize)
        {
            // return db.Product.OrderByDescending(x => x.CreatedDate).ToPagedList(page,pageSize);
            totalRecord = db.Products.Count();
            return db.Products.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        //sitemap xml
        public List<Product> ListProducts()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Product> GetCateIdByTabProduct(long cateId)
        {
            return db.Products.Where(x => x.CategoryID == cateId).ToList();
        }

        public List<ProductCategoryJoinModel> Search(string keyword, ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from p in db.Products
                         join c in db.ProductCategories
                         on p.CategoryID equals c.ID
                         where p.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = c.MetaTitle,
                             CateName = c.Name,
                             CreatedDate = p.CreatedDate,
                             ID = p.ID,
                             Images = p.Image,
                             Name = p.Name,
                             MetaTitle = p.MetaTitle,
                             Price = p.Price,
                         }).AsEnumerable().Select(x => new ProductCategoryJoinModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                         });
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Product> AdminSearch(string keyword, ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            return db.Products.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public long AddProducts(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }

        public List<ProductCategoryJoinModel>ListByCategoryId(long categoryID, ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.CategoryID == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price,
                             Status = a.Status
                         }).AsEnumerable().Select(x => new ProductCategoryJoinModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,
                             Status = x.Status
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            return model.Where(x => x.Status == true).ToList();
        }


    }
}
