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

        /// <summary>
        /// داده های (اطلاعات) خرید و فروش حقیقی و حقوقی سهم
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static List<PersonOrg> GetPersonOrgHistory(string symbolName)
        {
            string tsetmcSymbolId = CheckSymbolNameValidityAndGetTsetmcId(symbolName);

            //request url
            string url = $"http://www.tsetmc.com/tsev2/data/clienttype.aspx?i={tsetmcSymbolId}";

            string result = GetRequestResult(url);

            if (result == string.Empty) return new();

            return result.ToListOfPersonOrgHistory().CleanPersonOrgHistory();
        }


        /// <summary>
        /// داده های (اطلاعات) شناسه ی نماد
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static ShareIdInfo GetShareIdInfo(string symbolName)
        {
            string tsetmcSymbolId = CheckSymbolNameValidityAndGetTsetmcId(symbolName);
            string url = $@"http://tsetmc.ir/Loader.aspx?Partree=15131M&i={tsetmcSymbolId}";

            string html = GetRequestResult(url);
            HtmlDocument doc = new();
            doc.LoadHtml(html);
            var shareIdInfoItems = doc.DocumentNode
                .SelectSingleNode("/html/body/div[4]/form/span/div/div[2]/table/tbody")
                .ChildNodes.Where(n => n.Name != "#text")
                .Select(n => n.ChildNodes.Where(cn => cn.Name != "#text").ToList()[1].InnerText.Trim()).ToArray();

            return shareIdInfoItems.ToShareIdInfo();
        }

        public static string GetHoldersInfo(string symbolName)
        {
            ShareIdInfo shareIdInfo = GetShareIdInfo(symbolName);
            string url = $@"http://tsetmc.ir/Loader.aspx?Partree=15131T&c={shareIdInfo.CompanyCode12Digit}";
            string html = GetRequestResult(url);
            HtmlDocument doc = new();
            doc.LoadHtml(html);
            var shareIdInfoItems = doc.DocumentNode
            .SelectSingleNode("/html/body/div[4]/form/span/div/div[2]/table/tbody")
            .ChildNodes.Where(n => n.Name != "#text")
            .Select(n => n.ChildNodes.Where(cn => cn.Name != "#text").ToList()[1].InnerText.Trim()).ToArray();
            throw new NotImplementedException();
        }
    }
}
