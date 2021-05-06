namespace TestLab6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stdImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "photoName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "photoName");
        }
    }
}
