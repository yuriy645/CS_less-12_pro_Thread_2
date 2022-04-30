using System;
using System.Threading;

namespace _3_Mutex
{//Создайте приложение, которое может быть запущено только в одном экземпляре (используя
 //   именованный Mutex).
    class Program
    {
        static Mutex mutex = new Mutex(false, "MyMutex:VGHFVCICG76AD45BAUNCNGCZYUFCNUYCL"); //ОДИН ТУРНИКЕТ НА ВСЕ ПРОЦЕССЫ

        static void Main()
        {
            Thread[] threads = new Thread[5];

            for (int i = 0; i < 5; i++)
            {
                threads[i] = new Thread(Function);
                threads[i].Name = i.ToString();
                Thread.Sleep(500); // Потоки из разных процессов успеют стать в очередь вперемешку.
                threads[i].Start();
            }

            // Delay
            Console.ReadKey();
        }

        static void Function()
        {
            mutex.WaitOne();//щелк турникет, зашли на мостик . другой поток не может запустится, пока не завершится этот

            Console.WriteLine("Поток {0} зашел в защищенную область.", Thread.CurrentThread.Name);
            Thread.Sleep(2000); //ТУТ МОЖЕТ БЫТЬ ДОСТУП К РЕСУРСУ, С КОТОРЫМ МОЖЕТ ОДНОВРЕМЕННО РАБОТАТЬ ТОЛЬКО ОДИН ПОТОК  ОДНОЙ ПОГРАММЫ !!!
            Console.WriteLine("Поток {0}  покинул защищенную область.\n", Thread.CurrentThread.Name);

            mutex.ReleaseMutex(); //щелк, освободили ресурс
        }
    }
}
