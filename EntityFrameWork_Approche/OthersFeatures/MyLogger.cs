using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthersFeatures
{
    public class MyLogger
    {
        public static void Log(string application,string message)
        {
            Console.WriteLine("Application :{0} , EF Message :{1}", application, message);
            string path = @"D:\App\LogEntityFrameWork.txt";
            using(StreamWriter writer=new StreamWriter(path,true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
