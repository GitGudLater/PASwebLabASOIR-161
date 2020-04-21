using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities.SubLayerEntities;

namespace PASWebLab.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public SupplyContract Supply { get; set; }
        public ContractsEmploye Employe { get; set; }

    }
}
