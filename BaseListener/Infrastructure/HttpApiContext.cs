using Amazon.Lambda.APIGatewayEvents;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
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

        public async Task<APIGatewayProxyResponse> GetAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            var httpClient = this.Setup(apiGatewayProxyRequest, apiGatewayProxyRequest.HttpMethod);

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString(apiGatewayProxyRequest.Path, apiGatewayProxyRequest.QueryStringParameters));

            response.EnsureSuccessStatusCode();

            var contentStream = await response.Content.ReadAsStringAsync();

            return new APIGatewayProxyResponse() { StatusCode = (int) response.StatusCode, Body = contentStream };
        }

        public async Task<APIGatewayProxyResponse> UpdateAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            var httpClient = this.Setup(apiGatewayProxyRequest, apiGatewayProxyRequest.RequestContext.HttpMethod);

            HttpContent httpContent = new StringContent(apiGatewayProxyRequest.Body, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(Path.Combine(apiGatewayProxyRequest.RequestContext.Path, apiGatewayProxyRequest.RequestContext.RouteKey), httpContent);

            response.EnsureSuccessStatusCode();

            return new APIGatewayProxyResponse() { StatusCode = (int) response.StatusCode };
        }
    }
}
