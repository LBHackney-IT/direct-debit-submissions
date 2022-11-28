using System;
using System.ComponentModel.DataAnnotations;

namespace BaseListener.Domain
{
    public class BacsDataEntity
    {
        public int SortCodeResident { get; set; }
        public string AccountNumberResident { get; set; }
        public string TransactionType { get; set; } = "017";
        public int SortCodeHackney { get; set; }
        public int AccountNumberHackney { get; set; }
        public string FreeFormat { get; set; } = "0000";
        public string AccountNameHackney { get; set; }
        public decimal? RentPayment { get; set; }
        public string PaymentReferenceHackney { get; set; }
        public string FundHackney { get; set; } = "HSGRENT";
        public string AccountNameResident { get; set; }
        public int ProcessingDate { get; set; } //bddYYY

    }
}
