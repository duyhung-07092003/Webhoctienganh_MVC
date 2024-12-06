using Nhom3_BTL_NguyenDuyHung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class StudysController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();
        // GET: Studys
        public ActionResult Index()
        {
            user user = (user)Session["user"];
            if (user == null)
            {
                return RedirectToAction("Index", "Loginusers");
            }
            ViewBag.Name = user.hoTen;
            var khoahoc = db.Enrollment
                .Where(e => e.maTaiKhoan == user.maTaiKhoan)
                .Select(e => new KhoaHocViewModel
                {
                    MaKhoaHoc = e.Courses.maKhoaHoc,
                    TenKhoaHoc = e.Courses.tenKhoaHoc,
                    TienDo = e.tienDo ?? 0,
                    TotalLessons = e.Courses.Lesson.Count
                });
            var baidau = db.Enrollment.FirstOrDefault(e => e.maTaiKhoan == user.maTaiKhoan);
            var td = baidau.maKhoaHoc;
            Courses bai = (Courses)db.Courses.FirstOrDefault(m => m.maKhoaHoc == td);
            ViewBag.bai = bai;
            ViewBag.sb = db.Lesson.Count(m => m.maKhoaHoc == bai.maKhoaHoc);
            return View(khoahoc.ToList());
        }
    }
}