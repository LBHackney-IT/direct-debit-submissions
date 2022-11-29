using Amazon.Lambda.APIGatewayEvents;
using BaseListener.Boundary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseListener.UseCase.Interfaces
{
    public interface IDirectDebitProcessUseCase
    {
        Task ProcessExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}
