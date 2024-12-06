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
    public class testsController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();

        // GET: tests
        public ActionResult Index()
        {
            var test = db.test.Include(t => t.Courses);
            return View(test.ToList());
        }

        // GET: tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            test test = db.test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: tests/Create
        public ActionResult Create()
        {
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc");
            return View();
        }

        // POST: tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBaiKT,maKhoaHoc,tieuDe,ngayTao")] test test)
        {
            if (ModelState.IsValid)
            {
                db.test.Add(test);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", test.maKhoaHoc);
            return View(test);
        }

        // GET: tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            test test = db.test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", test.maKhoaHoc);
            return View(test);
        }

        // POST: tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBaiKT,maKhoaHoc,tieuDe,ngayTao")] test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", test.maKhoaHoc);
            return View(test);
        }

        // GET: tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            test test = db.test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            test test = db.test.Find(id);
            db.test.Remove(test);
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
