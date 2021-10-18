using IranTsetmc.Model;
using System;
using System.Collections.Generic;

namespace IranTsetmc.Extensions
{
    internal static class PersonOrgExtensions
    {
        public static List<PersonOrg> ToListOfPersonOrgHistory(this string tsetmcPersonOrgHistoryDate)
        {
            string[] daysData = tsetmcPersonOrgHistoryDate.Split(";");

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

            return personOrgHistory;
        }
        public static List<PersonOrg> CleanPersonOrgHistory(this List<PersonOrg> personOrgHistory)
        {
            List<PersonOrg> cleanHistory = new();
            foreach (var historyItem in personOrgHistory)
            {
                bool thisDateExists = false;
                foreach (var cleanHistoryItem in cleanHistory)
                    if (cleanHistoryItem.Date.Date == historyItem.Date.Date)
                    { thisDateExists = true; break; }

                if (!thisDateExists)
                    cleanHistory.Add(historyItem);
            }
            return cleanHistory;
        }
    }
}
