using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Boundary.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmission.UseCase.Interfaces
{
    public interface IDirectDebitProcessUseCase
    {
        Task<APIGatewayProxyResponse> ProcessExecuteAsync(DirectDebitApiGatewayProxyRequest directDebitApiGatewayProxyRequest);
    }
}
