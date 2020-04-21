using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PASWebLab.Entities;
using PASWebLab.Entities.SubLayerEntities;
using PASWebLab.ViewModels;
using System.IO;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace PASWebLab.Services
{
    public class DbService:IDbService
    {
        DataContext db;
        public DbService(DataContext dataContext)
        {
            db = dataContext;
            if (!db.Employers.Any())
            {
                db.Employers.Add(new Employe() { EmployeFullName = "Solovyov Andrey Uladimiravich", DepartamentId = "IT", Qualification = "Engeneer", MoneyProfit = 100, Premials = 10, Month = "October" });
                db.Organizations.Add(new Organization() { Adress = "Test adress", Sity = "N", CountryCode = 555, Phone = "555-35-35", Email = "blablabla@company.com" });
                db.Supplies.Add(new Supply() { HardwareType = "IBM PC", UsersReview = "None" });
                db.Contracts.Add(new Contract());
                db.SaveChanges();
                Employe defaultEmploye = db.Employers.Include(data => data.Contracts).FirstOrDefault();
                Contract defaultContract = db.Contracts.Include(data => data.Employe).Include(data => data.Supply).FirstOrDefault();
                Organization defaultOrganization = db.Organizations.Include(data => data.Supply).FirstOrDefault();
                Supply defaultSupply = db.Supplies.Include(data => data.Organization).Include(data => data.Contract).FirstOrDefault();
                ContractsEmploye CElink = new ContractsEmploye() { ContractId = defaultContract.Id, EmployeId = defaultEmploye.Id };
                SupplyContract SClink = new SupplyContract() { ContractId = defaultContract.Id, SupplyId = defaultSupply.Id };
                SupplyOrganizations SOlink = new SupplyOrganizations() { SupplyId = defaultSupply.Id, OrganizationId = defaultOrganization.Id };
                defaultEmploye.Contracts.Add(CElink);
                defaultContract.Supply = SClink;
                defaultSupply.Organization.Add(SOlink);
                ///modify
                db.Entry(defaultEmploye).State = EntityState.Modified;//look for mistakes
                db.Entry(defaultContract).State = EntityState.Modified;//
                db.Entry(defaultSupply).State = EntityState.Modified;//
                db.SaveChanges();
            }

        }

        public IEnumerable<ViewContracts> GetContracts()
        {
            List<Contract> contracts = db.Contracts.Include(data =>data.Supply).Include(data => data.Employe).ToList();
            List<ViewContracts> viewContracts = new List<ViewContracts>();
            foreach (var contract in contracts)
            {
                int id;
                ViewContracts bufferContract = new ViewContracts() { Id = contract.Id };
                try {
                    bufferContract.EmployeId = contract.Employe.EmployeId;
                }
                catch { }
                try
                {
                    bufferContract.SupplyId = contract.Supply.SupplyId;
                }
                catch { }
                viewContracts.Add(bufferContract);
            }
            return viewContracts;
        }

        public IEnumerable<ViewEmployers> GetEmployers()
        {
            List<Employe> employers = db.Employers.Include(data => data.Contracts).ToList();
            List<ViewEmployers> viewEmployers = new List<ViewEmployers>();
            foreach(var employe in employers)
            {
                int count = 0;
                foreach (var link in employe.Contracts)
                {
                    if (link.EmployeId == employe.Id)
                        count++;
                }
                viewEmployers.Add(new ViewEmployers() { Id = employe.Id, DepartamentId = employe.DepartamentId, EmployeFullName = employe.EmployeFullName, Qualification = employe.Qualification, Month = employe.Month, MoneyProfit = employe.MoneyProfit, Premials = employe.Premials, ContractsCount= count });
            }
            return viewEmployers;
        }
        public IEnumerable<ViewSupplies> GetSupplies()
        {
            List<Supply> supplies = db.Supplies.Include(data => data.Contract).Include(data => data.Organization).ToList();
            List<ViewSupplies> viewSupplies = new List<ViewSupplies>();
            foreach(var supply in supplies)
            {
                int count=0;
                foreach(var link in supply.Organization)
                {
                    if (link.SupplyId == supply.Id)
                        count++;
                }
                int contractId;
                try {
                    contractId = supply.Contract.ContractId;
                    viewSupplies.Add(new ViewSupplies() { Id = supply.Id, UsersReview = supply.UsersReview, HardwareType = supply.HardwareType, ContractId = contractId, OrganizationsCount = count });
                }
                catch
                {
                    viewSupplies.Add(new ViewSupplies() { Id = supply.Id, UsersReview = supply.UsersReview, HardwareType = supply.HardwareType, OrganizationsCount = count });
                }
                //viewSupplies.Add(new ViewSupplies() { Id = supply.Id, UsersReview = supply.UsersReview, HardwareType = supply.HardwareType, ContractId = supply.Contract.ContractId, OrganizationsCount = count });
            }
            return viewSupplies;
        }
        public IEnumerable<ViewOrganizations> GetOrganizations()
        {
            List<Organization> organizations = db.Organizations.Include(data => data.Supply).ToList();
            List<ViewOrganizations> viewOrganizations = new List<ViewOrganizations>();
            foreach(var organization in organizations)
            {
                int supplyId;
                try
                {
                    supplyId = organization.Supply.SupplyId;
                    viewOrganizations.Add(new ViewOrganizations() { Id = organization.Id, Adress = organization.Adress, CountryCode = organization.CountryCode, Email = organization.Email, Phone = organization.Phone, Sity = organization.Sity, SupplyId = organization.Supply.SupplyId });
                }
                catch
                {
                    viewOrganizations.Add(new ViewOrganizations() { Id = organization.Id, Adress = organization.Adress, CountryCode = organization.CountryCode, Email = organization.Email, Phone = organization.Phone, Sity = organization.Sity });
                }
                /*try { }
                catch { }
                if (organization.Supply.SupplyId == null)
                    supplyId = 0;*/
                //viewOrganizations.Add(new ViewOrganizations() { Id = organization.Id, Adress = organization.Adress, CountryCode = organization.CountryCode, Email = organization.Email, Phone = organization.Phone, Sity = organization.Sity, SupplyId = organization.Supply.SupplyId });
            }
            return viewOrganizations;
        }
        public void AddContract(ViewContracts contract)
        {
            Contract _contract = new Contract();
            db.Contracts.Add(_contract);
            db.SaveChanges();
        }
        public void AddEmployer(ViewEmployers employe)
        {
            Employe _employe = new Employe() { DepartamentId = employe.DepartamentId, EmployeFullName = employe.EmployeFullName, MoneyProfit = employe.MoneyProfit, Month = employe.Month, Premials = employe.Premials, Qualification = employe.Qualification };
            db.Employers.Add(_employe);
            db.SaveChanges();
        }
        public void AddOrganization(ViewOrganizations organization)
        {
            Organization _organization = new Organization() { Adress = organization.Adress, CountryCode = organization.CountryCode, Email = organization.Email, Phone = organization.Phone, Sity = organization.Sity };
            db.Organizations.Add(_organization);
            db.SaveChanges();
        }
        public void AddSupply(ViewSupplies supply)
        {
            Supply _supply = new Supply() { HardwareType = supply.HardwareType, UsersReview = supply.UsersReview };
            db.Supplies.Add(_supply);
            db.SaveChanges();
        }

        public void AddContractEmployeLink(GetContractEmploye link)
        {
            Employe defaultEmploye = db.Employers.Include(data => data.Contracts).FirstOrDefault(data => data.Id == link.EmployeId);
            ViewContracts contract = new ViewContracts();
            this.AddContract(contract);
            db.SaveChanges();
            Contract defaultContract = db.Contracts.Include(data => data.Employe).Include(data => data.Supply).Last();
            ContractsEmploye CE = new ContractsEmploye() { ContractId = defaultContract.Id, EmployeId = link.EmployeId };
            defaultEmploye.Contracts.Add(CE);
            db.Entry(defaultEmploye).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void AddSupplyContractLink(GetSupplyContract link)
        {
            Supply defaultSupply = db.Supplies.Include(data => data.Contract).Include(data => data.Organization).FirstOrDefault(data => data.Id == link.SupplyId);
            Contract defaultContract = db.Contracts.Include(data => data.Employe).Include(data => data.Supply).FirstOrDefault(data => (/*data.Supply == null && data.Employe != null*/data.Id == link.Contractid));
            SupplyContract SC = new SupplyContract() { ContractId = defaultContract.Id, SupplyId = defaultSupply.Id };
            defaultSupply.Contract = SC;
            db.Entry(defaultSupply).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void AddSupplyOrganizationLink(GetSupplyOrganization link)
        {
            Supply defaultSupply = db.Supplies.Include(data => data.Contract).Include(data => data.Organization).FirstOrDefault(data => data.Id == link.SupplyId);
            SupplyOrganizations SO = new SupplyOrganizations() { OrganizationId = link.OrganizationId, SupplyId = defaultSupply.Id };
            defaultSupply.Organization.Add(SO);
            db.Entry(defaultSupply).State = EntityState.Modified;
            db.SaveChanges();
        }

        public ExcelPackage CreateExel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Vedomost zarplat";
            package.Workbook.Properties.Author = "Solovyov A.V.";

            var worksheet = package.Workbook.Worksheets.Add("Report");

            worksheet.Cells[1, 1].Value = "ФИО сотрудника";
            worksheet.Cells[1, 2].Value = "Оклад руб.";
            worksheet.Cells[1, 3].Value = "Премия руб.";
            worksheet.Cells[1, 4].Value = "Подоходный налог";
            worksheet.Cells[1, 5].Value = "Пенсионный налог";
            worksheet.Cells[1, 6].Value = "К выдаче руб.";

            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;

            List<Employe> employers = db.Employers.Include(data => data.Contracts).ToList();

            int countString = 0;
            for(int i = 0; i < employers.Count; i++)
            {
                countString++;

                worksheet.Cells[i + 1 + 1, 1].Value = employers[i].EmployeFullName;
                worksheet.Cells[i + 1 + 1, 2].Value = employers[i].MoneyProfit;
                worksheet.Cells[i + 1 + 1, 3].Value = employers[i].Premials;
                worksheet.Cells[i + 1 + 1, 4].Value = (employers[i].MoneyProfit + employers[i].Premials)*0.12;
                worksheet.Cells[i + 1 + 1, 5].Value = (employers[i].MoneyProfit + employers[i].Premials) * 0.01;
                worksheet.Cells[i + 1 + 1, 6].Value = (employers[i].MoneyProfit + employers[i].Premials) - (employers[i].MoneyProfit + employers[i].Premials) * 0.12 - (employers[i].MoneyProfit + employers[i].Premials) * 0.01;
            }
            worksheet.Cells[countString + 2, 1].Value = "Итого";
            double result = 0;
            for (int i = 0; i < employers.Count; i++)
            {
                result += (employers[i].MoneyProfit + employers[i].Premials) - (employers[i].MoneyProfit + employers[i].Premials) * 0.12 - (employers[i].MoneyProfit + employers[i].Premials) * 0.01;
            }
            worksheet.Cells[countString + 1 + 1, 6].Value = result;

            var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: countString + 2, toColumn: 6), "Data");

            tbl.ShowHeader = true;
            tbl.TableStyle = TableStyles.Medium1;
            tbl.ShowTotal = true;

            worksheet.Cells[countString + 1 + 1, 6].AutoFitColumns();


            return package;

        }

    }
}
