using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DirectDebitSubmission.Boundary.Response
{
    public class Pagination
    {
        [JsonPropertyName("resultCount")]
        public int ResultCount { get; set; }

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
    }
}
