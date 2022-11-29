using Amazon.Lambda.APIGatewayEvents;
using BaseListener.Boundary.Response;
using BaseListener.Gateway.Interfaces;
using BaseListener.Helpers;
using BaseListener.Helpers.GeneralModels;
using BaseListener.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseListener.Gateway
{
    public class HttpApiGateway: IHttpApiGateway
    {
        private readonly IHttpApiContext<HttpTransactionApi> _httpApiContext;

        public HttpApiGateway(IHttpApiContext<HttpTransactionApi> httpApiContext)
        {
            this._httpApiContext = httpApiContext;
        }

        public async Task<decimal> GetAmount(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            string periodEndDate;

            apiGatewayProxyRequest.QueryStringParameters.TryGetValue("PeriodEndDate", out periodEndDate);

            apiGatewayProxyRequest.QueryStringParameters.Add(KeyValuePair.Create("PeriodStartDate", DateTime.Parse(periodEndDate).AddYears(-1).ToString()));

            var response = await this._httpApiContext.Resolve.GetAsync<PaginatedResponse<TransactionResponse>>(apiGatewayProxyRequest);

           CalculateDirectDebit directDebit = new CalculateDirectDebit(response.Results);

           return directDebit.Result;
        }
    }
}
