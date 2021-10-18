using IranTsetmc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTsetmc.Extensions
{
    internal static class ShareIdInfoExtensions
    {
        public static ShareIdInfo ToShareIdInfo(this string[] shareIdInofItmes)
        {
            return new ShareIdInfo
            {
                SymbolCode12Digit = shareIdInofItmes[0],
                SymbolCode5Digit = shareIdInofItmes[1],
                LatinNameOfTheCompany = shareIdInofItmes[2],
                CompanyCode4Digit = shareIdInofItmes[3],
                CompanyName = shareIdInofItmes[4],
                PersianSymbol = shareIdInofItmes[5],
                PersianSymbol30Digit = shareIdInofItmes[6],
                CompanyCode12Digit = shareIdInofItmes[7],
                Bazaar = shareIdInofItmes[8],
                BoardCode = shareIdInofItmes[9],
                IndustryGroupCode = shareIdInofItmes[10],
                IndustryGroup = shareIdInofItmes[11],
                IndustrySubGroupCode = shareIdInofItmes[12],
                IndustrySubGroup = shareIdInofItmes[13]
            };
        }
    }
}
