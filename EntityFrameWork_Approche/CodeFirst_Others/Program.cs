using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Others
{
    class Program
    {
       static  myModel _myModel = new myModel();
        static void Main(string[] args)
        {
            Console.WriteLine("Check Connection");
            //var result=myModel.CheckConnection();
            //if (!result)
            //{
            //    Console.WriteLine("Impossible d'atteindre le serveur de base de donnés...!");

            //}
            //else
            //{
            //    Console.WriteLine("Acces Ok Server Database...!");
            //}
                Console.WriteLine("Create database & prise ne compte des datas de references...");
                _myModel.BuildDatabase();

                Console.WriteLine("Insertion to  database...");
                _myModel.BuildbyAddingEntity();

            //}
            Console.WriteLine("Press Any key to exit...");
            Console.ReadKey();
        }
    }
}
