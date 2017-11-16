namespace CodeFirts_MigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DebugMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Age", c => c.Int());
            AddColumn("dbo.Students", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Surnom", c => c.String());

            //---
            Sql("UPDATE Students SET Age = 95 WHERE ID =1"); 
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Surnom");
            DropColumn("dbo.Students", "Rating");
            DropColumn("dbo.Students", "Age");
        }
    }
}
