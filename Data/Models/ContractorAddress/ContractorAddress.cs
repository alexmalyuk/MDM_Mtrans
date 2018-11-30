using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ContractorAddress : IContractorAddress
    {
        public Guid Id { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }
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
