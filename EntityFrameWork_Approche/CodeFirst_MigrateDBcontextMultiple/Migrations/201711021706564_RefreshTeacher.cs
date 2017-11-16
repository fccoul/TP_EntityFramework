namespace CodeFirst_MigrateDBcontextMultiple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefreshTeacher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "HireDate", c => c.DateTime());
            AddColumn("dbo.Teachers", "FirstMidName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "HireDate");
            DropColumn("dbo.Teachers", "FirstMidName");
        }
    }
}
