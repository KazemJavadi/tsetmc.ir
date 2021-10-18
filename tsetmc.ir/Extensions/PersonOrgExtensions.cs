using IranTsetmc.Model;
using System.Collections.Generic;

namespace IranTsetmc.Extensions
{
    internal static class PersonOrgExtensions
    {
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
