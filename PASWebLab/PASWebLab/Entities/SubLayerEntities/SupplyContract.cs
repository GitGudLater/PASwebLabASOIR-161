using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities;


namespace PASWebLab.Entities.SubLayerEntities
{
    public class SupplyContract
    {
        public int SupplyId { get; set; }
        public Supply Supply { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
