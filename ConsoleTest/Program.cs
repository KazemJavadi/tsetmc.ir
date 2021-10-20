using IranTsetmc;
using static System.Console;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Tsetmc.GetShareHoldersInfo("خودرو");
            ReadLine();
        }
    }
}
