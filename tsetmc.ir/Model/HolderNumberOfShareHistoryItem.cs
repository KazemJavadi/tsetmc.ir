using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
