using IranTsetmc;
using System;
using static System.Console;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Tsetmc.GetDayTradeHistory("کیمیاتک", DateTime.Now.AddDays(-1));
            ReadLine();
        }
    }
}
