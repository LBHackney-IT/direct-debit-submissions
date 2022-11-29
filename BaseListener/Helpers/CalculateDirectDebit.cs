using BaseListener.Boundary.Response;
using System.Collections.Generic;
using System.Linq;

namespace BaseListener.Helpers
{
    public class CalculateDirectDebit
    {
        private IEnumerable<TransactionResponse> _source;
        public CalculateDirectDebit(IEnumerable<TransactionResponse> source)
        {
            this._source = source;
        }

        public decimal Result
        {
            get {
                var data = this._source.Select(k => new { k.PaidAmount, k.HousingBenefitAmount, k.FinancialMonth }).GroupBy(x => new { x.FinancialMonth }, (key, group) => new
                {
                    paidAmount = group.Sum(k => k.PaidAmount),
                    HBAmount = group.Sum(k => k.HousingBenefitAmount),
                    mnth = key.FinancialMonth,
                }).ToList();

                var hbAmount = data.Sum(x => x.HBAmount);
                var paidAmount = data.Sum(x => x.paidAmount);
                var countMonth = data.Count();

                return ((paidAmount - hbAmount) / countMonth);
            }
        }
    }
}
