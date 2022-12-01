using BaseListener.Gateway.Interfaces;
using System.Threading.Tasks;
using BaseListener.UseCase.Interfaces;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using BaseListener.Boundary.Response;
using BaseListener.Helpers.GeneralModels;
using BaseListener.Factories;
using System.Net;

namespace BaseListener.UseCase
{
    public class DirectDebitProcessUseCase : IDirectDebitProcessUseCase
    {
        private readonly IHttpApiGateway _gateway;

        public DirectDebitProcessUseCase(IHttpApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<APIGatewayProxyResponse> ProcessExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            APIGatewayProxyResponse response = await _gateway.GetAsync(apiGatewayProxyRequest).ConfigureAwait(false);

            if (response.StatusCode == (int) HttpStatusCode.OK)
            {
                PaginatedResponse<TransactionResponse> model = JsonSerializer.Deserialize<PaginatedResponse<TransactionResponse>>(response.Body);

                apiGatewayProxyRequest.Body = JsonSerializer.Serialize(model.Results.ToCalculateAmount());

                response = await _gateway.UpdateAsync(apiGatewayProxyRequest).ConfigureAwait(false);
            }

            return response;
        }
    }
}
