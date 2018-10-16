using DataModels.EF;
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

      
        // Get List product by category
        public List<Product> ListByCategoryId (long categoryID , ref int totalRecord, int pageIndex=1,int pageSize=2  )
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model= db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x=>x.CreateDate).Skip((pageIndex-1) *pageSize).Take(pageSize).ToList();
            return model;
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
