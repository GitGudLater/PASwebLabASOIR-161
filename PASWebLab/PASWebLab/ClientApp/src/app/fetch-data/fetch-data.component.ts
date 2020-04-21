import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import './Tables';
import { ViewEmployers, ViewOrganizations, ViewContracts, ViewSupplies, AddContractForOrganizationsSupply, ContractEmploye, SupplyContract, SupplyOrganization } from './Tables';
import { DataService } from './data.service';

@Component({
  selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html',
    providers: [DataService]
})
export class FetchDataComponent {
    public employers: ViewEmployers[];
    public organizations: ViewOrganizations[];
    public contracts: ViewContracts[];
    public supplies: ViewSupplies[];
    public fullContract: AddContractForOrganizationsSupply = new AddContractForOrganizationsSupply;
    public contractEmploye: ContractEmploye = new ContractEmploye;
    public supplyOrganization: SupplyOrganization = new SupplyOrganization;
    public supplyContract: SupplyContract = new SupplyContract;
    public employe: ViewEmployers = new ViewEmployers;
    public organization: ViewOrganizations = new ViewOrganizations;
    public supply: ViewSupplies = new ViewSupplies;

    constructor(private dataService: DataService,private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    }

    addEmploye() {
        this.dataService.addEmploye(this.employe);
    }

    addOrganization() {
        this.dataService.addOrganization(this.organization);
    }

    addSupply() {
        this.dataService.addSupply(this.supply);
    }

    addContract() {
        this.dataService.addContract();
    }

    addContractEmploye() {
        this.dataService.addContractEmployeLink(this.contractEmploye.ContractId, this.contractEmploye.EmployeId);
    }

    addSupplyOrganization() {
        this.dataService.addSupplyOrganizationLink(this.supplyOrganization.SupplyId, this.supplyOrganization.OrganizationId);
    }

    addSupplyContract() {
        this.dataService.addSupplyContractLink(this.supplyContract.SupplyId, this.supplyContract.ContractId);
    }

    checkFields() {
        console.log(this.employers);
        console.log(this.contracts);
        console.log(this.organizations);
        console.log(this.supplies);
    }

    /*loadWeather() {
        this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast').subscribe(result => {
            this.forecasts = result;
        }, error => console.error(error));
    }*/

    ngOnInit() {
        this.loadtables();
    }
    loadtables() {
        this.dataService.getEmployers()
            .subscribe(data => ( this.employers = data ));
        this.dataService.getContracts()
            .subscribe(data => ( this.contracts = data));
        this.dataService.getOrganizations()
            .subscribe(data => ( this.organizations = data ));
        this.dataService.getSupplies()
            .subscribe(data => ( this.supplies = data ));
    }

    sendContractId(id: number) {
        this.contractEmploye.ContractId = id;
        this.supplyContract.ContractId = id;
    }

    sendSupplyId(id: number) {
        this.supplyContract.SupplyId = id;
        this.supplyOrganization.SupplyId = id;
    }

    /*insertDarta() {
        this.dataService.addData(this.fullContract);
    }*/

    generateSheets() {
        this.dataService.generateSheets();
    }

}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
