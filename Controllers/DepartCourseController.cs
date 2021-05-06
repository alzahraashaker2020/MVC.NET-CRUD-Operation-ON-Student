using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestLab6.Models;

namespace TestLab6.Controllers
{
    public class DepartCourseController : Controller
    {
        private ModelLab db = new ModelLab();
        // GET: DepartCourse
        public ActionResult Index()
        {
            return View(db.DepartmentCourses.ToList());
        }
        public ActionResult AddCourse(int id)
        {
           var allCourses = db.Courses.ToList();
           var courseInDept = db.DepartmentCourses.Where(a => a.DeptNo == id).Select(m => m.Course);
           var courseNotInDept = allCourses.Except(courseInDept).ToList();
           

            return View(courseNotInDept);
        }
        [HttpPost]
        public ActionResult AddCourse(int id,Dictionary<string,bool>crs)
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
            //var allCourses = db.Courses.ToList();
            var courseInDept = db.DepartmentCourses.Where(a => a.DeptNo == id).Select(m => m.Course);
            //var courseNotInDept = allCourses.Except(courseInDept).ToList();

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
                    //db.DepartmentCourses.Add(new DepartmentCourse() { DeptNo = id, CrsId = int.Parse(item.Key) });
                    var deptCourse = db.DepartmentCourses.FirstOrDefault(a => a.DeptNo == id && a.CrsId == crsid);
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}