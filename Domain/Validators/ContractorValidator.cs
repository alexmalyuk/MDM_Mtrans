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
        private IContractorData contractor;

        public ContractorValidator(IContractorData contractor)
        {
            this.contractor = contractor;
        }

        public bool ValidateINN()
        {
            int lastDigit = 0;
            int checkSum = 0;

            switch (contractor.CountryOfRegistration)
            {
                #region UA
                case Data.CountryEnum.UA:
                    // Контрольная сумма ИНН для Украины http://1s.biz.ua/public/284564/

                    if (string.IsNullOrEmpty(contractor.INN))
                        return false;

                    if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.Entrepreneur)
                    {
                        if (!Regex.IsMatch(contractor.INN, @"^(\d{10})$"))
                            return false;
                        checkSum = CalculateChecksumMod11(contractor.INN, new int[] { -1, 5, 7, 9, 4, 6, 10, 5, 7 }) % 10;
                    }
                    else if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                    {
                        if (!Regex.IsMatch(contractor.INN, @"^(\d{12})$"))
                            return false;
                        checkSum = CalculateChecksumMod11(contractor.INN, new int[] { 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 });
                        if (checkSum >= 10)
                            checkSum = CalculateChecksumMod11(contractor.INN, new int[] { 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59 }) % 10;
                    }
                    else
                        return true;

                    lastDigit = int.Parse(contractor.INN[contractor.INN.Length - 1].ToString());
                    return (checkSum == lastDigit);
                #endregion

                #region RU
                case Data.CountryEnum.RU:
                    // Контрольная сумма ИНН для россии https://www.egrul.ru/test_inn.html

                    int len = contractor.INN.Length;

                    if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.Entrepreneur)
                    {
                        if (!Regex.IsMatch(contractor.INN, @"^(\d{12})$"))
                            return false;

                        int checkSum1 = CalculateChecksumMod11(contractor.INN, new int[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8, 0 });
                        if (checkSum1 > 9)
                            checkSum1 %= 10;

                        if (checkSum1 != int.Parse(contractor.INN[len - 2].ToString()))
                            return false;

                        int checkSum2 = CalculateChecksumMod11(contractor.INN, new int[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8, 0 });
                        if (checkSum2 > 9)
                            checkSum2 %= 10;

                        return (checkSum2 != int.Parse(contractor.INN[len - 1].ToString()));
                    }
                    else if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                    {
                        if (!Regex.IsMatch(contractor.INN, @"^(\d{10})$"))
                            return false;

                        checkSum = CalculateChecksumMod11(contractor.INN, new int[] { 2, 4, 10, 3, 5, 9, 4, 6, 8, 0 }) ;
                        if (checkSum > 9)
                            checkSum %= 10;

                        lastDigit = int.Parse(contractor.INN[len - 1].ToString());
                        return (checkSum == lastDigit);
                    }
                    else
                        return false;
                #endregion

                default:
                    return true;
            }
        }

        public bool ValidateOKPO()
        {
            int checkSum = 0;

            switch (contractor.CountryOfRegistration)
            {
                #region UA
                case Data.CountryEnum.UA:

                    if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                    {
                        if (string.IsNullOrEmpty(contractor.OKPO) || !Regex.IsMatch(contractor.OKPO, @"^(\d{8})$|^(\d{10})$"))
                            return false;

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
                        return (checkSum == int.Parse(contractor.OKPO[contractor.OKPO.Length - 1].ToString()));
                    }
                    else
                        return true;
                #endregion

                #region RU
                case Data.CountryEnum.RU:

                    // http://www.temabiz.com/terminy/chto-takoe-kod-okpo.html 
                    //
                    if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.LegalEntity)
                    {
                        if (string.IsNullOrEmpty(contractor.OKPO) || !Regex.IsMatch(contractor.OKPO, @"^(\d{8})$"))
                            return false;

                        checkSum = CalculateChecksumMod11(contractor.OKPO, new int[] { 1, 2, 3, 4, 5, 6, 7 }) % 10;
                    }
                    else if (contractor.TypeOfCounterparty == Data.TypeOfCounterpartyEnum.Entrepreneur)
                    {
                        if (string.IsNullOrEmpty(contractor.OKPO) || !Regex.IsMatch(contractor.OKPO, @"^(\d{10})$"))
                            return false;

                        checkSum = CalculateChecksumMod11(contractor.OKPO, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }) % 10;
                    }
                    else
                        return true;

                    return (checkSum == int.Parse(contractor.OKPO[contractor.OKPO.Length - 1].ToString()));
                #endregion

                default:
                    return true;
            }
        }
        public bool ValidateVATNumber()
        {
            switch (contractor.CountryOfRegistration)
            {
                case Data.CountryEnum.UA:
                    if (string.IsNullOrEmpty(contractor.VATNumber))
                        return true;

                    return Regex.IsMatch(contractor.VATNumber, @"^(\d{8,9})$");

                ///TODO: Контрольная сумма НДС для Украины

                case Data.CountryEnum.RU:

                    ///TODO: Контрольная сумма НДС для россии
                    return true;

                default:
                    return true;
            }

        }

    }
}
