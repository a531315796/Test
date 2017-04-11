using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Test
{
    class Program
    {
        static bool done;
        static object locker = new object();
        static void Main(string[] args)
        {

            CreateFile("abcdefghijklmnopqrstuvwxyz");


        }

        private static void x() {
            lock (locker)
            {
                if (!done)
                {
                    Console.WriteLine("Done：" + done);
                    done = true;
                }
            }
             
        }

        private static void CreateFile(string str) {
            string path = "d://test.txt";
            if (File.Exists(path)) {
                File.Delete(path);
            }
            //FileStream stream = new FileStream(path, FileMode.CreateNew);
            using (StreamWriter sw= File.AppendText(path)) {
                sw.Write(str);
            }
        }
    }
}
