using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Meeting
{
    private static int maxTime = 8;

    private readonly static string[] maleNames = { "Vova", "Petya", "Rudolf", "Zhenya" };
    private readonly static string[] femaleNames = { "Liza", "Vasilisa", "Natasha", "Olga" };

    public static string PickupPartner(string name, Gender gender)
    {
        Random rnd = new Random();
        var sleep = rnd.Next(1, maxTime);
        Thread.Sleep(sleep * 1000);
        switch(gender)
        {
            case Gender.Male:
                var mIndex = rnd.Next(0, maleNames.Count() - 1);
                return maleNames[mIndex];
            case Gender.Female:
                var fIndex = rnd.Next(0, femaleNames.Count() - 1);
                return femaleNames[fIndex];
        }

        return String.Empty;
    }
}

public enum Gender
{
    Male = 0,
    Female = 1,
}
