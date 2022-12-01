using Amazon.Lambda.APIGatewayEvents;
using BaseListener.Boundary.Request;
using BaseListener.Boundary.Response;
using BaseListener.Helpers.GeneralModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseListener.Gateway.Interfaces
{
    public interface IHttpApiGateway
    {
        public Task<PaginatedResponse<TransactionResponse>> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest);

        public Task UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
