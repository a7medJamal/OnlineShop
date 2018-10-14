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
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);

        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMD5Pass = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMD5Pass;

                }
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Can't add this user");
                }
            }
            return View("Index");

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
                    ModelState.AddModelError("","Can't Edit this user");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete (int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}