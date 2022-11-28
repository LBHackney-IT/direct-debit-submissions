using System.Collections.Generic;
using System.Text.RegularExpressions;
using BaseListener.Domain;

namespace BaseListener.Helpers
{
    public class DirectDebitToBacsGenerator
    {
        public FilteredBacsItems DirectDebitEntitiesToBacsObjects(IEnumerable<DirectDebitDomain> directDebits)
        {
            var filteredBacsItems = new FilteredBacsItems();
            foreach (var directDebit in directDebits)
            {
                var directDebitValidation = DirectDebitValidForBacs(directDebit);
                if (directDebitValidation.IsValid)
                {
                    var bacsItem = DirectDebitToBacs(directDebit);
                    filteredBacsItems.ValidBacsItems.Add(bacsItem);
                }
                else
                {
                    filteredBacsItems.InvalidDirectDebitItems.Add(
                        new FilteredBacsItems.InvalidDirectDebitItem()
                        {
                            InvalidDirectDebitDomain = directDebit,
                            Information = directDebitValidation.Information
                        }
                    );
                }
            }
            return filteredBacsItems;
        }

        public string BacsObjectToOutputString(BacsDataEntity bacsDataEntity)
            //TODO: Convert ProcessingDate to bddYYY format
            // Should this be a method of BacsDataEntity?
            {
                return $"{bacsDataEntity.SortCodeResident, -6}" +
                       $"{bacsDataEntity.AccountNumberResident, -8}" +
                       $"{bacsDataEntity.TransactionType}" +
                       $"{bacsDataEntity.SortCodeHackney, -6}" +
                       $"{bacsDataEntity.AccountNumberHackney, -8}" +
                       $"{bacsDataEntity.FreeFormat}" +
                       $"{bacsDataEntity.RentPayment, -11}" +
                       $"{bacsDataEntity.AccountNameHackney, -18}" +
                       $"{bacsDataEntity.PaymentReferenceHackney + bacsDataEntity.FundHackney, -18}" +
                       $"{bacsDataEntity.AccountNameResident, -18}" +
                       $"{bacsDataEntity.ProcessingDate, -6}";
            }

        private BacsDataEntity DirectDebitToBacs(DirectDebitDomain directDebitDomain)
        {
            return new BacsDataEntity()
            {
                SortCodeResident = int.Parse(directDebitDomain.BranchSortCode),
                AccountNumberResident = directDebitDomain.AccountNumber,
                SortCodeHackney = 1, //TODO: Use Environment Variable
                AccountNumberHackney = 1, //TODO: Use Environment Variable
                AccountNameHackney = "UseEnvironmentVariable", //TODO: Use Environment Variable
                RentPayment = ResolveAmount(directDebitDomain),
                PaymentReferenceHackney = directDebitDomain.PaymentReference,
                FundHackney = directDebitDomain.Fund,
                AccountNameResident = directDebitDomain.AccountHolder,
                ProcessingDate = directDebitDomain.PreferredDate,
            };
        }
        private class BacsValidationResponse
        {
            public bool IsValid;
            public string Information;
        }

        private BacsValidationResponse DirectDebitValidForBacs(DirectDebitDomain directDebitDomain)
        {
            // TODO: Verify which ones exactly need validation
            bool sortCodeValid = Regex.Match(directDebitDomain.BranchSortCode, "[0-9]{6}").Success;
            bool accountNumberValid = Regex.Match(directDebitDomain.AccountNumber, "[0-9]{8}").Success;
            bool accValid = directDebitDomain.Acc == 0;
            bool transValid = directDebitDomain.Trans == 17;
            decimal? totalAmount = ResolveAmount(directDebitDomain);
            bool amountValid = totalAmount.HasValue;
            bool paymentReferenceValid = Regex.Match($"{directDebitDomain.PaymentReference}{directDebitDomain.Fund}", "[0-9]+").Success;
            bool fundValid = directDebitDomain.Fund == "HSGRENT";
            bool payerAccountNameValid = Regex.Match(directDebitDomain.AccountHolder, "[A-z]+").Success;
            bool preferredDateValid = Regex.Match(directDebitDomain.PreferredDate.ToString(), "[0-9]{2}").Success;

            var allValid = (sortCodeValid && accountNumberValid && accValid && transValid && amountValid &&
                                paymentReferenceValid && payerAccountNameValid && preferredDateValid);

            string InformationString()
            {
                if (allValid)
                {
                    return "";
                }

                string infoString = $"Direct Debit {directDebitDomain.Id} could not be converted:\n";

                if (!sortCodeValid){ infoString += $" Invalid Customer Sort Code {directDebitDomain.BranchSortCode},"; }
                if (!accountNumberValid){ infoString += $" Invalid Customer Account Number {directDebitDomain.AccountNumber},"; }
                if (!accValid || !transValid){ infoString += $" Invalid Acc Code {directDebitDomain.Acc} or Trans Code {directDebitDomain.Trans},"; }
                if (!amountValid){ infoString += $" Invalid Payment Amount {totalAmount},"; }
                if (!paymentReferenceValid){ infoString += $" Invalid Service User Reference {directDebitDomain.PaymentReference},"; }
                if (!fundValid){ infoString += $" Invalid Fund {directDebitDomain.Fund},"; }
                if (!payerAccountNameValid){ infoString += $" Invalid Customer Account Name {directDebitDomain.AccountHolder},"; }
                if (!preferredDateValid){infoString += $" Invalid Processing Date {directDebitDomain.PreferredDate},"; }

                return infoString;
            }

            return new BacsValidationResponse()
            {
                IsValid = allValid,
                Information = InformationString()
            };
        }

        private decimal? ResolveAmount(DirectDebitDomain directDebitDomain)
        {
            decimal? amountValue;

            bool AmountValid(decimal? amount)
            {
                // Match currency in pence, so no decimal points
                return Regex.Match(amount.ToString(), @"^[0-9]+$").Success;
            }

            if (directDebitDomain.FixedAmount.HasValue)
            {
                amountValue = (AmountValid(directDebitDomain.FixedAmount))
                    ? directDebitDomain.FixedAmount
                    : null;
            }
            else
            {
                amountValue = (AmountValid(directDebitDomain.Amount + directDebitDomain.AdditionalAmount))
                    ? directDebitDomain.Amount + directDebitDomain.AdditionalAmount
                    : null;
            }
            return amountValue;
        }
    }
}
