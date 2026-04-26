import { indexModel } from "../core.model";

export class PageListFilter extends indexModel {
    fromDate: string = '';
    toDate: string = '';
    pageName: string = '';
    menuId: number = 0;
}

export class PageList {
    id: number;
    name: string;
    menuId: number;
    menuName: string;
    isActive: boolean;
    isDeleted: boolean;
    modifiedDate: Date;
    modifiedByName: string;
}

export class Page {
    id: number;
    name: string;
    menuId: number;
    isActive: boolean;
    isDeleted: boolean;
    icon:string;
    pageUrls: {
        id: number,
        url: string,
        permissionType: number
    }[];
}