using Nhom3_BTL_NguyenDuyHung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class StudyplantController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();
        // GET: Studyplant
        public ActionResult Index(int? mabai)
        {
            user user = (user)Session["user"];
            if (mabai == null)
            {
                return HttpNotFound();
            }
            var baihoc = db.Lesson.Where(m => m.maKhoaHoc == mabai);
            var khoahoc = db.Courses.FirstOrDefault(m => m.maKhoaHoc == mabai);
            ViewBag.ghidanh = db.Enrollment.FirstOrDefault(m => m.maTaiKhoan == user.maTaiKhoan && m.maKhoaHoc == khoahoc.maKhoaHoc);
            ViewBag.Name = khoahoc.tenKhoaHoc;
            return View(baihoc.ToList());
        }
    }
}