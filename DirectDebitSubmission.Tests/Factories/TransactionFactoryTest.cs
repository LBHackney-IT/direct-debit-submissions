using AutoFixture;
using DirectDebitSubmission.Factories;
using DirectDebitSubmission.Infrastructure.Entities;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace DirectDebitSubmission.Tests.Factories
{
    public class TransactionFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void CanMapAddRequestEntityEnumerableToDomain()
        {
            var transactionEntityCollection = _fixture.CreateMany<TransactionEntity>(5);

            var transactionCollection = transactionEntityCollection.ToDomain();

            transactionCollection.Should().BeEquivalentTo(transactionEntityCollection, options =>
            {
                options.Excluding(info => info.AssetId);
                options.Excluding(info => info.AssetType);
                options.Excluding(info => info.TenancyAgreementRef);
                options.Excluding(info => info.PropertyRef);
                options.Excluding(info => info.PostDate);
                options.Excluding(info => info.PeriodNo);
                options.Excluding(info => info.TransactionSource);
                options.Excluding(info => info.TransactionDate);
                options.Excluding(info => info.TransactionAmount);
                options.Excluding(info => info.PaymentReference);
                options.Excluding(info => info.BankAccountNumber);
                options.Excluding(info => info.IsSuspense);
                options.Excluding(info => info.ChargedAmount);
                options.Excluding(info => info.BalanceAmount);
                options.Excluding(info => info.Address);
                options.Excluding(info => info.Fund);
                options.Excluding(info => info.SortCode);
                return options;
            });
        }

        [Fact]
        public void CanMapEmptyRequestEntityEnumerableToDomain()
        {
            var transactionEntityCollection = Enumerable.Empty<TransactionEntity>();

            var transactionCollection = transactionEntityCollection.ToDomain();

            transactionCollection.Should().BeEmpty();
        }

        [Fact]
        public void CanMapAddRequestEntityToDomain()
        {
            var transactionEntity = _fixture.Create<TransactionEntity>();

            var transactionCollection = transactionEntity.ToDomain();

            transactionCollection.Should().BeEquivalentTo(transactionEntity, options =>
            {
                options.Excluding(info => info.AssetId);
                options.Excluding(info => info.AssetType);
                options.Excluding(info => info.TenancyAgreementRef);
                options.Excluding(info => info.PropertyRef);
                options.Excluding(info => info.PostDate);
                options.Excluding(info => info.PeriodNo);
                options.Excluding(info => info.TransactionSource);
                options.Excluding(info => info.TransactionDate);
                options.Excluding(info => info.TransactionAmount);
                options.Excluding(info => info.PaymentReference);
                options.Excluding(info => info.BankAccountNumber);
                options.Excluding(info => info.IsSuspense);
                options.Excluding(info => info.ChargedAmount);
                options.Excluding(info => info.BalanceAmount);
                options.Excluding(info => info.Address);
                options.Excluding(info => info.Fund);
                options.Excluding(info => info.SortCode);
                return options;
            });
        }

        [Fact]
        public void CanMapEmptyRequestEntityToDomain()
        {
            var transactionEntity = default(TransactionEntity);

            var transactionCollection = transactionEntity.ToDomain();

            transactionCollection.Should().BeNull();
        }
    }
}
