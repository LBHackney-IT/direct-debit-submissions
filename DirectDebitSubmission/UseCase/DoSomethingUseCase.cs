using DirectDebitSubmission.Boundary;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Gateway.Interfaces;
using DirectDebitSubmission.Infrastructure.Exceptions;
using DirectDebitSubmission.UseCase.Interfaces;
using Hackney.Core.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DirectDebitSubmission.UseCase
{
    [ExcludeFromCodeCoverage]
    public class DoSomethingUseCase : IDoSomethingUseCase
    {
        private readonly IDbEntityGateway _gateway;

        public DoSomethingUseCase(IDbEntityGateway gateway)
        {
            _gateway = gateway;
        }

        [LogCall]
        public async Task ProcessMessageAsync(EntityEventSns message)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));

            // TODO - Implement use case logic
            DomainEntity entity = await _gateway.GetEntityAsync(message.EntityId).ConfigureAwait(false);
            if (entity is null) throw new EntityNotFoundException<DomainEntity>(message.EntityId);

            entity.Description = "Updated";

            // Save updated entity
            await _gateway.SaveEntityAsync(entity).ConfigureAwait(false);
        }
    }
}
