using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Calc
{
    public static int Sum(int a, int b)
    {
        Thread.Sleep(5000);
        return a + b;
    }

    public static int Factorial(int n)
    {
        int res = 1;
        for (int i = 1; i <= n; i++)
            res *= i;
        return res;
    }
}
