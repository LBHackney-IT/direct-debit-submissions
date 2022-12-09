using System.Text.Json.Serialization;
using System;

namespace DirectDebitSubmission.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string TransactionType { get; set; }

        public Guid TargetId { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal HousingBenefitAmount { get; set; }

        public short FinancialYear { get; set; }

        public short FinancialMonth { get; set; }
    }
}
