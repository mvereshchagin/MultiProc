using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class TaskTest
{
    public static void ThreeWaysOfStartTasks()
    {
        Task task = new Task(() =>
        {
            Console.WriteLine("Start with instance creation");
            Thread.Sleep(1000);
            Console.WriteLine("End with instance creation");
        });
        task.Start();

        Task.Run(() => 
        {
            Console.WriteLine("Start with run");
            Thread.Sleep(1000);
            Console.WriteLine("End with run");
        });

        Task.Factory.StartNew(() => 
        {
            Console.WriteLine("Start through factory");
            Thread.Sleep(1000);
            Console.WriteLine("End through factory");
        });
    }

    public static void InnerTask()
    {
        Console.WriteLine("Method start");

        Task outerTask = new Task(() =>
        {
            Console.WriteLine("Start outer task");

            var innerTask = new Task(() =>
                {
                    Console.WriteLine("Inner task start");
                    Thread.Sleep(4000);
                    Console.WriteLine("Inner task end");
                }, TaskCreationOptions.AttachedToParent);
            innerTask.Start();

            innerTask.Wait();

            Thread.Sleep(2000);

            Console.WriteLine("End outer task");
        });
        outerTask.Start();

        outerTask.Wait();

        Console.WriteLine("Method end");
    }

    public static void ListOfTasks()
    {
        Console.WriteLine("Method start");

        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 5; i++)
        {
            Task task = new Task(() =>
            {
                Random rnd = new Random();
                int sleep = rnd.Next(1, 5);
                Console.WriteLine($"Start task");
                Thread.Sleep(sleep * 1000);
                Console.WriteLine($"End task");
            });
            tasks.Add(task);
            task.Start();
        }

        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("Method end");
    }

    public static void TestResults()
    {
        //var task = new Task<int>(() =>
        //{
        //    return Sum(3, 6);
        //});

        var task = new Task<int>(() => Calc.Sum(3, 6));
        task.Start();
        var res = task.Result;
        Console.WriteLine(res);
    }

    public static void TestContinue()
    {
        var task1 = new Task<int>(() => Calc.Sum(3, 6));
        var task2 = task1.ContinueWith<int>((t) => Calc.Sum(t.Result, 4));
        task1.Start();
        var res = task2.Result;
        Console.WriteLine($"The result is {res}");
    }

    public static void TestCancelation()
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;

        Task task = new Task(() =>
        {
            string[] names = { "Adolf", "Iosif", "Winston", "Boris", "Donald", "Barak", "Dmitry", "Ernesto" };

            foreach(var name in names)
            {
                if(token.IsCancellationRequested)
                {
                    Console.WriteLine("Operation has been cancelled");
                    return;
                }
                string partnerName = Meeting.PickupPartner(name, Gender.Female);
                Console.WriteLine($"The best partner for {name} is {partnerName}");
            }
        }, token);
        task.Start();

        Console.WriteLine("To cancel, please, print \"exit\" or \"cancel\"");
        var answer = Console.ReadLine();
        if ((new string[] { "exit", "Exit", "eexit", "cancel" }).Contains(answer))
            cancelTokenSource.Cancel();
    }
}
