using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DirectDebitSubmission.Boundary.Response
{
    public class ApiResponse<T> where T : class
    {
        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }
    }
}
