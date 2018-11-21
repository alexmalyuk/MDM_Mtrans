using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class ContractorValidator : BaseValidator
    {
        private Contractor contractor;

        public ContractorValidator(Contractor contractor)
        {
            this.contractor = contractor;
        }

        public bool ValidateINN()
        {
            switch (contractor.Country)
            {
                case Data.CountryEnum.UA:
                    if (string.IsNullOrEmpty(contractor.INN))
                        return false;

                    int checkSum = 0;

                    if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                    {
                        if (!Regex.IsMatch(contractor.INN, @"^(\d{10})$"))
                            return false;
                        checkSum = CalculateChecksumMod11(contractor.INN, new int[] { -1, 5, 7, 9, 4, 6, 10, 5, 7 }) % 10;
                    }
                    else if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.Entrepreneur)
                    {
                        if (!Regex.IsMatch(contractor.INN, @"^(\d{12})$"))
                            return false;
                        checkSum = CalculateChecksumMod11(contractor.INN, new int[] { 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 });
                        if (checkSum >= 10)
                            checkSum = CalculateChecksumMod11(contractor.INN, new int[] { 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59 }) % 10;
                    }
                    else
                        return true;

                    int lastDigit = int.Parse(contractor.INN[contractor.INN.Length - 1].ToString());
                    return (checkSum == lastDigit);

                case Data.CountryEnum.RU:

                    ///TODO: Контрольная сумма ИНН для россии
                    return true;
                    break;

                default:
                    return true;
            }
        }

        public bool ValidateOKPO()
        {
            switch (contractor.Country)
            {
                case Data.CountryEnum.UA:

                    if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                    {
                        if (string.IsNullOrEmpty(contractor.OKPO) || !Regex.IsMatch(contractor.OKPO, @"^(\d{8})$|^(\d{10})$"))
                            return false;

                        int checkSum = 0;
                        long intValue = long.Parse(contractor.OKPO);
                        if (intValue > 30000000L && intValue <= 60000000L)
                        {
                            checkSum = CalculateChecksumMod11(contractor.OKPO, new int[] { 7, 1, 2, 3, 4, 5, 6 });
                            if (checkSum >= 10)
                                checkSum = CalculateChecksumMod11(contractor.OKPO, new int[] { 9, 3, 4, 5, 6, 7, 8 }) % 10;
                        }
                        else
                        {
                            checkSum = CalculateChecksumMod11(contractor.OKPO, new int[] { 1, 2, 3, 4, 5, 6, 7 });
                            if (checkSum >= 10)
                                checkSum = CalculateChecksumMod11(contractor.OKPO, new int[] { 3, 4, 5, 6, 7, 8, 9 }) % 10;
                        }
                        int lastDigit = int.Parse(contractor.OKPO[contractor.OKPO.Length - 1].ToString());
                        return (checkSum == lastDigit);
                    }
                    else
                        return true;

                case Data.CountryEnum.RU:

                    ///TODO: Контрольная сумма ОКПО для россии
                    return true;
                    break;

                default:
                    return true;
            }
        }
        public bool ValidateVATNumber()
        {
            switch (contractor.Country)
            {
                case Data.CountryEnum.UA:
                    if (string.IsNullOrEmpty(contractor.VATNumber))
                        return true;

                    return Regex.IsMatch(contractor.VATNumber, @"^(\d{9,12})$");

                ///TODO: Контрольная сумма НДС для Украины

                case Data.CountryEnum.RU:

                    ///TODO: Контрольная сумма НДС для россии
                    return true;
                    break;

                default:
                    return true;
            }

        }

    }
}
