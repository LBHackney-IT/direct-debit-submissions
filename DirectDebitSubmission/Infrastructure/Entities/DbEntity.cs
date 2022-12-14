using Amazon.DynamoDBv2.DataModel;
using Hackney.Core.DynamoDb.Converters;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DirectDebitSubmission.Infrastructure.Entities
{
    // TODO - Alter if DynamoDb is not required
    [ExcludeFromCodeCoverage]
    [DynamoDBTable("SomeTable", LowerCamelCaseProperties = true)]
    public class DbEntity
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string Description { get; set; }

        // DynamoDb documents that get modified will have a version number to ensure data integrity
        [DynamoDBVersion]
        public int? VersionNumber { get; set; }
    }
}
