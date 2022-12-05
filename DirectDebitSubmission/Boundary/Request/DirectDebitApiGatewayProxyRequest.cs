using Amazon.Lambda.APIGatewayEvents;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Factories;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DirectDebitSubmission.Boundary.Request
{
    public class DirectDebitApiGatewayProxyRequest
    {
        [JsonPropertyName("transactionapirequest")]
        public APIGatewayProxyRequest TransactionApiRequest { get; set; }

        [JsonPropertyName("directdebitapirequest")]
        public APIGatewayProxyRequest DirectDebitApiRequest { get; set; }

        public IEnumerable<Transaction> Data { get; set; }
    }
}
