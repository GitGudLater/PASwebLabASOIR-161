import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ViewEmployers, ViewContracts, ViewOrganizations, ViewSupplies, AddContractForOrganizationsSupply, ContractEmploye, SupplyContract, SupplyOrganization } from './Tables';

@Injectable()
export class DataService {

    private url;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
    }

    getEmployers() {
        return this.http.get<ViewEmployers[]>(this.url + 'weatherforecast/employers');
    }

    getSupplies() {
        return this.http.get<ViewSupplies[]>(this.url + 'weatherforecast/supplies');
    }

    getOrganizations() {
        return this.http.get<ViewOrganizations[]>(this.url + 'weatherforecast/organizations');
    }

    getContracts() {
        return this.http.get<ViewContracts[]>(this.url + 'weatherforecast/contracts');
    }

    addContractEmployeLink(contractId: number, employeId: number) {
        let link: ContractEmploye = new ContractEmploye();
        link.EmployeId = employeId;
        link.ContractId = contractId;
        this.http.post(this.url + 'weatherforecast/postCElink', link).toPromise();
    }

    addSupplyContractLink(supplyId: number, contractid: number) {
        let link: SupplyContract = new SupplyContract();
        link.SupplyId = supplyId;
        link.ContractId = contractid;
        this.http.post(this.url + 'weatherforecast/postSClink', link).toPromise();
    }

    addSupplyOrganizationLink(supplyId: number, organizationId: number) {
        let link: SupplyOrganization = new SupplyOrganization();
        link.SupplyId = supplyId;
        link.OrganizationId = organizationId;
        this.http.post(this.url + 'weatherforecast/postSOlink', link).toPromise();
    }

    addContract() {
        let contract = new ViewContracts;
        this.http.post(this.url + 'weatherforecast/postContract', contract);
    }

    addEmploye(employe: ViewEmployers) {
        this.http.post(this.url + 'weatherforecast/postEmploye', employe).toPromise();

    }

    addSupply(supply: ViewSupplies) {
        this.http.post(this.url + 'weatherforecast/postSupply', supply).toPromise();

    }

    addOrganization(organization: ViewOrganizations) {
        this.http.post(this.url + 'weatherforecast/postOrganization', organization).toPromise();

    }

    generateSheets() {
        this.http.post(this.url + 'weatherforecast/postExel', null).toPromise();
    }
}
