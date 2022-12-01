using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;

namespace BaseListener.Infrastructure
{
    public interface IHttpApiContext
    {
        Task<TModel> GetAsync<TModel>(APIGatewayProxyRequest apiGatewayProxyRequest) where TModel : class;

        Task UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
