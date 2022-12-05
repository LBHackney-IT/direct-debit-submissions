using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DirectDebitSubmission.Factories
{
    public static class DirectDebitFactory
    {
        public static DirectDebit ToDomain(this IEnumerable<Transaction> source)
        {
            var data = source.Select(k => new { k.PaidAmount, k.HousingBenefitAmount, k.FinancialMonth }).GroupBy(x => new { x.FinancialMonth }, (key, group) => new
            {
                paidAmount = group.Sum(k => k.PaidAmount),
                HBAmount = group.Sum(k => k.HousingBenefitAmount),
                mnth = key.FinancialMonth,
            }).ToList();

            var hbAmount = data.Sum(x => x.HBAmount);
            var paidAmount = data.Sum(x => x.paidAmount);
            var countMonth = data.Count();

            return new DirectDebit() { Amount = (paidAmount - hbAmount) / countMonth };
        }
    }
}
