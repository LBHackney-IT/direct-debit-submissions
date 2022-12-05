using System.Text.Json.Serialization;
using System;

namespace DirectDebitSubmission.Infrastructure.Entities
{
    public class TransactionEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("transactionType")]
        public string TransactionType { get; set; }

        [JsonPropertyName("targetId")]
        public Guid TargetId { get; set; }

        [JsonPropertyName("assetId")]
        public Guid? AssetId { get; set; }

        [JsonPropertyName("assetType")]
        public string AssetType { get; set; }

        [JsonPropertyName("tenancyAgreementRef")]
        public string TenancyAgreementRef { get; set; }

        [JsonPropertyName("propertyRef")]
        public string PropertyRef { get; set; }

        [JsonPropertyName("postDate")]
        public DateTime PostDate { get; set; }

        [JsonPropertyName("periodNo")]
        public short PeriodNo { get; set; }

        [JsonPropertyName("transactionSource")]
        public string TransactionSource { get; set; }

        [JsonPropertyName("transactionDate")]
        public DateTime TransactionDate { get; set; }

        [JsonPropertyName("transactionAmount")]
        public decimal TransactionAmount { get; set; }

        [JsonPropertyName("paymentReference")]
        public string PaymentReference { get; set; }

        [JsonPropertyName("bankAccountNumber")]
        public string BankAccountNumber { get; set; }

        [JsonPropertyName("sortCode")]
        public string SortCode { get; set; }

        [JsonPropertyName("isSuspense")]
        public bool IsSuspense { get; set; }

        [JsonPropertyName("paidAmount")]
        public decimal PaidAmount { get; set; }

        [JsonPropertyName("chargedAmount")]
        public decimal ChargedAmount { get; set; }

        [JsonPropertyName("balanceAmount")]
        public decimal BalanceAmount { get; set; }

        [JsonPropertyName("housingBenefitAmount")]
        public decimal HousingBenefitAmount { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("fund")]
        public string Fund { get; set; }

        [JsonPropertyName("financialYear")]
        public short FinancialYear { get; set; }

        [JsonPropertyName("financialMonth")]
        public short FinancialMonth { get; set; }
    }
}
