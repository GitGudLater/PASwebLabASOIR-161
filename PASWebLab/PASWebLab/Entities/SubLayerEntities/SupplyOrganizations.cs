using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities;

namespace PASWebLab.Entities.SubLayerEntities
{
    public class SupplyOrganizations
    {
        public int SupplyId { get; set; }
        public Supply Supply { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
