namespace TestLab6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repeat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptNo = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.DeptNo);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Age = c.Int(nullable: false),
                        Uname = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DeptNo = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DeptNo)
                .Index(t => t.DeptNo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "DeptNo", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "DeptNo" });
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
        }
    }
}
