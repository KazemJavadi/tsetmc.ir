using System;
using System.Collections.Generic;

namespace IranTsetmc.Help
{
    internal static class Helper
    {
        public static (int year, int month, int day) SeprateDateParts(string dateString)
        {
            int year = int.Parse(dateString.Substring(0, 4));
            int month = int.Parse(dateString.Substring(4, 2));
            int day = int.Parse(dateString.Substring(6, 2));

            return (year, month, day);
        }

        public static string CombineDateTime(DateTime date) => $"{date.Year}{date.Month}{date.Day}";
    }
}
