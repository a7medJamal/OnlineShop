using DataModels.Dao;
using DataModels.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page =1, int pagSize=10)
        {
            var dao = new UserDao();
            var model = dao.listAllPaging(page, pagSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMD5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMD5Pass;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("","Cant add this user");
                }
            }
            return View("Index");
         
        }
    }
}