using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Serializable]
    [DataContract]
    public class ContractorAddress : IContractorAddress
    {
        public Guid Id { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string House { get; set; }
        [DataMember]
        public string Flat { get; set; }
        [DataMember]
        public string StringRepresentedAddress { get; set; }

        public Contractor Contractor { get; set; }

        public override string ToString()
        {
            // In most of the world, addresses are written in order from most specific to general
            // Street, City, District, Region, Postal code, Country

            if (string.IsNullOrEmpty(StringRepresentedAddress))
            {
                return string.Format("{0} {1}/{2}, {3}, {4}, {5}, {6}, {7}", Street, House, Flat, City, District, Region, PostalCode, Country);
            }
            else
            {
                return StringRepresentedAddress;
            }
        }
    }


}
