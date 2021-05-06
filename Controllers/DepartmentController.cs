using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestLab6.Models;

namespace TestLab6.Controllers
{
    public class DepartmentController : Controller
    {
        private ModelLab db = new ModelLab();
        // GET: Department
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(dept);
                db.SaveChanges();
                return RedirectToAction("index");

            }
            return View(dept);
        }
        public ActionResult Ditails(int id)
        {
           Department Exist= db.Departments.Find(id);
            if (Exist != null)
            {
                return View(Exist);
            }
            return RedirectToAction("index");
        }
        public ActionResult Edit(int id)
        {
            Department Exist = db.Departments.Find(id);
            if (Exist != null)
            {
                return View(Exist);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(Department dnew)
        {
            Department Dold = db.Departments.Find(dnew.DeptNo);
            if (Dold != null)
            {
                Dold.DeptName = dnew.DeptName;
                db.SaveChanges();
                return RedirectToAction("index");
                
            }
            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            //var stdep = db.Students.Where(a => a.DeptNo == id).Select(m => m.DeptNo).ToString();
            Department Exist = db.Departments.Find(id);
            //var stdep = db.Students.Where(a => a.DeptNo == id).Select(m => m.DeptNo).ToString();
            if (Exist != null)
            {
                //foreach (var item in stdep)
                //{
                //    item. = null;
                //}


                db.Departments.Remove(Exist);
                db.SaveChanges();
                return RedirectToAction("index");

            }
            return RedirectToAction("index");
        }


        ////////////DepatmentCourse/////////////
        public ActionResult AddCourse(int id)
        {
            var allCourses = db.Courses.ToList();
            var courseInDept = db.DepartmentCourses.Where(a => a.DeptNo == id).Select(m => m.Course);
            var courseNotInDept = allCourses.Except(courseInDept).ToList();
            ViewBag.dept = db.Departments.FirstOrDefault(a => a.DeptNo == id);


            return View(courseNotInDept);
        }
        [HttpPost]
        public ActionResult AddCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (var item in crs)
            {
                if (item.Value == true)
                {
                    db.DepartmentCourses.Add(new DepartmentCourse() { DeptNo = id, CrsId = int.Parse(item.Key) });
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
        //Remove Course

        public ActionResult RemoveCourse(int id)
        {  
            var courseInDept = db.DepartmentCourses.Where(a => a.DeptNo == id).Select(m => m.Course);
            ViewBag.dept = db.Departments.FirstOrDefault(a => a.DeptNo == id);
            return View(courseInDept);
        }
        [HttpPost]
        public ActionResult RemoveCourse(int id, Dictionary<string, bool> crs)
        {
            foreach (var item in crs)
            {
                if (item.Value == true)
                {
                    int crsid = int.Parse(item.Key);
                    var deptCourse = db.DepartmentCourses.FirstOrDefault(a => a.DeptNo == id && a.CrsId == crsid);
                    db.DepartmentCourses.Remove(deptCourse);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
