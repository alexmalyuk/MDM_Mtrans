using OpenDataProvider;

namespace OpenData
{
    public interface IODPService
    {
        IContractorODPModel GetContractorData(string okpo);
    }
}