using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Boundary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DirectDebitSubmission.UseCase.Interfaces
{
    public interface IDirectDebitProcessUseCase
    {
        Task<APIGatewayProxyResponse> ProcessExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
