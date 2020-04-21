using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASWebLab.ViewModels
{
    public class GetContractForSupply
    {
        public int OrganizationId { get; set; }
        public int EmployeId { get; set; }
        public int SupplyId { get; set; }
        public int ContractId { get; set; }
        public string UserHardwareType { get; set; }
        public string UsersReview { get; set; }
        public int OrganizationCountryCode { get; set; }
        public string OrganizationSity { get; set; }
        public string OrganizationAdress { get; set; }
        public string OrganizationPhone { get; set; }
        public string OrganizationEmail { get; set; }
    }
}
