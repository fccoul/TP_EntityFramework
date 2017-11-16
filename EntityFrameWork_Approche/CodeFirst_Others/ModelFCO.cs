using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Others
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
       
    }

    public class House
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public Adress Adress { get; set; }
        public ICollection<Person> Resident { get; set; }

        public Person Owner { get; set; }
    }

    public class dept
    {
        public int Id { get; set; }
        public string libelle { get; set; }
    }

    public class Adress
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
    }


    public class myModel
    {
        //This will create the database and all tables defined by the DbContext.
        public  void BuildDatabase()
        {
               using(DebugContext context=new DebugContext())
                {
                    context.Database.Initialize(true);
                }
        }

            public class DebugContext:DbContext
                    {
                        public DebugContext ():base("name=FCOMyContextOther")
	                    {
                            Database.SetInitializer<DebugContext>(new CustomInitializer<DebugContext>());
                           // Database.SetInitializer<DebugContext>(null);
	                    }

                        public DbSet<Person> Persons { get; set; }
                        public DbSet<House> Houses { get; set; }

                //----------------------------
                        public DbSet<dept> Departements { get; set; }

                        //static DebugContext()
                        //{
                        //    //Database.SetInitializer<YourContext>(new CreateDatabaseIfNotExists<YourContext>()); //Default one
                        //    //Database.SetInitializer<YourContext>(new DropCreateDatabaseIfModelChanges<YourContext>()); //Drop database if changes detected
                        //    //Database.SetInitializer<YourContext>(new DropCreateDatabaseAlways<YourContext>()); //Drop database every times
                        //    //Database.SetInitializer<YourContext>(new CustomInitializer<YourContext>()); //Custom if model changed and seed values
                             
                        //    Database.SetInitializer<DebugContext>(null);
                        //}
            }


        public void BuildbyAddingEntity()
            {
              using(var ctx=new DebugContext())
              {
                  var stud = new Person
                  {
                      Name = "Boyka",
                      BirthDate = new DateTime(2000, 10, 11)
                  };
                  ctx.Persons.Add(stud);
                  ctx.SaveChanges();
              }
            }


        //---Customing
        public class CustomInitializer<T>:DropCreateDatabaseIfModelChanges<DebugContext>
        {
            public override void InitializeDatabase(DebugContext context)
            {
                // En utilisant" DoNotEnsureTransaction", EF ne démarrera pas une transaction avant d'exécuter la commande. 
                //Cela permet à la commande ALTER DATABASE de s'exécuter avec succès.
                /*
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                                                  string.Format("ALTER DATABASE {0} SET MULTI_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

                  */
                //genere un execption qd la bdd n'existe pas encore
                //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                //                                 string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

                if(CheckConnection())
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                                                 string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));
                
               

                base.InitializeDatabase(context);
            }

            protected override void Seed(DebugContext context)
            {
                //integration data reference la 1ère fois quel que soit le nbre d'execution ! 
                //- ici un student, tout en conservant les enregistrements avant appel au "CustomInitializer"
                var stud = new Person
                {
                    Name = "Nicolas",
                    BirthDate = new DateTime(2003, 10, 11)
                };
                context.Persons.Add(stud);
                base.Seed(context);
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
    }




}
