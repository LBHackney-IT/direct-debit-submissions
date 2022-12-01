using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;

namespace BaseListener.Infrastructure
{
    public interface IHttpApiContext
    {
        Task<APIGatewayProxyResponse> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest);

        Task<APIGatewayProxyResponse> UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
