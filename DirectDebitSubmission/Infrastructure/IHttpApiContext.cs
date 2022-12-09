using Amazon.Lambda.APIGatewayEvents;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Net.Http;
using System.Threading.Tasks;

namespace DirectDebitSubmission.Infrastructure
{
    public interface IHttpApiContext
    {
        Task<APIGatewayProxyResponse> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest);

        Task<APIGatewayProxyResponse> UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
