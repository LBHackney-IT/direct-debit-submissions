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
        public Task<APIGatewayProxyResponse> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest);

        public Task<APIGatewayProxyResponse> UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
