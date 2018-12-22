using Data.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public static class ModelConvertor
    {
        public static void ContractorToContractorViewModel(Contractor contractor, out ContractorViewModel contractorVM)
        {
            contractorVM = new ContractorViewModel();
            if (contractor != null)
            {
                contractorVM.Id = contractor.Id;
                contractorVM.Name = contractor.Name;
                contractorVM.FullName = contractor.FullName;
                contractorVM.INN = contractor.INN;
                contractorVM.OKPO = contractor.OKPO;
                contractorVM.VATNumber = contractor.VATNumber;
                contractorVM.CountryOfRegistration = contractor.CountryOfRegistration;
                contractorVM.Street = contractor.Address.Street;
                contractorVM.House = contractor.Address.House;
                contractorVM.Flat = contractor.Address.Flat;
                contractorVM.City = contractor.Address.City;
                contractorVM.District = contractor.Address.District;
                contractorVM.Region = contractor.Address.Region;
                contractorVM.Country = contractor.Address.Country;
                contractorVM.StringRepresentedAddress = contractor.Address.StringRepresentedAddress;
            }
        }
    }
}
