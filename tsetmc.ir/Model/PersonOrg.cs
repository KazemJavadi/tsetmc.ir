using System;

namespace IranTsetmc.Model
{
    public class PersonOrg
    {
        //Date of data
        public DateTime PersianDate { get; set; }

        //Org - Buy
        //خرید حقوقی
        public long NumberOfOrgBuyers { get; set; }
        public long OrgBuyTotalVolume { get; set; }
        public decimal OrgBuyTotalPrice { get; set; }
        public decimal OrgBuyAvgSharePrice { get; set; }

        //Person - Buy
        //خرید حقیقی
        public long NumberOfPersonBuyers { get; set; }
        public long PersonBuyTotalVolume { get; set; }
        public decimal PersonBuyTotalPrice { get; set; }
        public decimal PersonBuyAvgSharePrice { get; set; }

        //Org - Sell
        //فروش حقوقی
        public long NumberOfOrgSellers { get; set; }
        public long OrgSellTotalVolume { get; set; }
        public decimal OrgSellTotalPrice { get; set; }
        public decimal OrgSellAvgSharePrice { get; set; }

        //Person - Sell
        //فروش حقیقی
        public long NumberOfPersonSellers { get; set; }
        public long PersonSellTotalVolume { get; set; }
        public decimal PersonSellTotalPrice { get; set; }
        public decimal PersonSellAvgSharePrice { get; set; }

        //Org to Person ownership change
        //تغییر مالکیت از حقوقی به حقیقی
        public long OwnershipChangeFromOrgToPerson { get; set; }
    }
}
