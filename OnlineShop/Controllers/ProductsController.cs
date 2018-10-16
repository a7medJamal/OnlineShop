using DataModels.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Category(long catId )
        {
            var category = new CategoryDao().ViewDetail(catId);
            return View(category);
        }

        public ActionResult Datails(long Id)
        {
            var product = new ProductDao().ViewDetails(Id);
            ViewBag.Category = new ProductCategoryDao().ViewDetails(product.CategoryID.Value);
            ViewBag.RealtedProducts = new ProductDao().ListRelatedProducts(Id);
            return View(product);
        }
    }
}