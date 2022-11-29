using Amazon.Lambda.APIGatewayEvents;
using System.IO;
using System.Threading.Tasks;

namespace BaseListener.Infrastructure
{
    public abstract class HttpBaseApi<PModel> where PModel : class
    {
        protected abstract Task<Stream> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest);

        public abstract Task<TModel> GetAsync<TModel>(APIGatewayProxyRequest apiGatewayProxyRequest) where TModel : class;
    }
}
