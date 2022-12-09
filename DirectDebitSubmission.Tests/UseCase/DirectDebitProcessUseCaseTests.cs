using Amazon.Lambda.APIGatewayEvents;
using AutoFixture;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Gateway.Interfaces;
using DirectDebitSubmission.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DirectDebitSubmission.Tests.UseCase
{
    [Collection("UseCase collection")]
    public class DirectDebitProcessUseCaseTests
    {
        private readonly Mock<IHttpApiGateway> _mockGateway;
        private readonly DirectDebitProcessUseCase _directDebitProcessUseCase;
        private readonly Fixture _fixture = new Fixture();

        public DirectDebitProcessUseCaseTests()
        {
            _mockGateway = new Mock<IHttpApiGateway>();
            _directDebitProcessUseCase = new DirectDebitProcessUseCase(_mockGateway.Object);
        }

        [Fact]
        public async Task ProcessExecuteAsync_ReturnAPIGatewayProxyResponse_ShouldNotBeNUll()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { DirectDebitApiRequest = _fixture.Create<APIGatewayProxyRequest>(), TransactionApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            var transactionCollection = _fixture.CreateMany<Transaction>(5);

            _mockGateway.Setup(x => x.GetAsync(directDebitRequest)).ReturnsAsync(transactionCollection);

            var response = _fixture.Create<APIGatewayProxyResponse>();

            _mockGateway.Setup(x => x.UpdateAsync(directDebitRequest)).ReturnsAsync(response);

            var resut = await _directDebitProcessUseCase.ProcessExecuteAsync(directDebitRequest).ConfigureAwait(false);

            resut.Should().NotBeNull();
        }

        [Fact]
        public async Task ProcessExecuteAsync_ReturnAPIGatewayProxyResponse_DataShouldNotBeNull()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { DirectDebitApiRequest = _fixture.Create<APIGatewayProxyRequest>(), TransactionApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            var transactionCollection = _fixture.CreateMany<Transaction>(5);

            _mockGateway.Setup(x => x.GetAsync(directDebitRequest)).ReturnsAsync(transactionCollection);

            await _directDebitProcessUseCase.ProcessExecuteAsync(directDebitRequest).ConfigureAwait(false);

            directDebitRequest.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task ProcessExecuteAsync_ReturnAPIGatewayProxyResponse_DataShouldHaveConcreteType()
        {
            var directDebitRequest = new DirectDebitApiGatewayProxyRequest() { DirectDebitApiRequest = _fixture.Create<APIGatewayProxyRequest>(), TransactionApiRequest = _fixture.Create<APIGatewayProxyRequest>() };

            var transactionCollection = _fixture.CreateMany<Transaction>(5);

            _mockGateway.Setup(x => x.GetAsync(directDebitRequest)).ReturnsAsync(transactionCollection);

            await _directDebitProcessUseCase.ProcessExecuteAsync(directDebitRequest).ConfigureAwait(false);

            directDebitRequest.Data.Should().BeEquivalentTo(transactionCollection);
        }
    }
}
