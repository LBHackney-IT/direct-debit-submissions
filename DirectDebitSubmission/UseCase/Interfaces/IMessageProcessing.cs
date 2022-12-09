using DirectDebitSubmission.Boundary;
using System.Threading.Tasks;

namespace DirectDebitSubmission.UseCase.Interfaces
{
    public interface IMessageProcessing
    {
        Task ProcessMessageAsync(EntityEventSns message);
    }
}
