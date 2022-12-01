using BaseListener.Gateway.Interfaces;
using System.Threading.Tasks;
using BaseListener.UseCase.Interfaces;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using BaseListener.Boundary.Response;
using BaseListener.Helpers.GeneralModels;
using BaseListener.Factories;

namespace BaseListener.UseCase
{
    public class DirectDebitProcessUseCase : IDirectDebitProcessUseCase
    {
        private readonly IHttpApiGateway _gateway;

        public DirectDebitProcessUseCase(IHttpApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ProcessExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            PaginatedResponse<TransactionResponse> directDebitUpdateRequest = await _gateway.GetAsync(apiGatewayProxyRequest).ConfigureAwait(false);

            apiGatewayProxyRequest.Body = JsonSerializer.Serialize(directDebitUpdateRequest.Results.ToCalculateAmount());

            await _gateway.UpdateAsync(apiGatewayProxyRequest).ConfigureAwait(false);

            //new APIGatewayProxyResponse() { StatusCode = 500, Body = "Internal Server Error" };
        }
    }
}
