using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmission.Gateway.Interfaces
{
    public interface IHttpApiGateway
    {
        public Task<IEnumerable<Transaction>> GetAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest);

        public Task<APIGatewayProxyResponse> UpdateAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest);
    }
}
