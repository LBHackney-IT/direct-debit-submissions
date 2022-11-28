using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseListener.Domain
{
    public class DirectDebitDomain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PaymentReference { get; set; }
        public string AccountNumber { get; set; }
        public string Fund { get; set; }
        public int? Acc { get; set; } = 0;
        public int? Trans { get; set; } = 17;
        [StringLength(18)]
        public string AccountHolder { get; set; }
        public string BranchSortCode { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AdditionalAmount { get; set; }
        public decimal? FixedAmount { get; set; }
        public int PreferredDate { get; set; }
    }
}
