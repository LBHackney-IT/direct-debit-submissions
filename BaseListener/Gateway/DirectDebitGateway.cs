using System.Collections.Generic;
using BaseListener.Domain;
using BaseListener.UseCase.Interfaces;

namespace BaseListener.Gateway
{
    public class DirectDebitGateway : IDirectDebitGateway
    {
        public IEnumerable<DirectDebitDomain> GetDirectDebits()
        {
            throw new System.NotImplementedException();
        }
    }
}
