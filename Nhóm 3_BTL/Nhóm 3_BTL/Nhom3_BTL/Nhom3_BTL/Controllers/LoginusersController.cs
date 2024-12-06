using Nhom3_BTL_NguyenDuyHung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class LoginusersController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();
        [HttpGet]
        // GET: Loginusers
        public ActionResult Index()
        {
            ViewBag.Error = TempData["ErrorMessage"];
            ViewBag.FormType = TempData["FormType"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(string phone, string username, string password)
        {
            if (!string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                var user = db.user.FirstOrDefault(m => m.sdt == phone);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Số điện thoại không chính xác. Vui lòng thử lại.";
                    TempData["FormType"] = "phone";
                }
                else
                {
                    Session["user"] = user;
                    return RedirectToAction("Index", "Trangchu");
                }
            }

            else if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && string.IsNullOrEmpty(phone))
            {
                var user = db.user.FirstOrDefault(m => m.tenTaiKhoan == username && m.matKhau == password);
                if (user != null)
                {
                    Session["user"] = user;
                    return RedirectToAction("Index", "Trangchu");
                }
                else
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại.";
                    TempData["FormType"] = "username";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đúng thông tin đăng nhập.";
                TempData["FormType"] = string.IsNullOrEmpty(phone) ? "username" : "phone";
            }

            return RedirectToAction("Index");
        }
    }
}