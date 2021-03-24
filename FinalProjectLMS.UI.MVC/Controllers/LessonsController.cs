using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProjectLMS.DATA.EF;
using FinalProjectLMS.UI.MVC.Utilities;

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

        public ActionResult EmployeeDetails(int? id)
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
        public ActionResult Create([Bind(Include = "LessonID,LessonTitle,CourseID,Introduction,VideoURL,PdfFilename,IsActive")] Lessons lessons, HttpPostedFileBase imgURL)
        {
            if (ModelState.IsValid)
            {
                if (lessons.VideoURL != null)
                {
                    lessons.VideoURL = "https://www.youtube.com/embed/" + lessons.VideoURL.Substring(32);
                }                

                #region File Upload Utility                
                string imgName = "NoImage.PNG";

                if (imgURL != null)
                {

                    imgName = imgURL.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//.ext

                    string[] goodExts = { ".pdf" };

                    if (goodExts.Contains(ext.ToLower()) /*&& (imgURL.ContentLength <= 4194304)*/)
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();
                        string savePath = Server.MapPath("~/Content/assets/img/");
                        imgURL.SaveAs(savePath + imgName);
                        lessons.PdfFilename = imgName;
                    }
                    else
                    {
                        imgName = null;
                        lessons.PdfFilename = null;
                    }
                }
                #endregion

                db.Lessons1.Add(lessons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lessons.CourseID);
            return View(lessons);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "LessonID,LessonTitle,CourseID,Introduction,VideoURL,PdfFilename,IsActive")] Lessons lessons, HttpPostedFileBase imgURL, string videoURL)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        #region File Image Upload Utility

        //        string imgName = "NoImage.PNG";

        //        if (imgURL != null)
        //        {

        //            imgName = imgURL.FileName;

        //            string ext = imgName.Substring(imgName.LastIndexOf('.'));//.ext

        //            string[] goodExts = { ".pdf" };

        //            if (goodExts.Contains(ext.ToLower()) && (imgURL.ContentLength <= 4194304))
        //            {
        //                imgName = Guid.NewGuid() + ext.ToLower();
        //                string savePath = Server.MapPath("~/Content/assets/img/");
        //                imgURL.SaveAs(savePath + imgName);
        //            }
        //            else
        //            {
        //                imgName = "NoImage.PNG";
        //            }
        //        }
        //        lessons.PdfFilename = imgName;
        //        #endregion

        //        #region YouTube Upload Utility

        //        string videoName = "NoImage.PNG";

        //        if (videoURL != null)
        //        {

        //            videoName = videoURL;

        //            //string ext = videoName.Substring(videoName.LastIndexOf('/'));//.ext
        //            string ext = "https://www.youtube.com/";

        //            //string[] goodExts = { ".pdf" };

        //            if (videoURL.Contains(ext.ToLower()))
        //            {
        //                //imgName = Guid.NewGuid() + ext.ToLower();
        //                videoName = ext.ToLower() + "embeded" + videoURL.Substring()
        //                string savePath = Server.MapPath("~/Content/assets/img/");
        //                imgURL.SaveAs(savePath + imgName);
        //            }
        //            else
        //            {
        //                imgName = "NoImage.PNG";
        //            }
        //        }
        //        lessons.PdfFilename = imgName;
        //        #endregion

        //        db.Lessons1.Add(lessons);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", lessons.CourseID);
        //    return View(lessons);
        //}

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
        public ActionResult Edit([Bind(Include = "LessonID,LessonTitle,CourseID,Introduction,VideoURL,PdfFilename,IsActive")] Lessons lessons, HttpPostedFileBase imgURL)
        {
            if (ModelState.IsValid)
            {
                if (lessons.VideoURL != null && lessons.VideoURL.Contains("https://www.youtube.com/watch?v="))
                {
                    lessons.VideoURL = "https://www.youtube.com/embed/" + lessons.VideoURL.Substring(32);
                }
                //if (lessons.VideoURL.Contains("https://www.youtube.com/watch?v="))
                //{
                //    lessons.VideoURL = "https://www.youtube.com/embed/" + lessons.VideoURL.Substring(32);
                //}

                #region File Upload Utility
                string imgName = "NoImage.PNG";

                if (imgURL != null)
                {
                    imgName = imgURL.FileName;

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//.ext

                    string[] goodExts = { ".pdf" };

                    if (goodExts.Contains(ext.ToLower()) /*&& (imgURL.ContentLength <= 4194304)*/)
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();
                        string savePath = Server.MapPath("~/Content/assets/img/");
                        imgURL.SaveAs(savePath + imgName);
                        lessons.PdfFilename = imgName;
                    }
                    else
                    {
                        imgName = null;
                        lessons.PdfFilename = null;
                    }
                }                
                #endregion

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
