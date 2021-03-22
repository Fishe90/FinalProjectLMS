using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProjectLMS.DATA.EF;

namespace FinalProjectLMS.UI.MVC.Controllers
{
    public class LessonViewsController : Controller
    {
        private FinalProjectLMSEntities db = new FinalProjectLMSEntities();

        // GET: LessonViews
        public ActionResult Index()
        {
            var lessonViews1 = db.LessonViews1.Include(l => l.Lesson).Include(l => l.UserDetail);
            return View(lessonViews1.ToList());
        }

        // GET: LessonViews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonViews lessonViews = db.LessonViews1.Find(id);
            if (lessonViews == null)
            {
                return HttpNotFound();
            }
            return View(lessonViews);
        }

        // GET: LessonViews/Create
        public ActionResult Create()
        {
            ViewBag.LessonID = new SelectList(db.Lessons1, "LessonID", "LessonTitle");
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName");
            return View();
        }

        // POST: LessonViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonViewsID,UserID,LessonID,DateViewed")] LessonViews lessonViews)
        {
            if (ModelState.IsValid)
            {
                db.LessonViews1.Add(lessonViews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessonID = new SelectList(db.Lessons1, "LessonID", "LessonTitle", lessonViews.LessonID);
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName", lessonViews.UserID);
            return View(lessonViews);
        }

        // GET: LessonViews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonViews lessonViews = db.LessonViews1.Find(id);
            if (lessonViews == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessonID = new SelectList(db.Lessons1, "LessonID", "LessonTitle", lessonViews.LessonID);
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName", lessonViews.UserID);
            return View(lessonViews);
        }

        // POST: LessonViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonViewsID,UserID,LessonID,DateViewed")] LessonViews lessonViews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessonViews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessonID = new SelectList(db.Lessons1, "LessonID", "LessonTitle", lessonViews.LessonID);
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName", lessonViews.UserID);
            return View(lessonViews);
        }

        // GET: LessonViews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LessonViews lessonViews = db.LessonViews1.Find(id);
            if (lessonViews == null)
            {
                return HttpNotFound();
            }
            return View(lessonViews);
        }

        // POST: LessonViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonViews lessonViews = db.LessonViews1.Find(id);
            db.LessonViews1.Remove(lessonViews);
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
