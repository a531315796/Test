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
        bool done;
        string str = "";
        static object locker = new object();
        static int balance = 1000;
        static Random r = new Random();
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                threads[i] = new Thread(new ThreadStart(ConsoleWrite));
                threads[i].Name = "线程"+(i+1);
                threads[i].IsBackground = true;
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
        }


        static void ConsoleWrite() {
            if (balance < 0)
            { 
                Console.WriteLine("余额不足。");
                return;
            }
            lock (locker)
            {
                for (int i = 0; i < 100; i++)
                {
                    int num = r.Next(-10, 100);
                    if (balance - num < 0)
                    {
                        Console.WriteLine("存款：{0}, 取现：{1}, 余额不足。", balance, num);
                        return;
                    }
                    Console.WriteLine("存款：{0}, 取现：{1}, 余额：{2}。", balance, num, balance- num);
                    balance = balance - num;
                }
            }
        }

        private void CreateFile(string str) {
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
