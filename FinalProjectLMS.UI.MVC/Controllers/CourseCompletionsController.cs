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
    public class CourseCompletionsController : Controller
    {
        private FinalProjectLMSEntities db = new FinalProjectLMSEntities();

        // GET: CourseCompletions
        public ActionResult Index()
        {
            var courseCompletions1 = db.CourseCompletions1.Include(c => c.Cours).Include(c => c.UserDetail);
            return View(courseCompletions1.ToList());
        }

        // GET: CourseCompletions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCompletions courseCompletions = db.CourseCompletions1.Find(id);
            if (courseCompletions == null)
            {
                return HttpNotFound();
            }
            return View(courseCompletions);
        }

        // GET: CourseCompletions/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName");
            return View();
        }

        // POST: CourseCompletions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseCompletionID,UserID,CourseID,DateCompleted")] CourseCompletions courseCompletions)
        {
            if (ModelState.IsValid)
            {
                db.CourseCompletions1.Add(courseCompletions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", courseCompletions.CourseID);
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName", courseCompletions.UserID);
            return View(courseCompletions);
        }

        // GET: CourseCompletions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCompletions courseCompletions = db.CourseCompletions1.Find(id);
            if (courseCompletions == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", courseCompletions.CourseID);
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName", courseCompletions.UserID);
            return View(courseCompletions);
        }

        // POST: CourseCompletions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseCompletionID,UserID,CourseID,DateCompleted")] CourseCompletions courseCompletions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseCompletions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", courseCompletions.CourseID);
            ViewBag.UserID = new SelectList(db.UserDetails1, "UserID", "FirstName", courseCompletions.UserID);
            return View(courseCompletions);
        }

        // GET: CourseCompletions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCompletions courseCompletions = db.CourseCompletions1.Find(id);
            if (courseCompletions == null)
            {
                return HttpNotFound();
            }
            return View(courseCompletions);
        }

        // POST: CourseCompletions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseCompletions courseCompletions = db.CourseCompletions1.Find(id);
            db.CourseCompletions1.Remove(courseCompletions);
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
