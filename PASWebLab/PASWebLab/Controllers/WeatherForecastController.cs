using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PASWebLab.Entities;
using PASWebLab.Services;
using PASWebLab.ViewModels;
using System.IO;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace PASWebLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private readonly IWebHostEnvironment _hostingEnvironment;


        IDbService dbService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDbService service, ILogger<WeatherForecastController> logger, IWebHostEnvironment hostingEnvironment)
        {
            dbService = service;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

             
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }

        [HttpGet("employers")]
        public IEnumerable<ViewEmployers> GetEmploers()
        {
            IEnumerable<ViewEmployers> employeTable = dbService.GetEmployers();
            return employeTable;
        }

        [HttpGet("contracts")]
        public IEnumerable<ViewContracts> GetContracts()
        {
            IEnumerable<ViewContracts> contractsTable = dbService.GetContracts();
            return contractsTable;
        }

        [HttpGet("organizations")]
        public IEnumerable<ViewOrganizations> GetOrganizations()
        {
            IEnumerable<ViewOrganizations> organizationsTable = dbService.GetOrganizations();
            return organizationsTable;
        }

        [HttpGet("supplies")]
        public IEnumerable<ViewSupplies> GetSupplies()
        {
            IEnumerable<ViewSupplies> supplyTable = dbService.GetSupplies();
            return supplyTable;
        }

        [HttpPost("postEmploye")]
        public IActionResult PutEmployeToServer([FromBody]ViewEmployers employeData)
        {
            dbService.AddEmployer(employeData);
            return Ok();
        }

        [HttpPost("postSupply")]
        public IActionResult PutSupplyToServer([FromBody]ViewSupplies suppliestData)
        {
            dbService.AddSupply(suppliestData);
            return Ok();

        }

        [HttpPost("postOrganization")]
        public IActionResult PutOrganizationToServer([FromBody]ViewOrganizations OrganizationsData)
        {
            dbService.AddOrganization(OrganizationsData);
            return Ok();

        }

        [HttpPost("postCElink")]
        public IActionResult PutCElink([FromBody]GetContractEmploye link)
        {
            dbService.AddContractEmployeLink(link);
            return Ok();

        }

        [HttpPost("postSClink")]
        public IActionResult PutSClink([FromBody]GetSupplyContract link)
        {
            dbService.AddSupplyContractLink(link);
            return Ok();

        }

        [HttpPost("postSOlink")]
        public IActionResult PutSOlink([FromBody]GetSupplyOrganization link)
        {
            dbService.AddSupplyOrganizationLink(link);
            return Ok();

        }

        [HttpPost("postExel")]
        public IActionResult PostExel()
        {
            var fileDownloadName = "report.xlsx";


            using (var package = dbService.CreateExel())
            {
                package.SaveAs(new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, fileDownloadName)));
            }


            //exel sheets generation
            return Ok();
        }

    }
}
