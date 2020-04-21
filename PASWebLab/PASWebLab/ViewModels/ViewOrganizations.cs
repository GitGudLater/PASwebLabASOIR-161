using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASWebLab.ViewModels
{
    public class ViewOrganizations
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public int CountryCode { get; set; }
        public string Sity { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
