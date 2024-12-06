using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom3_BTL_NguyenDuyHung.Models;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class EnrollmentsController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();

        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollment = db.Enrollment.Include(e => e.Courses).Include(e => e.user);
            return View(enrollment.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc");
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maGhiDanh,maTaiKhoan,maKhoaHoc,ngayGhiDanh,tienDo")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollment.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", enrollment.maKhoaHoc);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", enrollment.maTaiKhoan);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", enrollment.maKhoaHoc);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", enrollment.maTaiKhoan);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maGhiDanh,maTaiKhoan,maKhoaHoc,ngayGhiDanh,tienDo")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", enrollment.maKhoaHoc);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", enrollment.maTaiKhoan);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            db.Enrollment.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
