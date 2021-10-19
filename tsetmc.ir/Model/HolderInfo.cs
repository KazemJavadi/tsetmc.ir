using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranTsetmc.Model
{
    public class HolderInfo
    {
        /// <summary>
        /// نام سهامدار
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// کد شناسه ی سهام دار - کد ویژه ای است که حتی به حقیقی ها هم داده می شود
        /// </summary>
        public int Code { get; set; }

    }
}
