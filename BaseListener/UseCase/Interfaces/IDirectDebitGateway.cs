using System.Collections.Generic;
using BaseListener.Domain;
using BaseListener.Infrastructure;

namespace BaseListener.UseCase.Interfaces
{
    public interface IDirectDebitGateway
    {
        IEnumerable<DirectDebitDomain> GetDirectDebits();
    }
}
