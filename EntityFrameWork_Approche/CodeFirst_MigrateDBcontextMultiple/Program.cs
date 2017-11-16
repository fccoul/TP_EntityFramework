using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_MigrateDBcontextMultiple
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var ctxStudent=new MyStudentContext())
            {
                //Create et save students

                var student = new Student
                {
                    FirstMidName = "Alain",
                    LastName = "Juppe",
                    EnrollmentDate = DateTime.UtcNow
                };

                ctxStudent.Students.Add(student);
               

                var student1 = new Student
                {
                    FirstMidName = "Denise",
                    LastName = "Aoula",
                    EnrollmentDate = DateTime.UtcNow
                };

                ctxStudent.Students.Add(student1);
                ctxStudent.SaveChanges();

                //---Display All Student
                var _allStudents = (from s in ctxStudent.Students
                                   orderby s.FirstMidName
                                   select s).ToList();
                foreach (var item in _allStudents)
                {
                    string name = item.FirstMidName + " " + item.LastName;
                    Console.WriteLine("ID: {0}, Name: {1}", item.ID, name);

                }
            }

            using(var ctxTeacher=new MyTeacherContext())
            {

                var teacher = new Teacher
                {
                    FirstMidName = "Tomas",
                    LastName = "Onisuka",
                    HireDate = DateTime.UtcNow
                };

                ctxTeacher.Teachers.Add(teacher);
                ctxTeacher.SaveChanges();


                //---Display All Teacher
                var _allTeachers = (from s in ctxTeacher.Teachers
                                    orderby s.FirstMidName
                                    select s).ToList();
                foreach (var item in _allTeachers)
                {
                    string name = item.FirstMidName + " " + item.LastName;
                    Console.WriteLine("ID: {0}, Name: {1}", item.ID, name);

                }
            }
            /////////////////
            Console.WriteLine("Press Any Key to exit...");
            Console.ReadKey();

        }

      
    }

    #region Model
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        //rajout type person
        public Person IdentityPerson { get; set; }
    }



    //-----Type imbriqué
    public class Person
    {
        public Person(string fathername,DateTime birthdate)
        {
            FatherName = fathername;
            BirthDate = birthdate;
        }

        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }
    }
    //------------------
    public class Enrollement
    {
        [Key]
        public int EnrollementID { get; set; }

        public int CourseID { get; set; } //foreign key ---liaison avec propriété de liaisobn virtual Student
        public int StudentID { get; set; } //foreign key
        public Grade? Grade { get; set; }


        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }//foreign key 
        [ForeignKey("CourseID")]
        public virtual Course Cours { get; set; }
    }
    public enum Grade
    {
        A,
        B,
        C,
        D,
        F
    }

    public class Course
    {
        public int CourseID { get; set; }

        // [Index(IsUnique=true)] //creation index unique
        public string Title { get; set; }

        [Index]
        public int Credits { get; set; }

        public virtual ICollection<Enrollement> Enrollments { get; set; } //lazy loading
    }




    public class Teacher
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }
    }

    public class MyStudentContext : DbContext
    {
        public MyStudentContext()
            : base("name=FCOMyFullContextMigration")
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Enrollement> Enrollments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
    }

    public class MyTeacherContext : DbContext
    {
        public MyTeacherContext()
            : base("name=FCOMyFullContextMigration")
        {

        }

        public virtual DbSet<Teacher> Teachers { get; set; }
    }

    #endregion
}
