using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASWebLab.Entities;

namespace PASWebLab.Entities.SubLayerEntities
{
    public class ContractsEmploye
    {
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int EmployeId { get; set; }
        public Employe Employe{get;set;}
    }
}
