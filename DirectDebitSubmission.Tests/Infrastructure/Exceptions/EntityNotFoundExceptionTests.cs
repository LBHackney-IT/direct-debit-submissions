using DirectDebitSubmission.Infrastructure.Exceptions;
using FluentAssertions;
using System;
using Xunit;


namespace DirectDebitSubmission.Tests.Infrastructure.Exceptions
{
    public class EntityNotFoundExceptionTests
    {
        [Fact]
        public void EntityNotFoundExceptionConstructorTest()
        {
            var id = Guid.NewGuid();

            var ex = new EntityNotFoundException<SomeEntity>(id);
            ex.Id.Should().Be(id);

            var typeName = typeof(SomeEntity).Name;
            ex.EntityName.Should().Be(typeName);
            ex.Message.Should().Be($"{typeName} with id {id} not found.");
        }
    }

    public class SomeEntity { }
}
