using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTsetmc.Model
{
    public class DayTradeHistory
    {
        public DateTime Date { get; set; }
        public List<DayTradeHistoryItem> DayHistory { get; set; }
    }
}
