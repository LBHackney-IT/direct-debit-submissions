using System;
using System.Collections.Generic;
using System.Text;

namespace DirectDebitSubmission.Boundary.Request
{
    public class DirectDebitUpdateRequest
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
