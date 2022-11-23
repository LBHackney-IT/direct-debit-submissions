using BaseListener.UseCase;
using BaseListener.UseCase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BaseListener;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.

namespace DirectDebitExport
{
    /// <summary>
    /// Lambda function triggered by an SQS message
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DirectDebitExportEntryPoint : BaseFunction
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public DirectDebitExportEntryPoint()
        { }

        /// <summary>
        /// Use this method to perform any DI configuration required
        /// </summary>
        /// <param name="services"></param>
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<IGenerateAndUploadBacsDatFileUseCase, GenerateAndUploadBacsDatFileUseCase>();

            base.ConfigureServices(services);
        }

        // Try: use custom startup

        /// <summary>
        /// This method is called for every Direct Debit Export Lambda invocation.
        /// </summary>
        public async Task FunctionHandler()
        {
            var useCase = ServiceProvider.GetService<IGenerateAndUploadBacsDatFileUseCase>();
            await useCase.Execute().ConfigureAwait(false);
        }
    }
}
