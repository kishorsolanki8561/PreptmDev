import { indexModel } from '../core.model';
export class SubCategoryModel {
    id: number;
    name: string;
    categoryId: number;
    slugUrl: string;
    icon: string;
}

export class SubCategoryViewModel {
    id: number;
    name: string;
    categoryName: string;
    slugUrl: string;
    icon: string;
    modifiedByName: string;
    modifiedDate: string;
    isActive: boolean;
    totalRows: number;
}


export class SubCategoryFilterModel extends indexModel {
    name: string | null;
    categoryId: number;
    slugUrl: string | null;
    fromDate: string | null;
    toDate: string | null;
}