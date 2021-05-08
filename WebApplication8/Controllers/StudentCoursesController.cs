using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Authorize]
    public class StudentCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentCourses
        public ActionResult Index(string id)
        {

            var std = db.Students.FirstOrDefault(a => a.Id == id);
            ViewBag.Std = std;
            var studentCourses = db.StudentCourses.Include(s => s.Course).Include(s => s.Student).Where(a => a.StdId == id);
            return View(studentCourses.ToList());
        }

        public ActionResult Mygrade()
        {
            var id = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).Id;
            var std = db.Students.FirstOrDefault(a => a.Id == id);
            ViewBag.Std = std;
            var studentCourses = db.StudentCourses.Include(s => s.Course).Include(s => s.Student).Where(a => a.StdId == id);
            return View("index",studentCourses.ToList());
        }
        // GET: StudentCourses/Create
        public ActionResult Create(string id)
        {
            ViewBag.stdId = id;
            var Deptid = db.Students.FirstOrDefault(a => a.Id == id).dId;
            var StudentCourses = db.DepartmentCourses.Where(a => a.DeptId == Deptid).Select(a => a.Course);
            var studentGrade = db.StudentCourses.Where(s => s.StdId == id).Select(c => c.Course);
            var studentCourseNotGrade = StudentCourses.Except(studentGrade).ToList();
            ViewBag.CourseId = new SelectList(studentCourseNotGrade, "CId", "CName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                var StdCourse = db.StudentCourses.FirstOrDefault(a => a.CourseId == studentCourse.CourseId && a.StdId == studentCourse.StdId);
                db.StudentCourses.Add(studentCourse);
                db.SaveChanges();
                return RedirectToAction("Index",new { id = studentCourse.StdId});
            }
            var Deptid = db.Students.FirstOrDefault(a => a.Id == studentCourse.StdId).dId;
            var StudentCourses = db.DepartmentCourses.Where(a => a.DeptId == Deptid).Select(a => a.Course);
            var studentGrade = db.StudentCourses.Where(s => s.StdId == studentCourse.StdId).Select(c => c.Course);
            var studentCourseNotGrade = StudentCourses.Except(studentGrade).ToList();
            ViewBag.CourseId = new SelectList(studentCourseNotGrade, "CId", "CName");
            return View(studentCourse);
        }

        // GET: StudentCourses/Edit/1,5
        public ActionResult Edit(string StId, int? CrsId)
        {
            ViewBag.StdIdcurent = StId;
            if (StId == null || CrsId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCourse studentCourse = db.StudentCourses.FirstOrDefault(a => a.StdId == StId && a.CourseId == CrsId);
            if (studentCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CId", "CName", studentCourse.CourseId);
            ViewBag.StdId = new SelectList(db.Students, "Id", "Name", studentCourse.StdId);
            return View(studentCourse);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = studentCourse.StdId });
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CId", "CName", studentCourse.CourseId);
            ViewBag.StdId = new SelectList(db.Students, "Id", "Name", studentCourse.StdId);
            return View(studentCourse);
        }

        // GET: StudentCourses/Delete/5
        // GET: StudentCourses/Delete/5,1
        public ActionResult Delete(string StId, int? CrsId)
        {
            if (StId == null || CrsId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCourse studentCourse = db.StudentCourses.FirstOrDefault(a => a.StdId == StId && a.CourseId == CrsId);
            if (studentCourse == null)
            {
                return HttpNotFound();
            }
            return View(studentCourse);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string StId, int? CrsId)
        {
            StudentCourse studentCourse = db.StudentCourses.FirstOrDefault(a => a.StdId == StId && a.CourseId == CrsId);
            db.StudentCourses.Remove(studentCourse);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = StId });
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
