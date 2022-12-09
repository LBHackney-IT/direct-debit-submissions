using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Factories;
using DirectDebitSubmission.Gateway;
using DirectDebitSubmission.Infrastructure;
using DirectDebitSubmission.Tests;
using FluentAssertions;
using Hackney.Core.Testing.DynamoDb;
using Hackney.Core.Testing.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BaseListener.Tests.Gateway
{
    [Collection("AppTest collection")]
    public class DynamoDbEntityGatewayTests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<ILogger<DynamoDbEntityGateway>> _logger;
        private readonly DynamoDbEntityGateway _classUnderTest;
        private readonly IDynamoDbFixture _dbFixture;
        private IDynamoDBContext DynamoDb => _dbFixture.DynamoDbContext;
        private readonly List<Action> _cleanup = new List<Action>();

        public DynamoDbEntityGatewayTests(MockApplicationFactory appFactory)
        {
            _dbFixture = appFactory.DynamoDbFixture;
            _logger = new Mock<ILogger<DynamoDbEntityGateway>>();
            _classUnderTest = new DynamoDbEntityGateway(DynamoDb, _logger.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                foreach (var action in _cleanup)
                    action();

                _disposed = true;
            }
        }

        private async Task InsertDatatoDynamoDB(DomainEntity entity)
        {
            await _dbFixture.SaveEntityAsync(entity.ToDatabase()).ConfigureAwait(false);
        }

        private DomainEntity ConstructDomainEntity()
        {
            var entity = _fixture.Build<DomainEntity>()
                                 .With(x => x.VersionNumber, (int?) null)
                                 .Create();
            return entity;
        }
    }
}
