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

        public ActionResult Category(long catId ,int page=2,int pageSize=1 )
        {
            var category = new CategoryDao().ViewDetail(catId);
            ViewBag.Category = category;

            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(catId ,ref totalRecord,page,pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page+1;
            ViewBag.Prev = page - 1;

            return View(model);
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