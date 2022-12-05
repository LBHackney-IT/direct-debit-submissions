using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Boundary.Response;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Factories;
using DirectDebitSubmission.Gateway.Interfaces;
using DirectDebitSubmission.Helpers.GeneralModels;
using DirectDebitSubmission.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DirectDebitSubmission.Gateway
{
    public class HttpApiGateway : IHttpApiGateway
    {
        private readonly IHttpApiContext _httpApiContext;

        public HttpApiGateway(IHttpApiContext httpApiContext)
        {
            this._httpApiContext = httpApiContext;
        }

        public async Task<DirectDebit> GetAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest)
        {
            string periodEndDate;

            directDebitApiGatewayProxyRequest.TransactionApiRequest.QueryStringParameters.TryGetValue("PeriodEndDate", out periodEndDate);

            directDebitApiGatewayProxyRequest.TransactionApiRequest.QueryStringParameters.Add(KeyValuePair.Create("PeriodStartDate", DateTime.Parse(periodEndDate).AddYears(-1).ToString()));

            var response = await this._httpApiContext.GetAsync(directDebitApiGatewayProxyRequest.TransactionApiRequest);

            PaginatedResponse<TransactionResponse> model = JsonSerializer.Deserialize<PaginatedResponse<TransactionResponse>>(response.Body);

            return model.Results.ToDomain();
        }

        public async Task<APIGatewayProxyResponse> UpdateAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest)
        {
            directDebitApiGatewayProxyRequest.DirectDebitApiRequest.Body = JsonSerializer.Serialize(directDebitApiGatewayProxyRequest.Data);

            return await this._httpApiContext.UpdateAsync(directDebitApiGatewayProxyRequest.DirectDebitApiRequest);
        }
    }
}
