export class ViewEmployers {
    Id :number
    DepartamentId: string;
    EmployeFullName: string;
    Qualification: string;
    MoneyProfit: number;
    Premials: number;
    Month: string;
    ContractsCount: number;
}

export class ViewSupplies {
    Id: number;
    ContractId: number;
    OrganizationsCount: number;
    HardwareType: string;
    UsersReview: string;
}

export class ViewOrganizations {
    Id: number;
    SupplyId: number;
    CountryCode: number;
    Sity: string;
    Adress: string;
    Phone: string;
    Email: string;
}

export class ViewContracts {
    Id: number;
    EmployeId: number;
    SupplyId: number;
}

export class AddContractForOrganizationsSupply {
    OrganizationId: number;
    EmployeId: number;
    SupplyId: number;
    ContractId: number;
    UserHardwareType: string;
    UsersReview: string;
    OrganizationCountryCode: number;
    OrganizationSity: string;
    OrganizationAdress: string;
    OrganizationPhone: string;
    OrganizationEmail: string;
}

export class ContractEmploye {
    ContractId: number;
    EmployeId: number;
}
export class SupplyContract {
    SupplyId: number;
    ContractId: number;
}
export class SupplyOrganization {
    SupplyId: number;
    OrganizationId: number;
}
