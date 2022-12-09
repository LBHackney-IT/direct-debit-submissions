using Amazon.Lambda.APIGatewayEvents;
using AutoFixture;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Boundary.Response;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Factories;
using DirectDebitSubmission.Gateway;
using DirectDebitSubmission.Infrastructure;
using DirectDebitSubmission.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DirectDebitSubmission.Tests.Gateway
{
    [Collection("Gateway collection")]
    public class HttpApiGatewayTests
    {
        private readonly Mock<IHttpApiContext> _httpApiContext;
        private readonly HttpApiGateway _httpApiGateway;
        private readonly Fixture _fixture = new Fixture();

        public HttpApiGatewayTests()
        {
            _httpApiContext = new Mock<IHttpApiContext>();
            _httpApiGateway = new HttpApiGateway(_httpApiContext.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnTransactionDomain_ShouldNotBeNull()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { TransactionApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            directDebitRequest.TransactionApiRequest.QueryStringParameters.Add("PeriodEndDate", DateTime.Now.ToString());

            APIGatewayProxyResponse apiResponse = _fixture.Create<APIGatewayProxyResponse>();

            var apiResponseWrapper = _fixture.Create<ApiResponse<TransactionEntity>>();

            apiResponse.Body = JsonSerializer.Serialize(apiResponseWrapper);

            _httpApiContext.Setup(x => x.GetAsync(directDebitRequest.TransactionApiRequest)).ReturnsAsync(apiResponse);

            var resut = await _httpApiGateway.GetAsync(directDebitRequest).ConfigureAwait(false);

            resut.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_ReturnTransactionDomain_ShouldHaveConcreteType()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { TransactionApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            directDebitRequest.TransactionApiRequest.QueryStringParameters.Add("PeriodEndDate", DateTime.Now.ToString());

            APIGatewayProxyResponse apiResponse = _fixture.Create<APIGatewayProxyResponse>();

            var apiResponseWrapper = _fixture.Create<ApiResponse<TransactionEntity>>();

            apiResponse.Body = JsonSerializer.Serialize(apiResponseWrapper);

            _httpApiContext.Setup(x => x.GetAsync(directDebitRequest.TransactionApiRequest)).ReturnsAsync(apiResponse);

            var resut = await _httpApiGateway.GetAsync(directDebitRequest).ConfigureAwait(false);

            resut.Should().BeOfType<List<Transaction>>();
        }

        [Fact]
        public async Task GetAsync_ReturnTransactionDomain_ShouldBeNull()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { TransactionApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            directDebitRequest.TransactionApiRequest.QueryStringParameters.Add("PeriodEndDate", DateTime.Now.ToString());

            APIGatewayProxyResponse apiResponse = _fixture.Create<APIGatewayProxyResponse>();

            var apiResponseWrapper = _fixture.Create<ApiResponse<TransactionEntity>>();

            apiResponseWrapper.Results = null;

            apiResponse.Body = JsonSerializer.Serialize(apiResponseWrapper);

            _httpApiContext.Setup(x => x.GetAsync(directDebitRequest.TransactionApiRequest)).ReturnsAsync(apiResponse);

            var resut = await _httpApiGateway.GetAsync(directDebitRequest).ConfigureAwait(false);

            resut.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task UpdateAsync_ReturnAPIGatewayProxyResponse_BodyShouldNotBeNUll()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { DirectDebitApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            var transactions = _fixture.CreateMany<Transaction>(5);

            transactions.ToList().ForEach(item => { item.PaidAmount = (decimal) Math.Pow((int) item.HousingBenefitAmount, 2); });

            directDebitRequest.Data = transactions;

            await _httpApiGateway.UpdateAsync(directDebitRequest).ConfigureAwait(false);

            directDebitRequest.DirectDebitApiRequest.Body.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task UpdateAsync_ReturnAPIGatewayProxyResponse_ShouldNotBeNUll()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { DirectDebitApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            var transactions = _fixture.CreateMany<Transaction>(5);

            transactions.ToList().ForEach(item => { item.PaidAmount = (decimal) Math.Pow((int) item.HousingBenefitAmount, 2); });

            directDebitRequest.Data = transactions;

            var apiResponse = _fixture.Create<APIGatewayProxyResponse>();

            _httpApiContext.Setup(x => x.UpdateAsync(directDebitRequest.DirectDebitApiRequest)).ReturnsAsync(apiResponse);

            var result = await _httpApiGateway.UpdateAsync(directDebitRequest).ConfigureAwait(false);

            result.Should().NotBeNull();
        }
    }
}
