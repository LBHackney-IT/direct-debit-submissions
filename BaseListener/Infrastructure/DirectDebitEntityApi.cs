using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseListener.Infrastructure
{
    public class DirectDebitEntityApi
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int TargetType { get; set; } //TODO: Should this be enum like in DD API?
        public Guid TargetId { get; set; }
        public DateTime? FirstPaymentDate { get; set; }
        public string PaymentReference { get; set; }
        public string AccountNumber { get; set; }
        public string Fund { get; set; }
        public int? Acc { get; set; } = 0;
        public int? Trans { get; set; } = 17;
        [StringLength(18)]
        public string AccountHolder { get; set; }
        public string BankAccountNumber { get; set; }
        public string BranchSortCode { get; set; } //TODO: This should be an integer
        public string ServiceUserNumber { get; set; }
        public string Reference { get; set; }
        public string BankOrBuildingSocietyTo { get; set; }
        public string BankOrBuildingSocietyName { get; set; }
        public string BankOrBuildingSocietyAddress1 { get; set; }
        public string BankOrBuildingSocietyAddress2 { get; set; }
        public string BankOrBuildingSocietyAddress3 { get; set; }
        public string BankOrBuildingSocietyPostcode { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AdditionalAmount { get; set; }
        public decimal? FixedAmount { get; set; }
        public int PreferredDate { get; set; }
        public string Status { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancellationDate { get; set; }
        public bool IsPaused { get; set; }
        public DateTime? PauseDate { get; set; }
        public DateTime? PauseTillDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IEnumerable<object> DirectDebitMaintenance { get; set; } //TODO: Should this have a class like in DD API?
    }
}
