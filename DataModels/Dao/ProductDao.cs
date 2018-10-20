using DataModels.EF;
using DataModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
     public class ProductDao
    {
        OnlineShopingDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopingDbContext();
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        //this for autocomplete search by jquery
        public List<string> ListName (string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
      
        // Get List product by category
        public List<ProductViewModel> ListByCategoryId (long categoryID , ref int totalRecord, int pageIndex=1,int pageSize=2  )
        {
            //totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            //var model= db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x=>x.CreateDate).Skip((pageIndex-1) *pageSize).Take(pageSize).ToList();
            //return model;


            // this code by productViewModel by LINQ 
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.CategoryID == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();

        }

        //this for autocomplete search by jquery by LINQ
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        //List Feature product
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot >DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProducts(long ProductId)
        {
            var product = db.Products.Find(ProductId);
            return db.Products.Where(x => x.ID != ProductId && x.CategoryID == product.CategoryID).ToList();
        }

        public Product ViewDetails(long id)
        {
            return db.Products.Find(id);
        }
    }
}
