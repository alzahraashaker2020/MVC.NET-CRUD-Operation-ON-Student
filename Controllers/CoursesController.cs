using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestLab6.Models;

namespace TestLab6.Controllers
{
    public class CoursesController : Controller
    {
        private ModelLab db = new ModelLab();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CrsId,CrsName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CrsId,CrsName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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


        ////////////DepatmentCourse/////////////
        public ActionResult AddDepartment(int id)
        {
            var allDepartment = db.Departments.ToList();
            var Depttakecourse = db.DepartmentCourses.Where(a => a.CrsId == id).Select(m => m.Department);
            var DeptNottakecourse = allDepartment.Except(Depttakecourse).ToList();
            ViewBag.crs = db.Courses.FirstOrDefault(a => a.CrsId == id);


            return View(DeptNottakecourse);
        }
        [HttpPost]
        public ActionResult AddDepartment(int id, Dictionary<string, bool> dep)
        {
            foreach (var item in dep)
            {
                if (item.Value == true)
                {
                    db.DepartmentCourses.Add(new DepartmentCourse() { CrsId = id, DeptNo = int.Parse(item.Key) });
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        //Remove Course

        public ActionResult RemoveDepartment(int id)
        {
            var Depttakecourse = db.DepartmentCourses.Where(a => a.CrsId == id).Select(m => m.Department);
            //ViewBag.dept = db.Departments.FirstOrDefault(a => a.DeptNo == id);
            return View(Depttakecourse);
        }
        [HttpPost]
        public ActionResult RemoveDepartment(int id, Dictionary<string, bool> dep)
        {
            foreach (var item in dep)
            {
                if (item.Value == true)
                {
                    int depid = int.Parse(item.Key);
                    var deptCourse = db.DepartmentCourses.FirstOrDefault(a => a.CrsId  == id && a.DeptNo == depid);
                    db.DepartmentCourses.Remove(deptCourse);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
