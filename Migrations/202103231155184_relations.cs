namespace TestLab6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentCourses",
                c => new
                    {
                        DeptNo = c.Int(nullable: false),
                        CrsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeptNo, t.CrsId })
                .ForeignKey("dbo.Courses", t => t.CrsId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DeptNo, cascadeDelete: true)
                .Index(t => t.DeptNo)
                .Index(t => t.CrsId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CrsId = c.Int(nullable: false, identity: true),
                        CrsName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.CrsId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        StId = c.Int(nullable: false),
                        CrsId = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StId, t.CrsId })
                .ForeignKey("dbo.Courses", t => t.CrsId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StId, cascadeDelete: true)
                .Index(t => t.StId)
                .Index(t => t.CrsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentCourses", "DeptNo", "dbo.Departments");
            DropForeignKey("dbo.DepartmentCourses", "CrsId", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "StId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "CrsId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "CrsId" });
            DropIndex("dbo.StudentCourses", new[] { "StId" });
            DropIndex("dbo.DepartmentCourses", new[] { "CrsId" });
            DropIndex("dbo.DepartmentCourses", new[] { "DeptNo" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.DepartmentCourses");
        }
    }
}
