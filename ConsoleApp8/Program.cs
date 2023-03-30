using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class Program
    {
        public static int a = 0;
        public static AutoResetEvent auto = new AutoResetEvent(true);
        public static Mutex mutex = new Mutex();
        public static Semaphore semaphore = new Semaphore(3, 3);
        public static object lock_ = new object();
        static void Main(string[] args)
        {
            //AutoResetEvent
            //Console.WriteLine("AutoResetEvent");
            //for (int i = 1; i < 6; i++)
            //{
            //    Thread myThread = new Thread(FuncAtuoReset);
            //    myThread.Name = $"Thread {i}";
            //    myThread.Start();
            //}
            //Console.ReadLine();

            ////Mutex
            //Console.WriteLine("Mutex");
            //for (int i = 1; i < 6; i++)
            //{
            //    Thread myThread = new Thread(FuncMutex);
            //    myThread.Name = $"Thread {i}";
            //    myThread.Start();
            //}
            //Console.ReadLine();

            ////Semaphore
            //Console.WriteLine("Semaphore");
            //for (int i = 1; i < 6; i++)
            //{
            //    Thread myThread = new Thread(FuncSemaphore);
            //    myThread.Name = $"Thread {i}";
            //    myThread.Start();
            //}
            //Console.ReadLine();

            //Monitor
            Console.WriteLine("Monitor");
            for (int i = 1; i < 6; i++)
            {
                Thread myThread = new Thread(FuncMonitor);
                myThread.Name = $"Thread {i}";
                myThread.Start();
            }
            Console.ReadLine();
        }
        public static void FuncAtuoReset()
        {
            auto.WaitOne();
            a = 1;
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {a}");
                a++;
            }
            auto.Set();
        }
        public static void FuncMutex()
        {
            mutex.WaitOne();
            a = 1;
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {a}");
                a++;
            }
            mutex.ReleaseMutex();
        }
        public static void FuncSemaphore()
        {
            int count = 3;
            while (count > 0)
            {
                semaphore.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} entered");
                Console.WriteLine($"{Thread.CurrentThread.Name} active");
                Thread.Sleep(1000);
                Console.WriteLine($"{Thread.CurrentThread.Name} left");
                semaphore.Release();

                count--;
                Thread.Sleep(1000);
            }
        }
        public static void FuncMonitor()
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(lock_, ref acquiredLock);
                a = 1;
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {a}");
                    a++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                if (acquiredLock) Monitor.Exit(lock_);
            }

        }

    }
}
