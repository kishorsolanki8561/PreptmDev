import { indexModel } from "../core.model";

export class menuListFilters extends indexModel {
    fromDate: string = '';
    toDate: string = '';
    menuName: string = '';
    hashChild: number = -1;
    parentMenuId: number = 0;
}
export class menuList {
    id: number;
    menuName: string;
    displayName: string;
    hashChild: boolean;
    parentId: number;
    parentMenuName: string;
    position: number;
    isActive: boolean;
    modifiedDate: Date;
    modifiedByName: string;
}

export class menu {
    id: number;
    menuName: string;
    displayName: string;
    hashChild: boolean;
    parentId: number;
    position: number;
    iconClass: string;
    userTypeCodes: number[];
}