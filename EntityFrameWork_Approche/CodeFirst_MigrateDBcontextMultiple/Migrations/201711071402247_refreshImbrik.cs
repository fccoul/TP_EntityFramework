namespace CodeFirst_MigrateDBcontextMultiple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refreshImbrik : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Credits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .Index(t => t.Credits);
            
            CreateTable(
                "dbo.Enrollements",
                c => new
                    {
                        EnrollementID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollementID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            AddColumn("dbo.Students", "IdentityPerson_FatherName", c => c.String());
            AddColumn("dbo.Students", "IdentityPerson_BirthDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollements", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Enrollements", "CourseID", "dbo.Courses");
            DropIndex("dbo.Enrollements", new[] { "StudentID" });
            DropIndex("dbo.Enrollements", new[] { "CourseID" });
            DropIndex("dbo.Courses", new[] { "Credits" });
            DropColumn("dbo.Students", "IdentityPerson_BirthDate");
            DropColumn("dbo.Students", "IdentityPerson_FatherName");
            DropTable("dbo.Enrollements");
            DropTable("dbo.Courses");
        }
    }
}
