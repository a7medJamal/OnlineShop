using DataModels.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession" ;
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public  ActionResult AddItem( long productId ,int quantity)
        {
            var product = new ProductDao().ViewDetails(productId);
            var cart = Session[CartSession];
            if (cart !=null)
            {
                var List = (List< CartItem>) cart;
                if (List.Exists(x=>x.Product.ID==productId))
                {
                    foreach (var item in List)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    List.Add(item);
                }
                Session[CartSession] = List;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var List = new List<CartItem>();
                List.Add(item);
                Session[CartSession] = List;
            }
            return RedirectToAction("Index");
        }
    }
}