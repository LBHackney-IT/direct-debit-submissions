using System.Collections.Generic;
using System.Text.Json.Serialization;
using BaseListener.Boundary.Response;

namespace BaseListener.Helpers.GeneralModels
{
    public class PaginatedResponse<T>
    {
        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }

        [JsonPropertyName("metadata")]
        public MetadataModel Metadata { get; set; }
    }
}
