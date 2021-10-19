using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTsetmc.Model
{
    public class ShareHolderInfo
    {
        public HolderInfo Holder { get; set; }
        public ShareIdInfo ShareInfo { get; set; }
        public long NumberOfOwnedShares { get; set; }
        public double PercentageOfOwnedShares { get; set; }
        public long ChangeOfOwnership { get; set; }
        public List<HolderNumberOfShareHistoryItem> NumberOfShareHistory { get; set; }
        public List<HolderOtherShareInfo> OtherSharesInfo { get; set; }
    }
}
