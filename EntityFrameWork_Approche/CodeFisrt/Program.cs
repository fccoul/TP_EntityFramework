using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFisrt
{
    class Program
    {
        static void Main(string[] args)
        {

            #region basics
            /*
            using (MyContext ctx=new MyContext())
            {
                //create & save student
                Student student = new Student
                {
                    FirstName = "Alain",
                    LastName = "Jerome",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                };

              //  ctx.Database.Log = Console.Write;
                ctx.Students.Add(student);
                
                Student student1 = new Student
                {
                    FirstName = "Mark",
                    LastName = "Houston",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                };
                ctx.Students.Add(student1);
                ctx.SaveChanges();

                var AllStudents = (from s in ctx.Students
                                   orderby s.FirstName
                                   select s).ToList<Student>();
                //Display All student from database
                foreach (var item in AllStudents)
                {
                    string name = item.FirstName + " " + item.LastName;
                    Console.WriteLine("ID : {0} , Name : {1} ", item.ID, name);
                }
            }
            */

#endregion
            //---SetUp and seed the database
            Console.WriteLine("Initialisation and seeding database...");
         
            Database.SetInitializer(new DatareferenceInitializer());
            var db = new DebugContext();
            //db.Database.Initialize(true);
            int count = db.Students.Count();
         

            /* ---OK
            Database.SetInitializer(new CodeFisrt.Program.MyContextSemence.UniDBInitializer <MyContextSemence>());
            var dbt = new MyContextSemence();
            int cpt = dbt.Students.Count();
             * */

            Console.WriteLine("Databse created and seeded with {0} students enregistrés...", count);
            //Console.WriteLine("Databse Context Semence created and seeded with {0} students enregistrés...", cpt);
            //Database.SetInitializer(new UniDBInitializer<MyContextSemence>());
           // var db=

            Console.WriteLine("Press Any key to exit...");
            Console.ReadKey();
        }


        #region data-model

        [Table("StduentInfo")] //au cas ou le nom de la class est different du nom de la table en BDD
        public class Student //interessant popur admAgent et admuUsers...
        {
            public int ID { get; set; }
            //[Index(IsUnique = true)] //creation index unique
            [Required]
            public string LastName { get; set; }
            [StringLength(15)]
            public string FirstName { get; set; }
            [Column("DateInscription")]
            public DateTime EnrollmentDate { get; set; }


            [NotMapped]
            public string FatherName { get; set; }

            public virtual ICollection<Enrollement> Enrollments {get;set;}

            
            public virtual StudentLogin StudentLogin { get; set; }
        }
        //---relation one to One

        public class StudentLogin //interessant popur admAgent et admuUsers...
        {
            [Key,ForeignKey("Student")]
            public int ID { get; set; }
            public string EmailID { get; set; }
            public string Password { get; set; }

            public virtual Student Student { get; set; }
        }
        //-------------------------------
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
            public virtual Course Cours {get;set;}
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



        /// <summary>
        /// prise en compte clé primaire composée
        /// </summary>
        public class DrivingLicense
        {
            [Key,Column(Order=1)]
            public int licenseNuumber { get; set; }
            [Key,Column(Order=2)]
            public string IssuingCountry { get; set; }

            public DateTime Issued { get; set; }
            public DateTime Delivred { get; set; }
        }

        public class MyContext : DbContext
        {
            //crrer la base de données avec ce nom : FCOMyContext
            //- il est a noté que le parmater fourni à base(..),le système l'interprètre dans un 1er temps comme nom de la bdd à créérer
            //*si aucunec haine de connection existe dans le file de config.sinon si 
            //des chaines de connxions exiestent et ne correspondaent en aaucun cas à celui passé en paramètre de la classe "MyContext"
            //EntityFramework crre la bse si inexistante et tale sur le server loacl (loacaldb) sisnon si correspondance de nom
            //considère ce nom comme le le nom de la chaine de connexion au niveau config
            //*et crrer la bse de donnés avec le nom de "initial catalog qui sera defini..."

            //public MyContext():base("FCOMyContext") //dans server localdb
            public MyContext():base("name=FCOMyContext") //chaine de connexion bien precise
            {
                
            }
            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<Enrollement> Enrollments { get; set; }
            public virtual DbSet<Student> Students { get; set; }

            public virtual DbSet<DrivingLicense> DrivingLicenses { get; set; }
         }
#endregion


        #region données de semence  - Initialisition de la bdd par exemples avec des valeurs ,datas de reference

        public class MyContextSemence : DbContext
        {
            public MyContextSemence() : base("name = FCOMyContext")
            {
                Database.SetInitializer<MyContextSemence>(new UniDBInitializer<MyContextSemence>());
            }

            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<Enrollement> Enrollments { get; set; }
            public virtual DbSet<Student> Students { get; set; }


            //--------------------------------------------------------------
            public class UniDBInitializer<T> : DropCreateDatabaseAlways<MyContextSemence>
            {
                public override void InitializeDatabase(MyContextSemence context)
                {

                    if (CheckConnection())
                        context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                                                         string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));


                    base.InitializeDatabase(context);
                }

                protected override void Seed(MyContextSemence context)
                {
                    IList<Student> _students = new List<Student>();
                    #region ...Add Student

                    _students.Add(new Student
                    {

                        LastName = "Biily",
                        FirstName = "JEan",
                        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                    });
                    _students.Add(new Student
                    {

                        LastName = "Nick",
                        FirstName = "Ford",
                        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                    });
                   
                    #endregion

                    foreach (var item in _students)
                    {
                        context.Students.Add(item);
                        base.Seed(context);
                    }
                }

            }
        }


     
        #endregion


        #region OtherSemence

        public class DebugContext:DbContext
        {
            public DebugContext ():base("name=FCOMyContext")
	        {

	        }

            static DebugContext()
            {
                Database.SetInitializer(new DatareferenceInitializer());
            }

            public IDbSet<Student> Students { get; set; }
        }
        public class DatareferenceInitializer : DropCreateDatabaseAlways<DebugContext>
        {

            public override void InitializeDatabase(DebugContext context)
            {

                if (CheckConnection())
                    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                                                     string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));


                base.InitializeDatabase(context);
            }

              protected override void Seed(DebugContext _ctx)
            {
                base.Seed(_ctx);

                IList<Student> _students = new List<Student>();
                #region ...Add Student

                _students.Add(new Student
                {

                    LastName = "Romy",
                    FirstName = "Trevaor",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                });
                _students.Add(new Student
                {

                    LastName = "Adrew",
                    FirstName = "Peters",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                });
                _students.Add(new Student
                {

                    LastName = "Brice",
                    FirstName = "Lambson",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                });

                #endregion

                foreach (var item in _students)
                {
                    _ctx.Students.Add(item);
                     
                }
            }
          }


        public static bool CheckConnection()
        {
            try
            {
                DebugContext MyContext = new DebugContext();
                MyContext.Database.Connection.Open();
                MyContext.Database.Connection.Close();
            }
            catch (SqlException)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
