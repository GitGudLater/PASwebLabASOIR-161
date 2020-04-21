using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.ViewModels;
using PASWebLab.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace PASWebLab.Services
{
    public interface IDbService
    {
        

        IEnumerable<ViewContracts> GetContracts();
        IEnumerable<ViewEmployers> GetEmployers();
        IEnumerable<ViewSupplies> GetSupplies();
        IEnumerable<ViewOrganizations> GetOrganizations();
        void AddContract(ViewContracts contract);
        void AddEmployer(ViewEmployers employe);
        void AddOrganization(ViewOrganizations organization);
        void AddSupply(ViewSupplies supply);
        void AddContractEmployeLink(GetContractEmploye link);
        void AddSupplyContractLink(GetSupplyContract link);
        void AddSupplyOrganizationLink(GetSupplyOrganization link);
        ExcelPackage CreateExel();
        
        //IEnumerable<PhoneDto> GetFavoriteList(string userName);
    }
}
