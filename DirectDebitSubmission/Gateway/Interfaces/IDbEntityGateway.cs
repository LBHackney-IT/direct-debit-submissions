using DirectDebitSubmission.Domain;
using System;
using System.Threading.Tasks;

namespace DirectDebitSubmission.Gateway.Interfaces
{
    public interface IDbEntityGateway
    {
        Task<DomainEntity> GetEntityAsync(Guid id);
        Task SaveEntityAsync(DomainEntity entity);
    }
}
