using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsApp_Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            doParrallel();

                //***********ContinueWidth
               
            var taskResult=doContinue().Result;
            Console.WriteLine(taskResult);
                //************************

            Console.WriteLine("Press Any key to exit...");         
            Console.ReadKey();

          
        }

        static async Task<int> doContinue()
        {
            var t = Task.Run(() => { return 42; })
                          .ContinueWith((i) => { return i.Result * 3; });

            return await t;
        }

        static async Task doParrallel()
        {
            List<Task> tasks = new List<Task>();
            //On lance les taches mais onles attend pas...
            tasks.Add(Task.Factory.StartNew(() =>
            {
                Console.Out.WriteLine("On met le minuteur !");
                Thread.Sleep(5000);
                Console.Out.WriteLine("Driiiiiiinnng !");
            }));

            //---On lance simulatenement une autre tache....
            tasks.Add(Task.Factory.StartNew(() =>
            {
                Console.Out.WriteLine("autre tache demarée! !");
                Thread.Sleep(4000);
                Console.Out.WriteLine("autre tache terminée! !");
            }));

            //---On lance simulatenement une 3eme tache....
            tasks.Add(Task.Run(()=>
            {
                Console.Out.WriteLine("third tache demarée! !");
                Thread.Sleep(7000);
                Console.Out.WriteLine("dernière tache terminée! !");
            }));



            await Task.WhenAll(tasks);//puis on attend que tout soit fini
        }


    }
}
