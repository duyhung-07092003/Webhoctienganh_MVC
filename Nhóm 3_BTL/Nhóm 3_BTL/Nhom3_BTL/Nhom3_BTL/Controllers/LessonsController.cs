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
    public class LessonsController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();

        // GET: Lessons
        public ActionResult Index()
        {
            var lesson = db.Lesson.Include(l => l.Courses);
            return View(lesson.ToList());
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBaiHoc,maKhoaHoc,tieuDe,noiDung,thuTu")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Lesson.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", lesson.maKhoaHoc);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", lesson.maKhoaHoc);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBaiHoc,maKhoaHoc,tieuDe,noiDung,thuTu")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKhoaHoc = new SelectList(db.Courses, "maKhoaHoc", "tenKhoaHoc", lesson.maKhoaHoc);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lesson.Find(id);
            db.Lesson.Remove(lesson);
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
