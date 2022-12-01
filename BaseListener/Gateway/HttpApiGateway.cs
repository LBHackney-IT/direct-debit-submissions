using Amazon.Lambda.APIGatewayEvents;
using BaseListener.Gateway.Interfaces;
using BaseListener.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text.Json;
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

        public async Task<APIGatewayProxyResponse> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            string periodEndDate;

            apiGatewayProxyRequest.QueryStringParameters.TryGetValue("PeriodEndDate", out periodEndDate);

            apiGatewayProxyRequest.QueryStringParameters.Add(KeyValuePair.Create("PeriodStartDate", DateTime.Parse(periodEndDate).AddYears(-1).ToString()));

            var response = await this._httpApiContext.GetAsync(apiGatewayProxyRequest);

            return response;
        }

        public async Task<APIGatewayProxyResponse> UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            return await this._httpApiContext.UpdateAsync(apiGatewayProxyRequest);
        }
    }
}
