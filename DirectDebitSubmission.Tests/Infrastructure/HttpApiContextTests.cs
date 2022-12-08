using Amazon.Lambda.APIGatewayEvents;
using Amazon.Runtime;
using AutoFixture;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Boundary.Response;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Gateway;
using DirectDebitSubmission.Gateway.Interfaces;
using DirectDebitSubmission.Infrastructure;
using DirectDebitSubmission.Infrastructure.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DirectDebitSubmission.Tests.Infrastructure
{
    [Collection("ApiContext collection")]
    public class HttpApiContextTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<HttpClient> _httpClient;
        private readonly HttpApiContext _httpApiContext;
        private readonly Fixture _fixture = new Fixture();

        public HttpApiContextTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _httpClient = new Mock<HttpClient>();
            _httpApiContext = new HttpApiContext(_httpClientFactory.Object);
        }

        [Theory]
        [InlineData("https://26exrtxaid.execute-api.eu-west-2.amazonaws.com/developmen", "/api/v1/transactions/active")]
        public async Task GetAsync_APIGatewayProxyResponse_NotSuccessful(string resource, string path)
        {
            APIGatewayProxyRequest apiGatewayProxyRequest = _fixture.Create<APIGatewayProxyRequest>();

            apiGatewayProxyRequest.Headers.Add("Authorization", "Bearer");

            apiGatewayProxyRequest.Resource = resource;

            apiGatewayProxyRequest.Path = path;

            apiGatewayProxyRequest.QueryStringParameters.Clear();

            apiGatewayProxyRequest.QueryStringParameters.Add("PeriodEndDate", DateTime.Now.ToString());

            _httpClient.Object.Timeout = TimeSpan.FromSeconds(30);

            _httpClient.Object.DefaultRequestVersion = HttpVersion.Version10;

            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);

            await Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _httpApiContext.GetAsync(apiGatewayProxyRequest);
            });
        }

        [Theory]
        [InlineData("https://26exrtxaid.execute-api.eu-west-2.amazonaws.com/developmen", "/api/v1/transactions/active")]
        public async Task GetAsync_APIGatewayProxyResponse_Successful(string resource, string path)
        {
            APIGatewayProxyRequest apiGatewayProxyRequest = _fixture.Create<APIGatewayProxyRequest>();

            apiGatewayProxyRequest.Headers.Add("Authorization", "Bearer");

            apiGatewayProxyRequest.Resource = resource;

            apiGatewayProxyRequest.Path = path;

            apiGatewayProxyRequest.QueryStringParameters.Clear();

            apiGatewayProxyRequest.QueryStringParameters.Add("PeriodEndDate", DateTime.Now.ToString());

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });

            HttpClient client = new HttpClient(mockMessageHandler.Object);

            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var response = await _httpApiContext.GetAsync(apiGatewayProxyRequest);

            response.StatusCode.Should().Be((int) HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("https://bvpgbe4xm0.execute-api.eu-west-2.amazonaws.com/development", "/api/v1/direct-debit/")]
        public async Task UpdateAsync_APIGatewayProxyResponse_NotSuccessful(string resource, string path)
        {
            APIGatewayProxyRequest apiGatewayProxyRequest = _fixture.Create<APIGatewayProxyRequest>();

            apiGatewayProxyRequest.Headers.Add("Authorization", "Bearer");

            apiGatewayProxyRequest.Resource = resource;

            apiGatewayProxyRequest.Path = path;

            apiGatewayProxyRequest.HttpMethod = HttpMethod.Put.Method;

            apiGatewayProxyRequest.PathParameters.Clear();

            apiGatewayProxyRequest.PathParameters.Add("routeKey", Guid.NewGuid().ToString());

            apiGatewayProxyRequest.Body = JsonSerializer.Serialize(_fixture.Create<DirectDebit>());

            _httpClient.Object.DefaultRequestHeaders.Add("Accept", "application/json");

            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);

            await Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _httpApiContext.UpdateAsync(apiGatewayProxyRequest);
            });
        }

        [Theory]
        [InlineData("https://bvpgbe4xm0.execute-api.eu-west-2.amazonaws.com/development", "/api/v1/direct-debit/")]
        public async Task UpdateAsync_APIGatewayProxyResponse_Successful(string resource, string path)
        {
            APIGatewayProxyRequest apiGatewayProxyRequest = _fixture.Create<APIGatewayProxyRequest>();

            apiGatewayProxyRequest.Headers.Add("Authorization", "Bearer");

            apiGatewayProxyRequest.Resource = resource;

            apiGatewayProxyRequest.Path = path;

            apiGatewayProxyRequest.HttpMethod = HttpMethod.Put.Method;

            apiGatewayProxyRequest.PathParameters.Clear();

            apiGatewayProxyRequest.PathParameters.Add("routeKey", Guid.NewGuid().ToString());

            apiGatewayProxyRequest.Body = JsonSerializer.Serialize(_fixture.Create<DirectDebit>());

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(string.Empty)
            });

            HttpClient client = new HttpClient(mockMessageHandler.Object);

            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            var response = await _httpApiContext.UpdateAsync(apiGatewayProxyRequest);

            response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
