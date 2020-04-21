using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASWebLab.ViewModels
{
    public class ViewEmployers
    {
        public int Id { get; set; }
        public string DepartamentId { get; set; }
        public string EmployeFullName { get; set; }
        public string Qualification { get; set; }
        public int MoneyProfit { get; set; }
        public int Premials { get; set; }
        public string Month { get; set; }
        public int ContractsCount { get; set; }
    }
}
