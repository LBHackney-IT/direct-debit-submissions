using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.SimpleNotificationService.Util;
using DirectDebitSubmission.Boundary.Request;
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DirectDebitSubmission
{
    /// <summary>
    /// Lambda function triggered by an SQS message
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DirectDebitSubmissionFunction : BaseFunction
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public DirectDebitSubmissionFunction()
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
        /// <param name="directDebitApiGatewayProxyRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [LogCall(LogLevel.Information)]
        public async Task FunctionHandler(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest, ILambdaContext context)
        {
            try
            {
                IDirectDebitProcessUseCase processor = ServiceProvider.GetService<IDirectDebitProcessUseCase>();

                await processor.ProcessExecuteAsync(directDebitApiGatewayProxyRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Exception processing message : {ex.Message}");
                throw;
            }
        }
    }
}
