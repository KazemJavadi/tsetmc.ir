namespace IranTsetmc.Model
{
    public class HolderOtherShareInfo
    {
        /// <summary>
        /// مشخصات سهم
        /// </summary>
        public ShareIdInfo ShareInfo { get; set; }
        /// <summary>
        /// شمار سهم هایی که دارد
        /// </summary>
        public long NumberOfOwnedShares { get; set; }
        /// <summary>
        /// میزان سهم هایی که دارد به درصد
        /// </summary>
        public double PercentageOfOwnedShares { get; set; }
    }
}
