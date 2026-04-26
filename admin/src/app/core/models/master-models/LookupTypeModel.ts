import { indexModel } from '../core.model';

export class LookupTypeModel {
    id: number;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
}

export class LookupTypeViewModel {
    id: number;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
}

export class LookupTypeViewListModel {
    id: number;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
    modifiedByName: string | null;
    modifiedDate: string;
    isActive: boolean;
}

export class LookupTypeFilterModel extends indexModel {
    title: string | null;
    titleHindi: string | null;
    fromDate: string | null;
    toDate: string | null;
}