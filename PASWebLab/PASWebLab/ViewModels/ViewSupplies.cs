using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASWebLab.ViewModels
{
    public class ViewSupplies
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public int OrganizationsCount { get; set; }//name
        public string HardwareType { get; set; }
        public string? UsersReview { get; set; }
    }
}
