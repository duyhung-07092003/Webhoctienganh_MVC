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
    public class AnswersController : Controller
    {
        private QLTiengAnhEntities db = new QLTiengAnhEntities();

        // GET: Answers
        public ActionResult Index()
        {
            var answer = db.Answer.Include(a => a.question).Include(a => a.user);
            return View(answer.ToList());
        }

        // GET: Answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            ViewBag.maCauHoi = new SelectList(db.question, "maCauHoi", "cauHoi");
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maTraLoi,maCauHoi,maTaiKhoan,traLoi,kiemTra")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answer.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maCauHoi = new SelectList(db.question, "maCauHoi", "cauHoi", answer.maCauHoi);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", answer.maTaiKhoan);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.maCauHoi = new SelectList(db.question, "maCauHoi", "cauHoi", answer.maCauHoi);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", answer.maTaiKhoan);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maTraLoi,maCauHoi,maTaiKhoan,traLoi,kiemTra")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maCauHoi = new SelectList(db.question, "maCauHoi", "cauHoi", answer.maCauHoi);
            ViewBag.maTaiKhoan = new SelectList(db.user, "maTaiKhoan", "tenTaiKhoan", answer.maTaiKhoan);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answer.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answer.Find(id);
            db.Answer.Remove(answer);
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
