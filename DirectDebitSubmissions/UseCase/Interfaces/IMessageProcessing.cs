using DirectDebitSubmissions.Boundary;
using System.Threading.Tasks;

namespace DirectDebitSubmissions.UseCase.Interfaces
{
    public interface IMessageProcessing
    {
        Task ProcessMessageAsync(EntityEventSns message);
    }
}
