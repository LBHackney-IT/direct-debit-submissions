using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;
using AutoFixture;
using DirectDebitSubmission.Boundary.Request;
using DirectDebitSubmission.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace DirectDebitSubmission.Tests.E2ETests.Steps
{
    public class BaseSteps
    {
        protected readonly JsonSerializerOptions _jsonOptions = JsonOptions.CreateJsonOptions();
        protected readonly Fixture _fixture = new Fixture();
        protected Exception _lastException;

        public BaseSteps()
        { }

        protected async Task TriggerFunction(Guid id)
        {
            var mockLambdaLogger = new Mock<ILambdaLogger>();
            ILambdaContext lambdaContext = new TestLambdaContext()
            {
                Logger = mockLambdaLogger.Object
            };

            var apiGatewayProxyRequest = _fixture.Build<DirectDebitApiGatewayProxyRequest>().Create();

            Func<Task> func = async () =>
            {
                var fn = new DirectDebitSubmissionFunction();
                await fn.FunctionHandler(apiGatewayProxyRequest, lambdaContext).ConfigureAwait(false);
            };

            _lastException = await Record.ExceptionAsync(func);
        }
    }
}
