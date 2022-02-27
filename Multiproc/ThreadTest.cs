using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ThreadTest
{
    public static void ThreadsWithLock()
    {
        int x = 0;

        object locker = new();  // объект-заглушка

        for (int i = 0; i < 4; i++)
        {
            var thread = new Thread(() =>
            {
                lock (locker) // блокируем данную часть кода от вмешательства из других потоков 
                {
                    for (int j = 0; j < 10; j++)
                    {
                        x++;
                        Console.WriteLine($"x = {x}");
                        Thread.Sleep(100);
                    }
                }
            });
            thread.Start();
        }
    }

    public static void AutoResetEvent()
    {
        int x = 0;

        AutoResetEvent waitHandler = new AutoResetEvent(true);  // объект-событие

        for (int i = 0; i < 4; i++)
        {
            var thread = new Thread(() =>
            {
                waitHandler.WaitOne();  // ожидаем сигнала
                for (int j = 0; j < 10; j++)
                {
                    x++;
                    Console.WriteLine($"x = {x}");
                    Thread.Sleep(100);
                }
                waitHandler.Set();

            });
            thread.Start();
        }
    }
}