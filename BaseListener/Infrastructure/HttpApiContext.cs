using Amazon.Lambda.APIGatewayEvents;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BaseListener.Infrastructure
{
    public class HttpApiContext : IHttpApiContext
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpApiContext(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        private HttpClient Setup(APIGatewayProxyRequest apiGatewayProxyRequest, string method)
        {
            string authorization;

            apiGatewayProxyRequest.Headers.TryGetValue("Authorization", out authorization);

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", authorization);

            httpClient.BaseAddress = new Uri((method == HttpMethod.Get.ToString()) ? apiGatewayProxyRequest.Resource : apiGatewayProxyRequest.RequestContext.ResourcePath);

            return httpClient;
        }

        public async Task<TModel> GetAsync<TModel>(APIGatewayProxyRequest apiGatewayProxyRequest) where TModel : class
        {
            var httpClient = this.Setup(apiGatewayProxyRequest, apiGatewayProxyRequest.HttpMethod);

            var result = await httpClient.GetAsync(QueryHelpers.AddQueryString(apiGatewayProxyRequest.Path, apiGatewayProxyRequest.QueryStringParameters));

            var contentStream = await result.Content.ReadAsStreamAsync();

            TModel model = await JsonSerializer.DeserializeAsync<TModel>(contentStream!);

            return model!;
        }

        public async Task UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            var httpClient = this.Setup(apiGatewayProxyRequest, apiGatewayProxyRequest.RequestContext.HttpMethod);

            HttpContent httpContent = new StringContent(apiGatewayProxyRequest.Body);

            await httpClient.PutAsync(Path.Combine(apiGatewayProxyRequest.RequestContext.Path, apiGatewayProxyRequest.RequestContext.RouteKey), httpContent);
        }
    }
}
