using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectDebitSubmission.Factories
{
    public static class TransactionFactory
    {
        public static Transaction ToDomain(this TransactionEntity entity) => entity == null ? null : new Transaction
        {
            Id = entity.Id,
            TargetId = entity.TargetId,
            TransactionType = entity.TransactionType,
            FinancialMonth = entity.FinancialMonth,
            FinancialYear = entity.FinancialYear,
            HousingBenefitAmount = entity.HousingBenefitAmount,
            PaidAmount = entity.PaidAmount,
            
        };

        public static IEnumerable<Transaction> ToDomain(this IEnumerable<TransactionEntity> databaseEntity)
        {
            return databaseEntity.Select(p => p.ToDomain()).ToList();
        }
    }
}
