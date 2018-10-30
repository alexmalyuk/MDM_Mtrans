using Mtrans_MDM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Mtrans_MDM.Validators
{
    /// TODO: вычисление контр. суммы для разных стран разное
    /// 
    public static class ContractorChecksumValidator
    {

        private static int CalculateChecksumMod11(string stringValue, int[] weights)
        {
            int sum = 0;
            for (int i = 0; i < stringValue.Length - 1; i++)
                sum = sum + int.Parse(stringValue[i].ToString()) * weights[i];
            return sum % 11;
        }

        public static bool ValidateINN(string stringValue)
        {

            // ua
            //
            if (!Regex.IsMatch(stringValue, @"^(\d{10})$|^(\d{12})$"))
                return false;

            int checkSum = 0;
            if (stringValue.Length == 10)
            {
                checkSum = CalculateChecksumMod11(stringValue, new int[] { -1, 5, 7, 9, 4, 6, 10, 5, 7 }) % 10;
            }
            else if (stringValue.Length == 12)
            {
                checkSum = CalculateChecksumMod11(stringValue, new int[] { 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 });
                if (checkSum >= 10)
                    checkSum = CalculateChecksumMod11(stringValue, new int[] { 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59 }) % 10;
            }
            int lastDigit = int.Parse(stringValue[stringValue.Length - 1].ToString());

            return (checkSum == lastDigit);
        }

        public static bool ValidateOKPO(string stringValue)
        {
            // ua
            //
            if (!Regex.IsMatch(stringValue, @"^(\d{8})$|^(\d{10})$"))
                return false;
            long intValue = long.Parse(stringValue);
            if (intValue > 30000000L && intValue <= 60000000L)
            {
                int checkSum = CalculateChecksumMod11(stringValue, new int[] { 7, 1, 2, 3, 4, 5, 6 });
                if (checkSum >= 10)
                    checkSum = CalculateChecksumMod11(stringValue, new int[] { 9, 3, 4, 5, 6, 7, 8 }) % 10;

                int lastDigit = int.Parse(stringValue[stringValue.Length - 1].ToString());
                return (checkSum == lastDigit);
            }
            else
            {
                int checkSum = CalculateChecksumMod11(stringValue, new int[] { 1, 2, 3, 4, 5, 6, 7 });
                if (checkSum >= 10)
                    checkSum = CalculateChecksumMod11(stringValue, new int[] { 3, 4, 5, 6, 7, 8, 9 }) % 10;

                int lastDigit = int.Parse(stringValue[stringValue.Length - 1].ToString());
                return (checkSum == lastDigit);
            }
        }
        public static bool ValidateVATNumber(string stringValue)
        {
            return true;
            // ua
            //
            if (!Regex.IsMatch(stringValue, @"^(\d{12})$"))
                return false;

        }
    }
}