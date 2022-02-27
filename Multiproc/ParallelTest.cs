using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ParallelTest
{
    public static void Invoke()
    {
        Parallel.Invoke(() =>
        {
            Thread.Sleep(2000);
            Console.WriteLine($"Factorial {5} is {Calc.Factorial(5)}");
        },
        () =>
        {
            Thread.Sleep(3000);
            Console.WriteLine($"Factorial {8} is {Calc.Factorial(8)}");
        },
        () =>
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Factorial {12} is {Calc.Factorial(12)}");
        },
        () =>
        {
            Thread.Sleep(4000);
            Console.WriteLine("Hello, how are you?");
        });
    }

    public static void For()
    {
        Parallel.For(0, 10, (i) =>
        {
            Random rnd = new Random();
            var sleep = rnd.Next(1, 5);
            Thread.Sleep(sleep * 1000);
            Console.WriteLine($"Factorial {i} is {Calc.Factorial(i)}");
        });
    }

    public static void Foreach()
    {
        string[] names = { "Inna", "Veronica", "Zhenya", "Violetta"};

        Parallel.ForEach(names, (name) =>
        {
            string partnerName = Meeting.PickupPartner(name, Gender.Male);
            Console.WriteLine($"The best partner for {name} is {partnerName}");
        });
    }
}
