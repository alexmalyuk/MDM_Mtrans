using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public interface IContractorAddress
    {
        string PostalCode { get; set; }
        string Country { get; set; }
        string Region { get; set; }
        string District { get; set; }
        string City { get; set; }
        string Street { get; set; }
        string House { get; set; }
        string Flat { get; set; }

        string StringRepresentedAddress { get; set; }

    }
}
