using DirectDebitSubmission.Gateway.Interfaces;
using System.Threading.Tasks;
using DirectDebitSubmission.UseCase.Interfaces;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using DirectDebitSubmission.Boundary.Response;
using DirectDebitSubmission.Helpers.GeneralModels;
using DirectDebitSubmission.Factories;
using System.Net;

namespace DirectDebitSubmission.UseCase
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
