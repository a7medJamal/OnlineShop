using DataModels.Dao;
using DataModels.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CommonDataSet;
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

        public JsonResult DeleteAll()
        {
            Session[CartSession]=null;

            return Json(new
            {
                status = true
            });

        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID==id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });

        }
        public JsonResult Update (string cartModel)
        {
           var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart =(List<CartItem>) Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem !=null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
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

        [HttpGet]
        public ActionResult  Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);

        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address,string email)
        {
            var order = new Order();
            order.CreateDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailsDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.OrderID = id;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                   
                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "New Order From OnlineShop", content);
               new MailHelper().SendMail(toEmail, "New Order From OnlineShop", content);
            }
            catch (Exception)
            {
                return Redirect("/NotSuccess");

            }
            
            return Redirect("/Success");

        }
        public ActionResult Success()
        {
            return View();
        }
    }
}