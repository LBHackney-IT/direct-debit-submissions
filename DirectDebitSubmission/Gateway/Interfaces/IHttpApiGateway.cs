using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Boundary.Response;
using DirectDebitSubmission.Helpers.GeneralModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmission.Gateway.Interfaces
{
    public interface IHttpApiGateway
    {
        public Task<APIGatewayProxyResponse> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest);

        public Task<APIGatewayProxyResponse> UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
