using BaseListener.Boundary;
using BaseListener.Domain;
using BaseListener.Gateway.Interfaces;
using BaseListener.Infrastructure.Exceptions;
using System.Threading.Tasks;
using System;
using BaseListener.UseCase.Interfaces;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Linq;
using BaseListener.Helpers;

namespace BaseListener.UseCase
{
    public class DirectDebitProcessUseCase : IDirectDebitProcessUseCase
    {
        private readonly IHttpApiGateway _gateway;

        public DirectDebitProcessUseCase(IHttpApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task ProcessExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            decimal amount = await _gateway.GetAmount(apiGatewayProxyRequest).ConfigureAwait(false);
        }
    }
}
