using Amazon.Lambda.APIGatewayEvents;
using BaseListener.Boundary.Response;
using BaseListener.Gateway.Interfaces;
using BaseListener.Helpers.GeneralModels;
using BaseListener.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseListener.Gateway
{
    public class HttpApiGateway: IHttpApiGateway
    {
        private readonly IHttpApiContext _httpApiContext;

        public HttpApiGateway(IHttpApiContext httpApiContext)
        {
            this._httpApiContext = httpApiContext;
        }

        public async Task<PaginatedResponse<TransactionResponse>> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            string periodEndDate;

            apiGatewayProxyRequest.QueryStringParameters.TryGetValue("PeriodEndDate", out periodEndDate);

            apiGatewayProxyRequest.QueryStringParameters.Add(KeyValuePair.Create("PeriodStartDate", DateTime.Parse(periodEndDate).AddYears(-1).ToString()));

            var response = await this._httpApiContext.GetAsync<PaginatedResponse<TransactionResponse>>(apiGatewayProxyRequest);

            return response;
        }

        public async Task UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            await this._httpApiContext.UpdateAsync(apiGatewayProxyRequest);
        }
    }
}
