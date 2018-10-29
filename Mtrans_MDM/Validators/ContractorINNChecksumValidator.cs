using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mtrans_MDM.Validators
{
    /// TODO: вычисление контр. суммы для разных стран разное
    /// 
    public static class ContractorChecksumValidator
    {

        public static bool ValidateINN(string stringValue)
        {
            if (stringValue.Length == 10)
            {
                int checkSum = 0;
                int currentDigit = 0;
                int[] _weights = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                for (int i = 0; i < 9; i++)
                {
                    currentDigit = int.Parse(stringValue[i].ToString());
                    checkSum = checkSum + currentDigit * _weights[i];
                }
                int checkDigit = (checkSum % 11) % 10;

                return (checkDigit == currentDigit);
            }
            else if (stringValue.Length == 12)
            {
                int checkSum = 0;
                int currentDigit = 0;
                int[] _weights = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                for (int i = 0; i < 11; i++)
                {
                    currentDigit = int.Parse(stringValue[i].ToString());
                    checkSum = checkSum + currentDigit * _weights[i];
                }
                int checkDigit11 = (checkSum % 11) % 10;

                if (checkDigit11 != currentDigit)
                    return false;

                checkSum = 0;
                currentDigit = 0;
                _weights = new int[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8, };
                for (int i = 0; i < 12; i++)
                {
                    currentDigit = int.Parse(stringValue[i].ToString());
                    checkSum = checkSum + currentDigit * _weights[i];
                }
                int checkDigit12 = (checkSum % 11) % 10;

                return (checkDigit12 == currentDigit);
            }
            else
            {
                return false;
            }
        }
    }
}