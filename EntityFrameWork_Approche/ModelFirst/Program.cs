using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db=new ModelFirt_DBContext())
            {
                //create new student....
                Console.Write("Enter a name for a new student: ");

                //-Transaction
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        #region add student...
                        var firstname = Console.ReadLine();

                        var student = new Student
                        {
                            EnrollmentDate = DateTime.Now,
                            //FisrtName = "Peter",
                            FisrtName = firstname,
                            LastName = "Pan",
                            StudentID = 2
                        };

                        db.Students.Add(student);
                        db.SaveChanges();
                        #endregion

                        Console.WriteLine("All student in the database :  ");

                        DisplayStudents(db);

                        #region Update student

                        Console.WriteLine("Upated Student ");

                        var stud = (from d in db.Students
                                    where d.FisrtName == "boyka"
                                    select d).Single();
                        stud.LastName = "YURI";
                        db.SaveChanges();


                        #endregion

                        Console.WriteLine("All student in the database after mise à jour :  ");
                        DisplayStudents(db);


                        #region Delete Student

                        //var studentToDel = (from s in db.Students
                        //                    where s.StudentID == 2
                        //                    select s).Single();
                        //db.Students.Remove(studentToDel);
                        //db.SaveChanges();

                        #endregion

                        Console.WriteLine("All student in the database after deleting :  ");
                        DisplayStudents(db);

                      

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }
        }

        static void DisplayStudents(ModelFirt_DBContext _db)
        {
            #region Display student
            //using (var db = new ModelFirt_DBContext())
            //{
                var query = from b in _db.Students
                            orderby b.FisrtName
                            select b;


                foreach (var item in query)
                {
                    Console.WriteLine(item.FisrtName + " " + item.LastName);
                }
            //}
            #endregion
        }
    }
}
