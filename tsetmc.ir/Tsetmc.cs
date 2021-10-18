using HtmlAgilityPack;
using IranTsetmc.Model;
using IranTsetmc.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace IranTsetmc
{
    public class Tsetmc
    {
        private static string GetRequestResult(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new(stream);
            string result = reader.ReadToEnd();

            return result;
        }

        private static string CheckSymbolNameValidityAndGetTsetmcId(string symbolName)
        {
            //checks
            if (string.IsNullOrEmpty(symbolName?.Trim()))
                throw new ArgumentException($"{nameof(symbolName)} is null or empty.", nameof(symbolName));

            string tsetmcSymbolId = GetTsetmcSymbolId(symbolName);

            if (string.IsNullOrEmpty(tsetmcSymbolId?.Trim()))
                throw new Exception($"tsetmc symbol id not found.");
            return tsetmcSymbolId;
        }

        public static string SearchShare(string symbolName)
        {
            throw new NotImplementedException();
        }

        public static string GetTsetmcSymbolId(string symbolName)
        {
            string url = $"http://tsetmc.ir/tsev2/data/search.aspx?skey={symbolName}";

            string result = GetRequestResult(url);

            string[] resultItems = result.Split(',');

            foreach (string resultItem in resultItems)
                if (resultItem.ToCharArray().Where(c => Char.IsLetter(c)).ToArray().Length == 0)
                    return resultItem;

            return null;
        }

        public static List<PersonOrg> GetPersonOrgHistory(string symbolName)
        {
            string tsetmcSymbolId = CheckSymbolNameValidityAndGetTsetmcId(symbolName);

            //request url
            string url = $"http://www.tsetmc.com/tsev2/data/clienttype.aspx?i={tsetmcSymbolId}";

            string result = GetRequestResult(url);

            if (result == string.Empty) return new();

            string[] daysData = result.Split(";");

            List<PersonOrg> personOrgHistory = new();
            foreach (string dayData in daysData)
            {
                string[] dayDataUnits = dayData.Split(',');
                PersonOrg rploh = new();
                //date
                //date
                int year = int.Parse(dayDataUnits[0].Substring(0, 4));
                int month = int.Parse(dayDataUnits[0].Substring(4, 2));
                int day = int.Parse(dayDataUnits[0].Substring(6, 2));
                rploh.Date = new DateTime(year, month, day);

                //person/org - sell/buy - count
                rploh.NumberOfOrgBuyers = long.Parse(dayDataUnits[1]);
                rploh.NumberOfPersonBuyers = long.Parse(dayDataUnits[2]);
                rploh.NumberOfPersonSellers = long.Parse(dayDataUnits[3]);
                rploh.NumberOfOrgSellers = long.Parse(dayDataUnits[4]);

                //person/org - sell/buy - volume
                rploh.PersonBuyTotalVolume = long.Parse(dayDataUnits[5]);
                rploh.OrgBuyTotalVolume = long.Parse(dayDataUnits[6]);
                rploh.PersonSellTotalVolume = long.Parse(dayDataUnits[7]);
                rploh.OrgSellTotalVolume = long.Parse(dayDataUnits[8]);

                //person/org - sell/buy - total price
                rploh.PersonBuyTotalPrice = decimal.Parse(dayDataUnits[9]);
                rploh.OrgBuyTotalPrice = decimal.Parse(dayDataUnits[10]);
                rploh.PersonSellTotalPrice = decimal.Parse(dayDataUnits[11]);
                rploh.OrgSellTotalPrice = decimal.Parse(dayDataUnits[12]);

                personOrgHistory.Add(rploh);
            }

            return personOrgHistory.CleanPersonOrgHistory();
        }



        public static ShareIdInfo GetShareIdInfo(string symbolName)
        {
            string tsetmcSymbolId = CheckSymbolNameValidityAndGetTsetmcId(symbolName);
            string url = $@"http://tsetmc.ir/Loader.aspx?Partree=15131M&i={tsetmcSymbolId}";

            string html = GetRequestResult(url);
            HtmlDocument doc = new();
            doc.LoadHtml(html);
            var items = doc.DocumentNode
                .SelectSingleNode("/html/body/div[4]/form/span/div/div[2]/table/tbody")
                .ChildNodes.Where(n => n.Name != "#text")
                .Select(n => n.ChildNodes.Where(cn => cn.Name != "#text").ToList()[1].InnerText.Trim()).ToArray();
            List<HtmlNode> myNodes = new();

            return new ShareIdInfo
            {
                SymbolCode12Digit = items[0],
                SymbolCode5Digit = items[1],
                LatinNameOfTheCompany = items[2],
                CompanyCode4Digit = items[3],
                CompanyName = items[4],
                PersianSymbol = items[5],
                PersianSymbol30Digit = items[6],
                CompanyCode12Digit = items[7],
                Bazaar = items[8],
                BoardCode = items[9],
                IndustryGroupCode = items[10],
                IndustryGroup = items[11],
                IndustrySubGroupCode = items[12],
                IndustrySubGroup = items[13]
            };
        }
    }
}
