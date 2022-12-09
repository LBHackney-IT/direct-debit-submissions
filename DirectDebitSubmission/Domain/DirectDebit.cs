namespace DirectDebitSubmission.Domain
{
    public class DirectDebit
    {
        public decimal? Amount { get; set; }
        public decimal? AdditionalAmount { get; set; }
        public decimal? FixedAmount { get; set; }
        public int? PreferredDate { get; set; }
        public string Status { get; set; }
        public int? PauseDuration { get; set; }
        public string Reason { get; set; }
    }
}
