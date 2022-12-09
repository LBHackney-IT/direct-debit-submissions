using Amazon.DynamoDBv2.Model;
using AutoFixture;
using DirectDebitSubmission.Domain;
using DirectDebitSubmission.Factories;
using DirectDebitSubmission.Infrastructure.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace DirectDebitSubmission.Tests.Factories
{
    public class DirectDebitFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void DirectDebitDomainShouldNotBeNull()
        {
            var transactions = _fixture.CreateMany<Transaction>(5);

            var directDebitDomain = transactions.ToDomain();

            directDebitDomain.Should().NotBeNull();
        }

        [Fact]
        public void DirectDebitDomainDivideByZeroThrowsException()
        {
            var transactions = Enumerable.Empty<Transaction>();

            Assert.Throws<DivideByZeroException>(delegate { transactions.ToDomain(); });
        }

        [Fact]
        public void DirectDebitCalculateAmountShouldNotBeNull()
        {
            var transactions = _fixture.CreateMany<Transaction>(5);

            var directDebitDomain = transactions.ToDomain();

            directDebitDomain.Amount.Should().NotBeNull();
        }

        [Fact]
        public void DirectDebitCalculateAmountShouldNotZero()
        {
            var transactions = _fixture.CreateMany<Transaction>(5);

            transactions.ToList().ForEach(item => { item.PaidAmount = (decimal) Math.Pow((int) item.HousingBenefitAmount, 2); });

            var directDebitDomain = transactions.ToDomain();

            directDebitDomain.Amount.Should().BeGreaterThan(0);
        }
    }
}
