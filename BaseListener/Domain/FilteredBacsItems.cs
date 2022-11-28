using System.Collections.Generic;

namespace BaseListener.Domain
{
    public class FilteredBacsItems
    {
        public class InvalidDirectDebitItem
        {
            public DirectDebitDomain InvalidDirectDebitDomain;
            public string Information;
        }
        public List<BacsDataEntity> ValidBacsItems { get; set; } = new List<BacsDataEntity>();
        public List<InvalidDirectDebitItem> InvalidDirectDebitItems { get; set; } = new List<InvalidDirectDebitItem>();
    }
}
