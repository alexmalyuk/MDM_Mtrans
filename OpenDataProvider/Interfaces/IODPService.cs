using OpenDataProvider;

namespace OpenData
{
    public interface IODPService
    {
        ContractorODPModel GetContractorData(string okpo);
    }
}