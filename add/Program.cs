using System;
using System.IO;
using System.Threading;

namespace add
{// Создайте Semaphore, осуществляющий контроль доступа к ресурсу из нескольких потоков.
 //   Организуйте упорядоченный вывод информации о получении доступа в специальный*.log  файл.
    class Program
    {
        static Semaphore pool;

        static void Function(object number)
        {
            pool.WaitOne(); // я захожу на мост
            File.AppendAllText("txt.log", string.Format("Поток {0} занял слот семафора. \n", number));

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("Поток {0} занял слот семафора. \n", number);

            Thread.Sleep(2000);
            
            File.AppendAllText("txt.log", string.Format("Поток {0} -----> освободил слот. \n", number));

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Поток {0} -----> освободил слот. \n", number);

            pool.Release(); 
        }

        public static void Main()
        {
            pool = new Semaphore(1, 4, "MySemafore");

            for (int i = 1; i <= 8; i++)
            {
                new Thread(Function).Start(i);
                //Thread.Sleep(500);  // Чтоб потоки успели успели создастся в разных процессах
            }

            Console.ReadKey();
        }
    }
}
