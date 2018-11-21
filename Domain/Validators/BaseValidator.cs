using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class BaseValidator
    {
        internal static int CalculateChecksumMod11(string stringValue, int[] weights)
        {
            int sum = 0;
            for (int i = 0; i < stringValue.Length - 1; i++)
                sum = sum + int.Parse(stringValue[i].ToString()) * weights[i];
            return sum % 11;
        }
    }
}
