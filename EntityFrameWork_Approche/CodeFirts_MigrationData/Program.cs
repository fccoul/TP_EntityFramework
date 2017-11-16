using CodeFirts_MigrationData.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirts_MigrationData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initilize BDD !!!!!!");
            BuildDatabase();

            Console.WriteLine("Press Any key to exit...");
            Console.ReadKey();
        }

        #region data model


        public class MyContext:DbContext
        {
            public MyContext()
                : base("name=FCOMyContextMigration")  
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyContext, Configuration>("FCOMyContextMigration"));
            }

            public virtual DbSet<Student> Students { get; set; }
            public virtual DbSet<Enrollment> Enrollments { get; set; }
            public virtual DbSet<Course> Courses { get; set; }
            //---rajout de cette table apres creationd e la bdd
          // public virtual DbSet<StudentLogIn> StudentLogins { get; set; } //suppression de l'entite après creation en bdd
        }

        public enum Grade
        {
            A,
            B,
            C,
            D,
            F
        }

        public class Enrollment
        {
            public int EnrollmentID { get; set; }
            public int CourseID { get; set; }
            public int StudentID { get; set; }
            public Grade? Grade { get; set; }

            public virtual Course Course { get; set; }
            public virtual Student Student { get; set; }

        }

        public class Student
        {
            public int ID { get; set; }
            public string LastName { get; set; }
            public string FirstMidName { get; set; }
            public DateTime EnrollmentDate { get; set; }

            //add via code-migration
            public int? Age { get; set; }

            public int Rating { get; set; }

            public string Surnom { get; set; }

            public virtual ICollection<Enrollment> Enrollments { get; set; }

        }

        public class Course
        {
           
            public int CourseID { get; set; }
            public string Title { get; set; }
            [Index]
            public int Credits { get; set; }

            public virtual ICollection<Enrollment> Enrollments { get; set; }

        }

        //---Rajour d'une classe après creation de la BDD
        public class StudentLogIn
        {
            [Key, ForeignKey("Student")]
            public int ID { get; set; }
            public string EmailID { get; set; }
            public string Password { get; set; }

            public virtual Student Student { get; set; }
        }
        #endregion

        #region Methode metier


        //This will create the database and all tables defined by the DbContext.
        public static void BuildDatabase()
        {
            using (MyContext context = new MyContext())
            {
               // context.Database.Initialize(true);
                context.Database.Initialize(true);
            }
        }

        #endregion
    }
}
