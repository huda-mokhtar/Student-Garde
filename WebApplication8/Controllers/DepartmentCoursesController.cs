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
    public class DepartmentCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DepartmentCourses
        public ActionResult Index(int id)
        {
            var dept = db.Departments.FirstOrDefault(a => a.dId == id);
            ViewBag.dept = id;
            var departmentCourses = db.DepartmentCourses.Include(d => d.Course).Include(d => d.Department).Where(d=>d.DeptId==id);
            return View(departmentCourses.ToList());
        }

        // GET: DepartmentCourses/Create
        public ActionResult Create(int id)
        {
            ViewBag.DeptId = id;
            var DepartmentCourses = db.DepartmentCourses.Where(a => a.DeptId == id).Select(a => a.Course);
            var AllCourses = db.Courses.ToList();
            var CoursesNotdepartment = AllCourses.Except(DepartmentCourses).ToList();
            ViewBag.CourseId = new SelectList(CoursesNotdepartment, "CId", "CName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DepartmentCourse departmentCourse)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentCourses.Add(departmentCourse);
                db.SaveChanges();
                return RedirectToAction("Index",new { id= departmentCourse .DeptId});
            }

            var DepartmentCourses = db.DepartmentCourses.Where(a => a.DeptId == departmentCourse.DeptId).Select(a => a.Course);
            var AllCourses = db.Courses.ToList();
            var CoursesNotdepartment = AllCourses.Except(DepartmentCourses).ToList();
            ViewBag.CourseId = new SelectList(CoursesNotdepartment, "CId", "CName");
            return View(departmentCourse);
        }

        // GET: DepartmentCourses/Delete/5
        public ActionResult Delete(int? DeptId, int? CourseId)
        {
            if (DeptId == null || CourseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentCourse departmentCourse = db.DepartmentCourses.FirstOrDefault(c => c.DeptId == DeptId && c.CourseId == CourseId);
            if (departmentCourse == null)
            {
                return HttpNotFound();
            }
            return View(departmentCourse);
        }

        // POST: DepartmentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? DeptId, int? CourseId)
        {
            DepartmentCourse departmentCourse = db.DepartmentCourses.FirstOrDefault(c => c.DeptId == DeptId && c.CourseId == CourseId);
            db.DepartmentCourses.Remove(departmentCourse);
            db.SaveChanges();
            return RedirectToAction("Index",new {id=DeptId });
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
