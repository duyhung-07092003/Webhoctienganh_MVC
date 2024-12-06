using Nhom3_BTL_NguyenDuyHung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class LoginAdminController : Controller
    {
        QLTiengAnhEntities db = new QLTiengAnhEntities();
        // GET: LoginAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckLogin(string tk,string mk)
        {
            var ad = db.Admin.FirstOrDefault(b => b.tenTaiKhoan.Contains(tk) && b.matKhau.Contains(mk));
            if (ad != null)
            {
                return RedirectToAction("Index","TrangChuAdmin");
            }
            return View();
        }
    }
}