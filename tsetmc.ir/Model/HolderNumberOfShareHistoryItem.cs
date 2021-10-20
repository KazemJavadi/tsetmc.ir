using System;

namespace IranTsetmc.Model
{
    public class HolderNumberOfShareHistoryItem
    {
        /// <summary>
        /// تاریخ
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// شمار سهم هایی که در این تاریخ از سهم مشخص دارد
        /// </summary>
        public long NumberOfShares { get; set; }
    }
}
