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
    public class LessonsController : Controller
    {
        private FinalProjectLMSEntities db = new FinalProjectLMSEntities();

        // GET: Lessons
        public ActionResult Index()
        {
            var lessons1 = db.Lessons1.Include(l => l.Cours);
            return View(lessons1.ToList());
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons1.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonID,LessonTitle,CourseID,Introduction,VideoURL,PdfFilename,IsActive")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Lessons1.Add(lessons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lessons.CourseID);
            return View(lessons);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons1.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lessons.CourseID);
            return View(lessons);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonID,LessonTitle,CourseID,Introduction,VideoURL,PdfFilename,IsActive")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lessons.CourseID);
            return View(lessons);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons1.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lessons lessons = db.Lessons1.Find(id);
            db.Lessons1.Remove(lessons);
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
