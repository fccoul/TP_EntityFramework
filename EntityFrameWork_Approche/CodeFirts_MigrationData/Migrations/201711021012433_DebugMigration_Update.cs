namespace CodeFirts_MigrationData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DebugMigration_Update : DbMigration
    {
        public override void Up()
        {
            //---
            Sql("UPDATE Students SET Age = 95 WHERE ID =1"); 
        }
        
        public override void Down()
        {
        }
    }
}
