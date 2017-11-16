using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFisrt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DBFirst_DBEntities())
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
                #region Vue
                var qry = from b in db.myViews
                          orderby b.FisrtName
                          select b;

                foreach (var item in qry)
                {
                    Console.WriteLine(item.FisrtName + " " + item.EnrollmentDate);
                }
                #endregion
                Console.WriteLine("***********************************************");
                Console.WriteLine("All student form Stored Procedue");

                #region PS
                int StudentID = 6;
                var studentGrades = db.GetStudentGrades(StudentID);
                foreach (var item in studentGrades.ToList())
                {
                    Console.WriteLine("Course :{0} , Grade :{1} ", item.CourseID, item.Grade);
                }

                #endregion
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
                #region Function
                var CourseID=1;
                var std=db.GetStudentGradesForCourse(CourseID);
                foreach (var result in std.ToList())
	                    {
		  Console.WriteLine("Student :{0} , Grade :{1} ", result.StudentID, result.Grade);
                        }

                #endregion
                Console.WriteLine("***********************************************");
                Console.WriteLine("Display All avec Query SQL ditect (natif)");

                #region SQL Natif
                //var students = db.Students.SqlQuery("SELECT STUDENTID, FisrtName FROM dbo.STUDENTS").ToList();
                var students = db.Students.SqlQuery("SELECT * FROM dbo.STUDENTS").ToList();
                foreach (var stud in students)
                {
                    Console.WriteLine("Student :{0} , ID :{1} ", stud.FisrtName, stud.StudentID);
                }
                #endregion


                Console.WriteLine("***********************************************");
                Console.WriteLine("Display All  Student from SQL non Typée");
                #region SQL Non Typée

                var studentnames = db.Database.SqlQuery<string>("SELECT TOP 3 FisrtName FROM dbo.STUDENTS").ToList();
                foreach (var item in studentnames)
                {
                    Console.WriteLine("Student :{0} ", item);
                }

                #endregion


                Console.WriteLine("***********************************************");
                Console.WriteLine("Display All  Student from SQL avec ExecuteSqlCommand");
                #region SQL ExecuteSqlCommand


                //-Upadte Command SQL
                int noOfRowUpdated = db.Database.ExecuteSqlCommand("UPDATE dbo.Students SET FisrtName='ALI' WHERE studentID=16");
                db.SaveChanges();

                var _st = db.Students.SqlQuery("SELECT * FROM dbo.STUDENTS WHERE studentID=16").Single();
                string name=_st.FisrtName+ " " +_st.LastName;

                Console.WriteLine("ID: {0}, Name: {1}, \tEnrollment Date {2} ", _st.StudentID, name, _st.EnrollmentDate.ToString());
                

                #endregion



                //*--------------------------------------------------------------------------------------------------
                    Console.WriteLine("Press Any key to exit...");
                    Console.ReadKey();
                }
            }
        }
    }


