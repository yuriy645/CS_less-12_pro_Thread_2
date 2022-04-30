using System;
using System.Threading;

namespace _2_event
{//Преобразуйте пример событийной блокировки таким образом, чтобы вместо ручной обработки
 //   использовалась автоматическая.
    class Program
    {
        static AutoResetEvent auto = new AutoResetEvent(false);

        static void Function()
        {
            Console.WriteLine("Запущен поток {0}", Thread.CurrentThread.Name);

            for (int i = 0; i < 80; i++)
            {
                Console.Write(".");
                Thread.Sleep(20);
            }

            Console.WriteLine("Завершен поток {0}", Thread.CurrentThread.Name);

            auto.Set();  // "Просыпайся!" (при AutoResetEvent зазвонило, разбудило (программа встретила первый код .WaitOne() ) и само затихло ;
                         //                при ManualResetEvent включалось и звенело до выключения вручную)
        }

        static void Main()
        {
            Thread thread = new Thread(Function) { Name = "1" }; // 1-й ПОТОК.
            thread.Start();

            Console.WriteLine("Приостановка выполнения первичного потока.");
            auto.WaitOne(); // засыпание

            Console.WriteLine("Первичный поток возобновил работу.");

            //auto.Reset();   не надо выключать вручную

            thread = new Thread(Function) { Name = "2" }; // 2-й ПОТОК./
            thread.Start();

            Console.WriteLine("Приостановка выполнения первичного потока.");
            auto.WaitOne();

            Console.WriteLine("Первичный поток возобновил и завершил работу.");

            // Delay
            Console.ReadKey();
        }
    }
}
