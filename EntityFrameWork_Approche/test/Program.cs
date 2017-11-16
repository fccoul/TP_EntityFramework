using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new dbFirst_DBEntities())
            {
                var query = from b in db.Students
                            orderby b.FisrtName
                            select b;

                Console.WriteLine("All Stduent in the databse :");
                foreach (var item in query)
                {
                    Console.WriteLine(item.FisrtName + "  " + item.LastName);
                }

                Console.WriteLine("All Student in View...");
                var qry = from b in db.myView
                          orderby b.FisrtName
                          select b;

                foreach (var item in qry)
                {
                    Console.WriteLine(item.FisrtName + " " + item.EnrollmentDate);
                }
                Console.WriteLine("***********************************************");
                Console.WriteLine("All student form Stored Procedue");
                int StudentID = 6;
                var studentGrades = db.GetStudentGrades(StudentID);
                foreach (var item in studentGrades)
                {
                    Console.WriteLine("Course :{0} , Grade :{1} ", item.CourseID, item.Grade);
                }


                Console.WriteLine("***********************************************");
                Console.WriteLine("Add student + enrollment");

               
                #region entity Disconnected
                /*
                //-ajout d"un etudiant et de ses inscriptions
                var student = new Student
                {
                    StudentID = 1001,
                    FisrtName = "Wasim",
                    LastName = "Akry",

                    EnrollmentDate = DateTime.Parse("2015-10-10"),

                    Enrollements = new List<Enrollement>
                    {
                        new Enrollement{EnrollmentID=2001,Grade="5.1",CourseCourseID=1,StudentStudentID=10001} //10001 --ici ,mais en base il aura unnumero auto-increment
                       ,new Enrollement{EnrollmentID=2002,Grade="58.1",CourseCourseID=2,StudentStudentID=10001}
                    }
                };

                using (var context = new DataBaseFirst_DBEntities())
                {
                    try
                    {
                        context.Students.Add(student);
                        context.SaveChanges();
                        //---affichage de l'etat actuel de l'objet
                        //--DbContext.Entry pour obtenir l'accès au suivi des changements d'informations que Entity Framework a au sujet de la nouvelle Student.
                        Console.WriteLine("New Student ({0} {1}):{2}", student.FisrtName, student.LastName, context.Entry(student).State);

                        foreach (var item in student.Enrollements)
                        {
                            Console.WriteLine("Enrollment ID : {0} State : {1}", item.EnrollmentID, context.Entry(item).State);
                        }
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {
                        //EntityValidationErrors is a collection which represents the entities which couldn't be validated successfully
                        foreach (var item in ex.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type : \"{0}\", in State: \"{1}\" has the following validation errors : ", item.Entry.Entity.GetType().Name, item.Entry.State);

                            foreach (var er in item.ValidationErrors)
                            {
                                Console.WriteLine("-Property : \"{0}\", Error: \"{1}\"", er.PropertyName, er.ErrorMessage);

                            }

                        }
                        //throw;

                    }

                    //---DbSet.Add : utiliser sur les nouvelles entités
                    //--- DbSet.Attach: utiliser sur les entites existantes

                    */
                #endregion

                 Console.WriteLine("***********************************************");
                Console.WriteLine("Display All note Student from Fucntion");
                var CourseID=1;
                var std=db.GetStudentGradesForCourse(CourseID);
                foreach (var result in std)
	                    {
		  Console.WriteLine("Student :{0} , Grade :{1} ", result.StudentStudentID, result.Grade);
	                    }
                    
                    Console.WriteLine("Press Any key to exit...");
                    Console.ReadKey();
                }
            }
        }
    }
}
