using DirectDebitSubmission.Gateway.Interfaces;
using System.Threading.Tasks;
using DirectDebitSubmission.UseCase.Interfaces;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using DirectDebitSubmission.Boundary.Response;
using DirectDebitSubmission.Helpers.GeneralModels;
using DirectDebitSubmission.Factories;
using System.Net;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Boundary.Request;

namespace DirectDebitSubmission.UseCase
{
    public class DirectDebitProcessUseCase : IDirectDebitProcessUseCase
    {
        private readonly IHttpApiGateway _gateway;

        public DirectDebitProcessUseCase(IHttpApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<APIGatewayProxyResponse> ProcessExecuteAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest)
        {
            directDebitApiGatewayProxyRequest.Data = await _gateway.GetAsync(directDebitApiGatewayProxyRequest).ConfigureAwait(false);

            return await _gateway.UpdateAsync(directDebitApiGatewayProxyRequest).ConfigureAwait(false);
        }
    }
}
