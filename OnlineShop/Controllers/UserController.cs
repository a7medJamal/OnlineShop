using BotDetect.Web.Mvc;
using DataModels.Dao;
using DataModels.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var doa = new UserDao();
            var result = doa.Login(model.UserName, Encryptor.MD5Hash(model.Password));
            if (result == 1)
            {
                var user = doa.GetById(model.UserName);
                var userSession = new UserLogin();
                userSession.UserName = user.UserName;
                userSession.UserID = user.ID;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return Redirect("/");
            }
            else if (result == 0)
            {
                ModelState.AddModelError("", "user name  Wrong");
            }
            else if (result == -1)
            {
                ModelState.AddModelError("", "This User Not Acess to Login");
            }
            else if (result == -2)
            {
                ModelState.AddModelError("", " Password Wrong");
            }
            else
            {
                ModelState.AddModelError("", "User name or Password Wrong");

            }

            return View(model);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Enter Number again (correct)!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Your user name Found");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Your Email Found ");

                }
                else
                {
                    var user = new User();
                    user.Name = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreateDate = DateTime.Now;
                    user.Status = true;
                    var result =dao.Insert(user);

                    if (result >0)
                    {
                        ViewBag.Success = " Added Success ";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cant add this user.");

                    }
                }
            }
            return View(model);
        }

    }
}