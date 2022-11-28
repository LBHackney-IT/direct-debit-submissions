using System.Collections;
using System.Collections.Generic;
using AutoFixture;
using BaseListener.Domain;
using BaseListener.Helpers;
using BaseListener.Infrastructure;
using FluentAssertions;
using Xunit;

namespace BaseListener.Tests.Helpers;

public class BacsStringGeneratorTests
{
    private static DirectDebitToBacsGenerator _directDebitToBacsGenerator = new DirectDebitToBacsGenerator();
    DirectDebitDomain GetTestDirectDebit(
        string accountNumber = null,
        string fund = null,
        int acc = 0,
        int trans = 17,
        string accountHolder = null,
        string bankAccountNumber = null,
        string branchSortCode = null,
        string serviceUserNumber = null,
        string reference = null,
        decimal amount = 1000,
        decimal additionalAmount = 3000,
        decimal? fixedAmount = null)
    {
        var testDirectDebit = new DirectDebitDomain
        {
            AccountNumber = accountNumber is not null ? accountNumber : "11111111",
            PaymentReference = "1234567890",
            Fund = fund is not null ? fund : "HSGRENT",
            Acc = acc,
            Trans = trans,
            AccountHolder = accountHolder is not null ? accountHolder : "Mr Egg Spammer",
            BranchSortCode = branchSortCode is not null ? branchSortCode : "333333",
            Amount = amount,
            AdditionalAmount = additionalAmount,
            FixedAmount = fixedAmount is not null ? fixedAmount : null,
            PreferredDate = 10234,
        };
        return testDirectDebit;
    }

    [Fact]
    public void CanCreateBacsObjectsFromDirectDebits()
    {
        // Arrange
        var fixtureDdNoFixedAmount = GetTestDirectDebit();

        var fixtureDdWithFixedAmount = GetTestDirectDebit();
        fixtureDdWithFixedAmount.FixedAmount = 9000;

        var fixtureDdInvalidFields = GetTestDirectDebit();
        fixtureDdInvalidFields.AccountNumber = "1234";
        fixtureDdInvalidFields.Fund = "Hello!";
        fixtureDdInvalidFields.BranchSortCode = "12";

        IEnumerable<DirectDebitDomain> directDebitEntitiesFixture = new[]
        {
            fixtureDdNoFixedAmount, fixtureDdWithFixedAmount, fixtureDdInvalidFields
        };

        // Act
        var bacsObjects = _directDebitToBacsGenerator.DirectDebitEntitiesToBacsObjects(directDebitEntitiesFixture);

        // Assert
        bacsObjects.InvalidDirectDebitItems.Count.Should().Be(1);
        bacsObjects.InvalidDirectDebitItems[0].InvalidDirectDebitDomain.Should().Be(fixtureDdInvalidFields);
        bacsObjects.InvalidDirectDebitItems[0].Information.Should().ContainAll(
            $"{fixtureDdInvalidFields.Id} could not be converted",
            fixtureDdInvalidFields.AccountNumber,
            fixtureDdInvalidFields.Fund
        );
        bacsObjects.InvalidDirectDebitItems[0].Information.Should().Contain(fixtureDdInvalidFields.BranchSortCode);
        bacsObjects.ValidBacsItems.Count.Should().Be(2);

        bacsObjects.ValidBacsItems[0].RentPayment.Should().Be(4000);
        bacsObjects.ValidBacsItems[1].RentPayment.Should().Be(9000);
    }

    [Fact]
    public void CanCreateValidBacsStringFromBacsObject()
    {
        // Arrange
        var testSortCodeResident = 123456;
        var testAccountNumberResident = "12345678";
        var testRentPayment = 4321;
        var testHackneyUserReference = "1029384756";
        var testAccountNameResident = "K A Pendragon";
        var testProcessingDate = 22320;

        var bacsObject = new BacsDataEntity()
        {
            SortCodeResident = testSortCodeResident,
            AccountNumberResident = testAccountNumberResident,
            RentPayment = testRentPayment,
            PaymentReferenceHackney = testHackneyUserReference,
            FundHackney = "HSGRENT",
            AccountNameResident = testAccountNameResident,
            ProcessingDate = testProcessingDate,
        };
        var directDebitToBacsGenerator = new DirectDebitToBacsGenerator();

        // Act
        string bacsString = directDebitToBacsGenerator.BacsObjectToOutputString(bacsObject);

        // Assert
        bacsString.Substring(0, 6).Should().Be(testSortCodeResident.ToString()); // [1-6]
        bacsString.Substring(6, 8).Should().Be(testAccountNumberResident); // [7-14]
        bacsString.Substring(14, 3).Should().Be("017"); // [15-17]
        // TODO: Test Hackney cases? With env variables?
        // bacsString.Substring(17, 6).Should().Be("HACKNEY_SORT_CODE"); // [18-23]
        // bacsString.Substring(23, 8).Should().Be("HACKNEY_ACCOUNT_NUMBER"); // [24-31]
        bacsString.Substring(31, 4).Should().Be("0000"); // [32-35]
        bacsString.Substring(35, 11).Should().Be(testRentPayment.ToString().PadRight(11)); // [36-46]
        // bacsString.Substring(46, 18).Should().Be("HACKNEY_ACCOUNT_NAME"); // [47-64]
        bacsString.Substring(64, 18).Should().Be($"{testHackneyUserReference}HSGRENT".PadRight(18)); // [65-82]
        bacsString.Substring(82, 18).Should().Be(testAccountNameResident.PadRight(18)); // [83-100]
        bacsString.Substring(100, 6).Should().Be(testProcessingDate.ToString().PadRight(6));
    }
}
