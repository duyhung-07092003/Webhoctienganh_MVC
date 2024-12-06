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
    public class GradesController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();

        // GET: Grades
        public ActionResult Index()
        {
            var grade = db.Grade.Include(g => g.test).Include(g => g.user);
            return View(grade.ToList());
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.maBaiKT = new SelectList(db.test, "maBaiKT", "tieuDe");
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDiem,maTaiKhoan,maBaiKT,Diem")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grade.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maBaiKT = new SelectList(db.test, "maBaiKT", "tieuDe", grade.maBaiKT);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", grade.maTaiKhoan);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.maBaiKT = new SelectList(db.test, "maBaiKT", "tieuDe", grade.maBaiKT);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", grade.maTaiKhoan);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDiem,maTaiKhoan,maBaiKT,Diem")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBaiKT = new SelectList(db.test, "maBaiKT", "tieuDe", grade.maBaiKT);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", grade.maTaiKhoan);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grade.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grade.Find(id);
            db.Grade.Remove(grade);
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
