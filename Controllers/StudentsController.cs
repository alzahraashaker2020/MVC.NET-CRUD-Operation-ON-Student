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
    public class StudentsController : Controller
    {
        private ModelLab db = new ModelLab();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DeptNo = new SelectList(db.Departments, "DeptNo", "DeptName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Student student, HttpPostedFileBase  PhotoName)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                if (PhotoName != null)
                {
                    string fileName = student.Id.ToString() + "." + PhotoName.FileName.Split('.')[1];
                    PhotoName.SaveAs(Server.MapPath("~/images/" + fileName));
                    student.photoName = fileName;
                }


                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptNo = new SelectList(db.Departments, "DeptNo", "DeptName", student.DeptNo);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptNo = new SelectList(db.Departments, "DeptNo", "DeptName", student.DeptNo);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Uname,Password,Email,DeptNo")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptNo = new SelectList(db.Departments, "DeptNo", "DeptName", student.DeptNo);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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



        /////////studentCourse////////////

        public ActionResult AddDegree(int id)
        {
            var Std = db.Students.Find(id);
            var stdDept = Std.DeptNo;
            var CursInDept = db.DepartmentCourses.Where(m => m.DeptNo == id).Select(a => a.Course).ToList();
            var courseWithDegree = db.StudentCourses.Where(a => a.StId == id && (a.Degree != 0)).Select(m => m.Course);
            var courseWithNoDegree = CursInDept.Except(courseWithDegree).ToList();
            return View(courseWithNoDegree);


            //var allCourses = db.Courses.ToList();
            //var courseWithDegree = db.StudentCourses.Where(a => a.StId == id && (a.Degree != 0)).Select(m => m.Course);
            //var courseWithNoDegree = allCourses.Except(courseWithDegree).ToList();
            //ViewBag.deg = db.Courses.FirstOrDefault(a => a.CrsId == id);


            //return View(courseWithNoDegree);
        }
        [HttpPost]
        public ActionResult AddDegree(int id, Dictionary<string, int> deg)
        {
            foreach (var item in deg)
            {
                if (item.Value >0)
                {
                    db.StudentCourses.Add(new StudentCourse() { StId = id, CrsId = int.Parse(item.Key),Degree=item.Value });
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
                    var deptCourse = db.DepartmentCourses.FirstOrDefault(a => a.CrsId == id && a.DeptNo == depid);
                    db.DepartmentCourses.Remove(deptCourse);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
       //////////////////////EditDegree///////////////////
        public ActionResult EditDegree(int id)
        {
            var allCourses = db.Courses.ToList();
            var courseWithDegree = db.StudentCourses.Where(a => a.StId == id && (a.Degree != 0)).Select(m => m.Course);
            var courseWithNoDegree = allCourses.Except(courseWithDegree).ToList();
            //ViewBag.deg = db.Courses.FirstOrDefault(a => a.CrsId == id);


            return View(courseWithDegree);
        }
        [HttpPost]
        public ActionResult EditDegree(int id, Dictionary<string, int> deg)
        {
            foreach (var item in deg)
            {
                if (item.Value > 0)
                {
                    //db.StudentCourses.Add(new StudentCourse() { StId = id, CrsId = int.Parse(item.Key), Degree = item.Value });
                    var stdcrs=db.StudentCourses.Find(int.Parse(item.Key));
                    stdcrs.Degree = item.Value;
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //////////////////ADD Course to Student /////////////////


        public ActionResult AddCourse(int id)
        {
            var allcrs = db.Courses.ToList();
            var DeptCrs = db.StudentCourses.Where(p => p.StId == id).Select(p => p.Course).ToList();
            var crsnotinDept = allcrs.Except(DeptCrs).ToList();
            ViewBag.dept = db.Students.FirstOrDefault(p => p.Id == id);

            return View(crsnotinDept);
        }
        [HttpPost]
        public ActionResult AddCourse(int id, Dictionary<string, bool> crs)
        {
            //KeyValuePair<string, bool>
            foreach (var item in crs)
            {
                if (item.Value == true)
                {
                    db.StudentCourses.Add(new StudentCourse() { StId = id, CrsId = int.Parse(item.Key) });
                }
            }

            db.SaveChanges();
            return RedirectToAction("index");
        }
        /////////////////////////Remove Course/////////////////////
        public ActionResult RemoveCourse(int id)
        {

            var DeptCrs = db.StudentCourses.Where(p => p.StId == id).Select(p => p.Course).ToList();

            ViewBag.dept = db.Students.FirstOrDefault(p => p.Id == id);

            return View(DeptCrs);
        }
        [HttpPost]
        public ActionResult RemoveCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (KeyValuePair<string, bool> item in crs)
            {
                if (item.Value == true)
                {
                    int x = int.Parse(item.Key);
                    var crsDelete = db.StudentCourses.FirstOrDefault(p => p.StId == id && p.CrsId == x);
                    db.StudentCourses.Remove(crsDelete);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
