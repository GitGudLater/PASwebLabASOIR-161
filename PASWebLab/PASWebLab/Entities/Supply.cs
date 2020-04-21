using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities.SubLayerEntities;


namespace PASWebLab.Entities
{
    public class Supply
    {
        public int Id { get; set; }
        public SupplyContract Contract { get; set; }

        public ICollection<SupplyOrganizations> Organization { get; set; }
        public string HardwareType { get; set; }
        public string? UsersReview { get; set; } 

        public Supply()
        {
            Organization = new List<SupplyOrganizations>();
        }
    }
}
