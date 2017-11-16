using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsApp_TaskAsync
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Diagnostics.Stopwatch minuterie = new System.Diagnostics.Stopwatch();
            minuterie.Start();
            int résultat = Calcul();
            //---faux faux en terme deasynchrone
            //int résultat = Calcul().Result;//-*--beaucoup plus lent...
            minuterie.Stop();
            Console.WriteLine("Résultat du calcul: {0} en {1} ms.", résultat, minuterie.ElapsedMilliseconds);

            /*
            Stopwatch _stopWatch = new Stopwatch();
              _stopWatch.Start();
                    //string val= HelloWorld();
              
           /// var task=  helloWorldAsync();
              //string _val = getResult();
              //Console.WriteLine(_val);
              string _val = getResult();
              Console.WriteLine(_val);

                    //Console.WriteLine(val);
             //Console.WriteLine(task.Result);  
                    Console.WriteLine("");
            // Format and display the TimeSpan value.
            // Get the elapsed time as a TimeSpan value.
                    _stopWatch.Stop();
            TimeSpan ts = _stopWatch.Elapsed;
 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("Press Any key to exit...");
            */
            Console.ReadKey();
        }

        private static string getResult()
        {
            var _f = helloWorldAsync();
            Task<string>.WaitAll(new[] { _f });
            return _f.Result;
        }

        //*************************************
        static async Task<int> f(int n)
        {
            await Task.Delay(2000); // en ms
            await Task.Delay(3000); // en ms
            return n + 1;
        }
        static async Task<int> g(int n)
        {
            await Task.Delay(3000); // en ms
            return n + 2;
        }
        static int Calcul()
        {
            var f_ = f(1);
            var g_ = g(1);
            Task<int>.WaitAll(new[] { f_, g_ });
            return f_.Result + g_.Result;
        }
        //static async Task<int> Calcul()
        //{
        //    var f_ = await f(1);
        //    var g_ = await g(1);
        //    return f_ + g_;
             
        //}
        //******************************

        static string HelloWorld()
        {
            List<string> list = new List<string>();         
            Thread.Sleep(2000);
            list.Add("Hello");
            Thread.Sleep(3000);
            list.Add("World");
            
            return string.Join("", list);

        }


        static string  getValue(string _val)
        {
            Console.WriteLine(_val);
                return _val;
        }


        //-Le mot clé async présent au niveau de la signature de la méthode informe seulement le compilateur que l'on souhaite implémenter une méthode asynchrone
        //-et ainsi d'autoriser l'utilisation du mot clé await au sein même de la méthode.
        //Ainsi, la présence de async transforme automatiquement le type de retour T en Task<T>.
        static async Task<string>  helloWorldAsync()
        {
            List<string> list = new List<string>();
            await Task.Delay(2000);
            list.Add("hello");
            await Task.Delay(3000);
            list.Add("kpleus !");

             

            return string.Join("-", list);
        }

        //static  Task<string> getMessageAsync(List<string> _lst) 
        //{
    
        //    return Task.Run<string>(() => getValue(string.Join("-", _lst)));

        //}

     

    }
}
