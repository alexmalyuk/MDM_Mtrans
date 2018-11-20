using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class Const
    {
        public const string DataContractNameSpace = "http://www.metrans.com.ua";
    }

    public enum TypeOfCounterpartyEnum
    {
        LegalEntity = 1,
        Entrepreneur = 2
    }

    public enum CountryEnum
    {
        UA = 804,
        RU = 643,
        Other = -1
    }

    public enum TypeOfSubjectEnum
    {
        Unknown,
        Contractor,
        Contract,
        Nomenclature
    }

}
