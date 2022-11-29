using Amazon.Lambda.APIGatewayEvents;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BaseListener.Infrastructure
{
    public class HttpTransactionApi : HttpBaseApi<HttpTransactionApi>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpTransactionApi(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        private HttpClient Setup(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            string authorization;

            apiGatewayProxyRequest.Headers.TryGetValue("Authorization", out authorization);

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", authorization);

            httpClient.BaseAddress = new Uri(apiGatewayProxyRequest.Resource);

            return httpClient;
        }

        protected override async Task<Stream> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            var httpClient = this.Setup(apiGatewayProxyRequest);

            var result = await httpClient.GetAsync(QueryHelpers.AddQueryString(apiGatewayProxyRequest.Path, apiGatewayProxyRequest.QueryStringParameters));

            var contentStream = await result.Content.ReadAsStreamAsync();

            return contentStream;
        }

        public override async Task<TModel> GetAsync<TModel>(APIGatewayProxyRequest apiGatewayProxyRequest) where TModel : class
        {
            using var stream = await this.GetAsync(apiGatewayProxyRequest);

            TModel actionsDTO = await JsonSerializer.DeserializeAsync<TModel>(stream!);

            return actionsDTO!;
        }
    }
}
