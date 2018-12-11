using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.IO;


namespace OOP_lab_15
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            //task1
            foreach (Process process in Process.GetProcesses())
            {
                Console.WriteLine("ID--- {0}   Name--- {1}  Priority--- {2}", process.Id,
                    process.ProcessName, process.BasePriority);
            }
            
            //task2
            AppDomain domain = AppDomain.CurrentDomain;
            AppDomainSetup setup = domain.SetupInformation;
            Console.WriteLine("Name - {0}", domain.FriendlyName);
            Console.WriteLine("Base DIrectory - {0} \n", domain.BaseDirectory);
            Console.WriteLine("SetupInformation of domain:");
            Console.WriteLine("Name of directory - {0}", setup.ApplicationBase);
            Console.WriteLine("Activator args - {0}", setup.ActivationArguments);            
            Console.WriteLine("Loader optimize - {0}", setup.LoaderOptimization);

            Assembly[] assemb = domain.GetAssemblies();
            foreach (Assembly asm in assemb)
            {
                Console.WriteLine(asm.GetName().Name);
            }

            AppDomain secondDomain = AppDomain.CreateDomain("Secondary domain");
            secondDomain.AssemblyLoad += Domain_AssemblyLoad;
            secondDomain.DomainUnload += SecondaryDomain_DomainUnload;

            //task3
            Thread task = new Thread(new ParameterizedThreadStart(Count));
            task.Start(10);
            Thread.Sleep(1000);
            Console.WriteLine("Thread sleep");
            task.Suspend();
            Console.WriteLine("Name: " + task.Name);
            Console.WriteLine("Priority: " + task.Priority);
            Console.WriteLine("Thread: " + task.ThreadState);
            Thread.Sleep(1000);
            task.Resume();
            Console.WriteLine("Thread resume");
            
            //task4
            Thread.Sleep(5000);
            Thread thread1 = new Thread(new ParameterizedThreadStart(Nechet));
            thread1.Name = "First thread";

            Thread thread2 = new Thread(new ParameterizedThreadStart(Chet));
            thread2.Name = "Second thread";
            thread1.Start(10);
            Thread.Sleep(500);
            thread2.Start(10);
            thread2.Priority = ThreadPriority.Highest;
            Thread.Sleep(10000);

            Console.WriteLine("\nFirst even - then odd");

            Thread thread3 = new Thread(new ParameterizedThreadStart(Nechet2));
            thread3.Name = "First thread";

            Thread thread4 = new Thread(new ParameterizedThreadStart(Chet2));
            thread4.Name = "Second thread";

            thread4.Start(10);
            thread3.Start(10);
            
           
            //task5            
            TimerCallback callback = new TimerCallback(TimerFunc);
            Timer timer = new Timer(callback, null, 0, 2000);
            
        }
        
        

        private static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Domain unload from process");
        }

        private static void Domain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Assembly load");
        }
        
        
        private const string Path = @"D:\Threads.txt";
        
        public static void Count(object n)
        {

            for (var i = 1; i < (int)n; i++)
            {
                Console.WriteLine("Second thread:");
                Console.WriteLine(i);
                Thread.Sleep(400);


                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine(i);
                }
            }

        }
        
        

        public static void Nechet2(object n)
        {

            mut.WaitOne();
            x = 1;
            for (int i = x; i <= (int)n; i = i + 2)
            {
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);
                x++;


                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);
                }

                Thread.Sleep(1000);


            }

            mut.ReleaseMutex();

        }


        public static void Nechet(object n)
        {
            x = 1;
            for (int i = x; i <= (int)n; i = i + 2)
            {
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);
                x++;


                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);
                }

                Thread.Sleep(1000);
            }       
        }
        
        
        public static int x;

        static Mutex mut = new Mutex();

        public static void Chet2(object n)
        {
            mut.WaitOne();
            x = 2;
            for (int i = x; i <= (int)n; i = i + 2)
            {
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);



                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);
                }

                Thread.Sleep(1000);

            }

            mut.ReleaseMutex();
        }

        public static void Chet(object n)
        {
         
            x = 2;
            for (int i = x; i <= (int)n; i = i + 2)
            {
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);



                using (StreamWriter sw = new StreamWriter(Path, true))
                {
                    sw.WriteLine(Thread.CurrentThread.Name + " --- x = " + i);
                }

                Thread.Sleep(1000);

            }

         
        }




        private static void TimerFunc(object c)
        {
            Console.WriteLine("It's Timer");
        }

    }
}



