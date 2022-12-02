using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.SimpleNotificationService.Util;
using DirectDebitSubmission.Boundary;
using DirectDebitSubmission.Gateway;
using DirectDebitSubmission.Gateway.Interfaces;
using DirectDebitSubmission.Infrastructure;
using DirectDebitSubmission.UseCase;
using DirectDebitSubmission.UseCase.Interfaces;
using Hackney.Core.DynamoDb;
using Hackney.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DirectDebitSubmission
{
    /// <summary>
    /// Lambda function triggered by an SQS message
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApiGatewayFunction : BaseFunction
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public ApiGatewayFunction()
        { }

        /// <summary>
        /// Use this method to perform any DI configuration required
        /// </summary>
        /// <param name="services"></param>
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDynamoDB();

            services.AddHttpClient();
            services.AddScoped<IDoSomethingUseCase, DoSomethingUseCase>();
            services.AddScoped<IDirectDebitProcessUseCase, DirectDebitProcessUseCase>();

            services.AddScoped<IDbEntityGateway, DynamoDbEntityGateway>();
            services.AddScoped<IHttpApiGateway, HttpApiGateway>();
            services.AddTransient<IHttpApiContext, HttpApiContext>();

            services.AddHttpClient("Direct Debit Submission", httpClient => { httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); });

            base.ConfigureServices(services);
        }


        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="apiGatewayProxyRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(APIGatewayProxyRequest apiGatewayProxyRequest, ILambdaContext context)
        {
            await ProcessMessageAsync(apiGatewayProxyRequest, context).ConfigureAwait(false);
        }

        /// <summary>
        /// Method called to process every distinct message received.
        /// </summary>
        /// <param name="apiGatewayProxyRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [LogCall(LogLevel.Information)]
        private async Task<APIGatewayProxyResponse> ProcessMessageAsync(APIGatewayProxyRequest apiGatewayProxyRequest, ILambdaContext context)
        {
            context.Logger.LogLine($"Processing message {apiGatewayProxyRequest.RequestContext.MessageId}");

            var eventType = apiGatewayProxyRequest.RequestContext.EventType;

            try
            {
                IDirectDebitProcessUseCase processor = null;
                switch (eventType)
                {
                    case EventTypes.DirectDebitCalculationEvent:
                        {
                            processor = ServiceProvider.GetService<IDirectDebitProcessUseCase>();
                            break;
                        }
                    default: throw new ArgumentException($"Unknown event type: {eventType} on message id: {apiGatewayProxyRequest.RequestContext.MessageId}");
                }

                return await processor.ProcessExecuteAsync(apiGatewayProxyRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Exception processing message id: {apiGatewayProxyRequest.RequestContext.MessageId}; type: {eventType}; request id: {apiGatewayProxyRequest.RequestContext.RequestId}");
                throw; // AWS will handle retry/moving to the dead letter queue
            }
        }
    }
}
