using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTsetmc.Model
{
    public class DayTradeHistoryItem
    {
        public TimeSpan Time { get; set; } 
        public long Volume { get; set; }
        public decimal Price { get; set; }
    }
}
