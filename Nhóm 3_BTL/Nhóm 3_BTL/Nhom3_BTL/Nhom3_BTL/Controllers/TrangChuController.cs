using Nhom3_BTL_NguyenDuyHung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class TrangChuController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();
        // GET: TrangChu
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return HttpNotFound();
            }
            user user = (user)Session["user"];
            return View(user);
        }
    }
}