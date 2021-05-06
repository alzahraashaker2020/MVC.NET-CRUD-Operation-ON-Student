using System;
using System.Data.Entity;
using System.Linq;

namespace TestLab6.Models
{
    public class ModelLab : DbContext
    {
        // Your context has been configured to use a 'ModelLab' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TestLab6.Models.ModelLab' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelLab' 
        // connection string in the application configuration file.
        public ModelLab()
            : base("name=ModelLab")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentCourse> DepartmentCourses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        public System.Data.Entity.DbSet<TestLab6.Models.Course> Courses { get; set; }
        //DepartmentCourse
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}