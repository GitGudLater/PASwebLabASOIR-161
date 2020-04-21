using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities.SubLayerEntities;


namespace PASWebLab.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public SupplyOrganizations Supply { get; set; }

        public int CountryCode { get; set; }
        public string Sity { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
