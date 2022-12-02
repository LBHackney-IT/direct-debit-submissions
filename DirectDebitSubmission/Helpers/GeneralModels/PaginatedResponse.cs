using System.Collections.Generic;
using System.Text.Json.Serialization;
using DirectDebitSubmission.Boundary.Response;

namespace DirectDebitSubmission.Helpers.GeneralModels
{
    public class PaginatedResponse<T>
    {
        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }

        [JsonPropertyName("metadata")]
        public MetadataModel Metadata { get; set; }
    }
}
