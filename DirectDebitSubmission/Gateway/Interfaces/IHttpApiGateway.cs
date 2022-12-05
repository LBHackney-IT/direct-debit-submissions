using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Domain;
using System.Threading.Tasks;

namespace DirectDebitSubmission.Gateway.Interfaces
{
    public interface IHttpApiGateway
    {
        public Task<DirectDebit> GetAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest);

        public Task<APIGatewayProxyResponse> UpdateAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest);
    }
}
