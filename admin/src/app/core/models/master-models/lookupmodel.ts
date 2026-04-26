import { indexModel } from "../core.model";

export class LookupModel {
    id: number;
    lookupTypeId: number;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
}

export class LookupViewModel {
    id: number;
    lookupTypeId: number;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
}

export class LookupViewListModel {
    id: number;
    lookupTypeName: string | null;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
    modifiedByName: string | null;
    modifiedDate: string;
    isActive: boolean;
}

export class LookupFilterModel extends indexModel {
	lookupTypeId: number;
	title: string | null;
	titleHindi: string | null;
	fromDate: string | null;
	toDate: string | null;
}