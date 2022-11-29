using Amazon.Lambda.APIGatewayEvents;
using BaseListener.Boundary.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseListener.Gateway.Interfaces
{
    public interface IHttpApiGateway
    {
        public Task<decimal> GetAmount(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
