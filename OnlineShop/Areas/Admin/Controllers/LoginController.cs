using DataModels.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var doa = new UserDao();
                var result = doa.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result==1)
                {
                    var user = doa.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result== 0)
                {
                    ModelState.AddModelError("", "user name  Wrong");
                }
                else if(result== -1)
                {
                    ModelState.AddModelError("", "This User Not Acess to Login");
                }
                else if(result== -2)
                {
                    ModelState.AddModelError("", " Password Wrong");
                }
                else
                {
                    ModelState.AddModelError("", "User name or Password Wrong");

                }
            }
            return View("Index");
        }
    }
}