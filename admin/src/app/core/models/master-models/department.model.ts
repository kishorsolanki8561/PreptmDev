import { indexModel } from "../core.model";

export class departmentListFilters extends indexModel {
    name: string = '';
    stateId: number = 0;
    fromDate: string = "";
    toDate: string = "";
    constructor() {
        super()
        this.orderBy = "ModifiedDate";
    }
}
export class departmentList {
    id: number;
    name: string;
    url: string;
    shortName?: any;
    logo?: any;
    stateName: string;
    phoneNumber?: any;
    modifiedByName: string;
    modifiedDate: string;
    isActive: boolean;
}

export class department {
    id: number;
    name: string;
    sortName?: any;
    address?: any;
    mapUrl?: any;
    email?: any;
    phoneNumber?: any;
    logo?: any;
    url: string;
    stateId: number;
    description: string;
    faceBookLink: string;
    twitterLink: string;
    wikipediaEnglishUrl: string;
    wikipediaHindiUrl: string;
}