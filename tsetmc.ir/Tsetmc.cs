using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TseTmc.IR.Model;

namespace Tsetmc.IR
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
            //checks
            if (string.IsNullOrEmpty(symbolName?.Trim()))
                throw new ArgumentException($"{nameof(symbolName)} is null or empty.", nameof(symbolName));

            string tsetmcSymbolId = GetTsetmcSymbolId(symbolName);

            if (string.IsNullOrEmpty(tsetmcSymbolId?.Trim()))
                throw new Exception($"tsetmc symbol id not found.");

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
                rploh.PersianDate = new DateTime(year, month, day);

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

            return CleanPersonOrgHistory(personOrgHistory);
        }

        private static List<PersonOrg> CleanPersonOrgHistory(List<PersonOrg> personOrgHistory)
        {
            List<PersonOrg> cleanHistory = new();
            foreach (var historyItem in personOrgHistory)
            {
                bool thisDateExists = false;
                foreach (var cleanHistoryItem in cleanHistory)
                    if (cleanHistoryItem.PersianDate.Date == historyItem.PersianDate.Date)
                    { thisDateExists = true; break; }

                if (!thisDateExists)
                    cleanHistory.Add(historyItem);
            }
            return cleanHistory;
        }
    }
}
