using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;
using System.Data.Entity.Spatial;
using System.Globalization;

namespace OthersFeatures
{
    class Program
    {
        static void Main(string[] args)
        {

            #region validation champs

            ValidationField();
            #endregion


            #region Jointure - Affichage Explicit Lazy
            ExplicitLazyLoading();
            #endregion

            #region Jointure - Affichage Lazy
            LazyLoading();
            #endregion

            #region Jointure - Affichage Eager
            EagerLoadingData();
            #endregion

            #region heritage
            inHeritance();
            Console.WriteLine("All Student database");
            

            #endregion

            #region GPS
            Dictionary<string, DbGeography> dicoLocations = new Dictionary<string, DbGeography>();
             
            dicoLocations.Add("CAP SUD",CreatePoint(5.298249,-3.986069));//5.298249, -3.986069 pour latitude :longitude respetivement
            dicoLocations.Add("CAP NORD",CreatePoint(5.356175,-3.966651));  
            dicoLocations.Add("SUPERMARCHE CARREFOUR BINGERVILLE",CreatePoint(5.373712,-3.931180));
            dicoLocations.Add("SUPERMARCHE CARREFOUR ABOBO", CreatePoint(5.394742, -4.008745));


           DbGeography myLocation=CreatePoint(5.373384, -3.998707); //---scocoe 2pltx
            //obtenir les locations par ordre deproximité à sococé !
           var locations = from loc in dicoLocations
                           orderby loc.Value.Distance(myLocation)
                           select loc;

           Console.WriteLine("Affichage des emplacements proches du supermarché SOCOCE !");
           foreach (var item in locations)
           {
               Console.WriteLine(item.Key);
           }
           Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

           Console.WriteLine("Mise à jour et creation Emplacement ");

           using (Edm_v6Context ctxGPS = new Edm_v6Context())
           {
               /*  genere un eexception si aucune colonne vide
               var res = (from g in ctxGPS.Courses
                          where g.Location.Equals(null)
                          select g).Take(1).SingleOrDefault<Cours>();
               */
               var res = (from g in ctxGPS.Courses
                          where g.Location.Equals(null)
                          select g).Take(1).FirstOrDefault();

               if(res!=null)
               res.Location = CreatePoint(5.295422, -3.983493);//-PRIMA CENTER, Côte d'Ivoire, Abidjan


               ctxGPS.Database.Log = Console.Write;
               ctxGPS.SaveChanges();


               int noOfRowUpdated2 = ctxGPS.Database.ExecuteSqlCommand("DELETE FROM Courses WHERE Title='TechDays' ");
               ctxGPS.SaveChanges(); 

               Console.WriteLine("Ajout Cours et Emplacement ");
               TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
               ctxGPS.Courses.Add(new Cours
               {
                   Credit=144,
                   Title="TechDays",
                   Location=DbGeography.FromText("POINT(-3.987559 5.295257)")//long,lat 5.295257, 
                   

               });
               ctxGPS.SaveChanges();
               Console.WriteLine("");

           }
           Console.WriteLine("Fin à Mise à jour Emplacement ");

            #endregion

          

           using (Model_FeaturesContext context=new Model_FeaturesContext())
           {

               #region Type Champ - Enum
               //---ajout des departements en BDD
                //context.Departements.Add(new Departement { DeptName = DepartementNames.Chimics });
                //context.Departements.Add(new Departement { DeptName = DepartementNames.Maths });
                //context.Departements.Add(new Departement { DeptName = DepartementNames.Informatique });
                //context.Departements.Add(new Departement { DeptName = DepartementNames.Economics });

                //context.SaveChanges();

                var department = (from d in context.Departements
                                  where d.DeptName == DepartementNames.Informatique
                                  select d).FirstOrDefault();

                Console.WriteLine("Departement ID : {0} - Departement Name : {1}",department.DeptID,department.DeptName);
               #endregion
                //*--------------------------------------------------------------------------------------------------
                #region Appel Asyncrhone -- EF v6

                QueryAsynchrosous();
               
                #endregion

                #region Logging
                //getLogging();
                setLoggingOut();
                #endregion

                Console.WriteLine("Press Any key to exit...");
                    Console.ReadKey();

            }

           
        }

        #region en mode asynchrone
        static  void QueryAsynchrosous()
        {
            Console.WriteLine("Database Operation Started");
            //DataBaseOperations();
            var task = DataBaseOperationsAsync(); // appel asynchrne
            //Console.WriteLine();
            //Console.WriteLine("Database Operation completed");
            Console.WriteLine();
            Console.WriteLine("Entity Framework Tutorial");
            task.Wait();//iic cette instruction ,impose au programme d'attendre la fin d'execution des taches asynchrone avant d'afficher "Press Any key to exit..."
        }

        private static void DataBaseOperations()
        {
            using(Edm_v6Context edv=new Edm_v6Context() )
            {

                int noOfRowUpdated2 = edv.Database.ExecuteSqlCommand("DELETE FROM Students WHERE FisrtName='Marina' ");
                edv.SaveChanges(); 
                //---create new student et save...
                edv.Students.Add(new Student
                {
                    EnrollmentDate = DateTime.UtcNow,
                    FisrtName = "Marina",
                    LastName = "Stepahnie"
                });

                Console.WriteLine("calling Save Changes...");
                edv.SaveChanges();
                Console.WriteLine("SaveChanges completed...");

                //---Query for all Stduent ordered by first name
                var students = (from s in edv.Students
                                orderby s.LastName
                                select s).ToList();
                //Write All Stuidents Out Console...
                Console.WriteLine();
                Console.WriteLine("All Student...");
                foreach (var item in students)
                {
                    string name = item.FisrtName + " " + item.LastName;
                    Console.WriteLine("  "+name);
                }
            }
        }

        public static async Task DataBaseOperationsAsync()
        {
            using (Edm_v6Context edv = new Edm_v6Context())
            {
                //---create new student et save...
                edv.Students.Add(new Student
                {
                    EnrollmentDate = DateTime.UtcNow,
                    FisrtName = "Marina",
                    LastName = "Stepahnie"
                });

                Console.WriteLine("calling Save Changes...");
                await edv.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed...");

                //---Query for all Stduent ordered by first name
                var students = await (from s in edv.Students
                                      orderby s.LastName
                                      select s).ToListAsync();
                //Write All Stuidents Out Console...
                Console.WriteLine();
                Console.WriteLine("All Student...");
                foreach (var item in students)
                {
                    string name = item.FisrtName + " " + item.LastName;
                    Console.WriteLine("  " + name);
                }
            }

            
        }

        #endregion

        #region logging cmde

        static void getLogging()
        {
            using(var ctx = new Edm_v6Context())
            {

                int noOfRowUpdated2 = ctx.Database.ExecuteSqlCommand("DELETE FROM Students WHERE FisrtName='Sersei' ");
                ctx.SaveChanges(); 

                ctx.Database.Log = Console.Write;
                //-create new student
                ctx.Students.Add(new Student
                {
                    FisrtName="Sersei",
                    LastName="Bolton",
                    EnrollmentDate=DateTime.UtcNow
                });

                ctx.SaveChanges();
            }
        }


        static void setLoggingOut()
        {
            using (var ctx = new Edm_v6Context())
            {
                string appName=GetAppTitle()+" v."+GetAppVersion();
                ctx.Database.Log = s =>MyLogger.Log(appName,s);
                //-create new student
                Student stud=new Student
                {
                    FisrtName = "Sersei",
                    LastName = "Bolton",
                    EnrollmentDate = DateTime.UtcNow
                };
                ctx.Students.Add(stud);

                ctx.SaveChanges();

              var student= (from s in ctx.Students
                           where s.StudentID==stud.StudentID //like (like IDENTITY in MS SQL)
                           select s).SingleOrDefault<Student>();
                          // select new {s.LastName,s.FisrtName}).SingleOrDefault<Student>();
              Console.WriteLine("Nom {0} , Prenomsc{1}", student.FisrtName, student.LastName);

            }
        }
#endregion

        #region Get Infos Assembly Title App - Version

        static string GetAppTitle()
        {
            AssemblyTitleAttribute attributes = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute), false);
            return attributes.Title;
        }

        static string GetAppVersion()
        {
            Assembly thisAssem = typeof(Program).Assembly;
            AssemblyName thisAssemname = thisAssem.GetName();
            Version ver = thisAssemname.Version;

            return ver.ToString();
        }


#endregion

        #region methode private GPS
        public static DbGeography CreatePoint(double latitude,double longitude)
        {
            String lon = longitude.ToString(CultureInfo.InvariantCulture);
            String lat = latitude.ToString(CultureInfo.InvariantCulture);
            var point = string.Format("POINT({0} {1})", lon, lat);

            // 4326 is most common coordinate system used by GPS/Maps
            //return DbGeography.PointFromText(point, 4326);
            //return DbGeography.PointFromText(point, DbGeography.DefaultCoordinateSystemId); //---Ok aussi
            return DbGeography.FromText(point, DbGeography.DefaultCoordinateSystemId);
        }

        #endregion

        #region Heritage Classes
        static void inHeritance()
        {
            using(var ctxHeritage=new InHeritanceModelContext())
            {

                //---Vider table
                int noOfRowUpdated = ctxHeritage.Database.ExecuteSqlCommand("TRUNCATE TABLE Persons_Teacher");              
                int noOfRowUpdated1 = ctxHeritage.Database.ExecuteSqlCommand("TRUNCATE TABLE Persons_Eleve");                 
                int noOfRowUpdated2 = ctxHeritage.Database.ExecuteSqlCommand("DELETE FROM Persons");
                ctxHeritage.SaveChanges(); 
               

                //add student and teacher
                var student = new Eleve
                {
                    LastName = "Lucky",
                    FisrtName = "Luke",
                    EnrollmentDate = DateTime.UtcNow
                };
                ctxHeritage.Persons.Add(student);

                var student1 = new Eleve
                {
                    LastName = "Dalton",
                    FisrtName = "Joe",
                    EnrollmentDate = DateTime.UtcNow
                };
                ctxHeritage.Persons.Add(student1);

                var HireYear=DateTime.Now.Year-5;
                var dateHire=new DateTime(HireYear,DateTime.Now.Month,DateTime.Now.Day);
                var Teacher = new Teacher()
                {
                    FisrtName = "Foljker",
                    LastName = "Tom",
                    HireDate = dateHire
                };
                ctxHeritage.Persons.Add(Teacher);

                ctxHeritage.SaveChanges();

                //Displaying
                //---StudentID est utilisé ,vu que la meme entité est deja utilisé dans le EDM_v6---afin deviter conflits et erreur
                Console.WriteLine("");
                foreach (var item in ctxHeritage.Persons.OfType<Eleve>())
                {
                    string name = item.FisrtName + " - " + item.LastName;
                    Console.WriteLine("ID :{0} , Name :{1} , \t Date Inscription :{2}", item.ID, name, item.EnrollmentDate);
                }

                Console.WriteLine("");
                Console.WriteLine("Display All teacher");
                foreach (var item in ctxHeritage.Persons.OfType<Teacher>())
                {
                    string name = item.FisrtName + " - " + item.LastName;
                    Console.WriteLine("ID :{0} , Name :{1} , \t Date Embauche :{2}", item.ID, name, item.HireDate);
                }
               
            }
        }
        #endregion

        #region Eager Loading
        static void EagerLoadingData()
        {
            using(Edm_v6Context evc=new Edm_v6Context())
            {
                //load all students and related enrollments
                var _students = evc.Students.Include(e => e.Enrollements).ToList();

                foreach (var student in _students)
                {
                    string name = student.FisrtName + " " + student.LastName;
                    Console.WriteLine("ID:{0}, Name:{1}", student.StudentID, name);
                     Console.WriteLine("----------------------");
                    foreach (var item in student.Enrollements)
                    {
                        Console.WriteLine("Enrollment ID:{0},Course ID:{1}", item.EnrollmentID, item.CourseCourseID);
                    }
                }


                ///get cours ID pour IDStudent :17
                var _stud = evc.Students
                               .Where(s=>s.StudentID==17)
                                .Include(i => i.Enrollements).SingleOrDefault();
                Console.WriteLine("Les cours de l'etudiant {0} ayant l'ID {1}",_stud.FisrtName+""+_stud.LastName,_stud.StudentID);
                foreach (var item in _stud.Enrollements)
                {
                    Console.WriteLine("\t Enrollment ID:{0}, \t Course ID:{1}", item.EnrollmentID, item.CourseCourseID);
                }

                //get Cours libelle ,note obtenue , eleve concerné
                evc.Database.Log = Console.Write;
              //  var _studAll = evc.Students
               //              .Include(s => s.Enrollements.Select(e => e.Cours)).ToList(); ---ok
                var _studAll = evc.Students.Include("Enrollements.Cours").ToList(); //meme chose que le precedent
                
                foreach (var _std in _studAll)
                {
                    Console.WriteLine("Etudiant {0}",_std.FisrtName+""+_std.LastName);
                    foreach (var enr in _std.Enrollements)
                    {
                        Console.WriteLine("\t Enrollment ID:{0}, \t Course ID:{1} \t Course:{2} \t Credit:{3} \t Note:{4}",
                               enr.EnrollmentID, enr.CourseCourseID,enr.Cours.Title,enr.Cours.Credit,enr.Grade);
                    }
            
                }
            }
        }
        #endregion

        #region Lazy Loading

        static void LazyLoading()
        {
            using(Edm_v6Context lazyContext=new Edm_v6Context())
            {
                //loading student Only
                IList<Student> lstStudents = lazyContext.Students.ToList<Student>();
                foreach (var item in lstStudents)
                {
                    string name = item.FisrtName + " " + item.LastName;
                    Console.WriteLine("ID:{0}, Name:{1}", item.StudentID, name);

                    //chargement au besoin des inscriptions concernés
                    foreach (var enr in item.Enrollements)
                    {
                        Console.WriteLine("Enrollment ID:{0},Course ID:{1}", enr.EnrollmentID, enr.CourseCourseID);
                    }
                }

                Console.WriteLine("End cahrgement lazy !!!!!!!!!!!!!!");
                Console.WriteLine("");
            }
        }

        #endregion

        #region Explicit Loading

        static void ExplicitLazyLoading()
        {
            using (Edm_v6Context ExplicitlazyContext = new Edm_v6Context())
            {
                //TUR off lazy Loading
                ExplicitlazyContext.Configuration.LazyLoadingEnabled = false;

                //recuperation dun eleve...
                var unstudent=(from s in ExplicitlazyContext.Students
                                   where s.FisrtName=="Wasim"
                                   select s).FirstOrDefault<Student>();

               string name = unstudent.FisrtName + " " + unstudent.LastName;
                    Console.WriteLine("ID:{0}, Name:{1}", unstudent.StudentID, name);

                    Console.WriteLine("cahrgement sans appliquer methode -->load()");
                    //chargement au besoin des inscriptions concernés //ici saurit bien pu se passer sur une collection entire d'etudiants
                    foreach (var enr in unstudent.Enrollements)
                    {
                        Console.WriteLine("Enrollment ID:{0},Course ID:{1}", enr.EnrollmentID, enr.CourseCourseID);
                    }
             
                Console.WriteLine("Chargement Explicit lazy sur une collection entites...");
                Console.WriteLine("");
                //---charegment explicit sur collection
                    ExplicitlazyContext.Entry(unstudent).Collection(s=>s.Enrollements).Load();

                 foreach (var enr in unstudent.Enrollements)
                    {
                        Console.WriteLine("Enrollment ID:{0},Course ID:{1}", enr.EnrollmentID, enr.CourseCourseID);
                    }
                Console.WriteLine("End cahrgement explicit !!!!!!!!!!!!!!");

                //-----------chargement explicit sur une reference ---l'incription qui contient une refrence au student
                Console.WriteLine("Chargement explicit Lazy sur une reference...");
                 //---pour ce faire un charge
                var inscription = ExplicitlazyContext.Enrollements.Find(4);
                 //------charger uniquement la lite d'incription liée à ce edtudiant
                ExplicitlazyContext.Entry(inscription).Reference(r => r.Student).Load();
                ExplicitlazyContext.Entry(inscription).Reference(r => r.Cours).Load();

                Console.WriteLine("Inscription ID {0},Cours {1} , Note {2} ,\n\t Student {3}",inscription.EnrollmentID,inscription.Cours.Title,inscription.Grade, inscription.Student.FisrtName + " " + inscription.Student.LastName
                       );
                

                //-----------charger uniquement le sous - ensemble d'une collection d'inscrition precise pour une etudiant
                Console.WriteLine("charger uniquement le sous - ensemble d'une collection d'inscrition precise pour une etudiant");
                //ExplicitlazyContext.Database.Log = Console.Write;
                var stu = ExplicitlazyContext.Students.Find(17);
               stu.Enrollements= ExplicitlazyContext.Entry(stu).Collection(c =>
                                        c.Enrollements).Query()//---pour le sous ensemble
                                                       .Where(w => w.CourseCourseID == 1)
                    //.Load();
                                                       .ToList();
                        
                     string names = stu.FisrtName + " " + stu.LastName;
                foreach (var enr in stu.Enrollements)
                {
                    Console.WriteLine("Enrollment ID:{0},Course ID:{1}", enr.EnrollmentID, enr.CourseCourseID);
                }
                Console.WriteLine("");
            }
        }


        #endregion

        #region validation...

        static void ValidationField()
        {

            #region override methode a mettre après mise ç jour de l'edmx depuis la BDD
            /*
                         protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, 
      System.Collections.Generic.IDictionary<object, object> items) {

          var list = new List<System.Data.Entity
             .Validation.DbValidationError>();

         if (entityEntry.Entity is ETUDIANT) {

            //if (entityEntry.CurrentValues.GetValue<string>("Adress") == "") {
            if (string.IsNullOrEmpty(entityEntry.CurrentValues.GetValue<string>("Adress"))) {

               //var list = new List<System.Data.Entity
               //   .Validation.DbValidationError>();

               list.Add(new System.Data.Entity.Validation
                  .DbValidationError("Adress", "Oups le champ adresse est obligatoire !"));

               //return new System.Data.Entity.Validation
               //   .DbEntityValidationResult(entityEntry, list);
            }

            if (entityEntry.CurrentValues.GetValue<string>("LastName") == "" || string.IsNullOrEmpty(entityEntry.CurrentValues.GetValue<string>("LastName")))
            {

                //var list = new List<System.Data.Entity
                //   .Validation.DbValidationError>();

                list.Add(new System.Data.Entity.Validation
                   .DbValidationError("LastName", "LastName is required"));

                //return new System.Data.Entity.Validation
                //   .DbEntityValidationResult(entityEntry, list);
            }

            return new System.Data.Entity.Validation
                 .DbEntityValidationResult(entityEntry, list);
         }

         return base.ValidateEntity(entityEntry, items);
               
   
               }
        */
            #endregion

            using (Edm_v6Context validationContext=new Edm_v6Context())
            {


                try
                {
                    var student = new ETUDIANT
                              {
                                  //LastName = "risk",
                                  FisrtName = "fracasse",
                                  EnrollmentDate = DateTime.Now
                              };

                    //-------enregistrement en BDD
                    validationContext.ETUDIANTs.Add(student);
                    validationContext.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbValidationex)
                {

                    foreach (var item in dbValidationex.EntityValidationErrors)
                    {
                        foreach (var error in item.ValidationErrors)
                        {
                            Console.WriteLine("Error : {0} , description {1}", error.PropertyName,error.ErrorMessage);
                            
                        }
                    }
                }
            }
        }

        #endregion
    }
}
