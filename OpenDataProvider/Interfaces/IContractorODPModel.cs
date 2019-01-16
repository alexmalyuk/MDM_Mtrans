using System;

namespace OpenDataProvider
{
    public interface IContractorODPModel
    {
        string boss { get; set; }
        string edrpou { get; set; }
        string inn { get; set; }
        string kved { get; set; }
        string location { get; set; }
        string name { get; set; }
        DateTime? registrationDate { get; set; }
        string shortName { get; set; }
        string state { get; set; }
    }
}