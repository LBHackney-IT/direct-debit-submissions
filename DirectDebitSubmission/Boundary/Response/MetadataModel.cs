using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DirectDebitSubmission.Boundary.Response
{
    public class MetadataModel
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }
}
