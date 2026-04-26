import { EditorVal, indexModel } from "./core.model";

export class SchemeRequestModel {
    id: number | null;
    title: string | null;
    titleHindi: string | null;
    departmentId: number | null;
    stateId: number | null;
    minAge: number | null;
    maxAge: number | null;
    startDate: string | Date;
    endDate: string | Date;
    extendedDate: string | null;
    correctionLastDate: string | null;
    postponeDate: string | null;
    levelType: number;
    mode: number;
    officelLink: string | null;
    applyLink: string | null;
    shortDescription: string | null;
    shortDescriptionHindi: string | null;
    keywords: string | null;
    keywordsHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    thumbnail: string | null;
    slug: string | null;
    fee: string | null;
    documentIds: number[] | null;
    eligibilityIds: number[] | null;
    contactDetail: SchemeContentDetailsLookup[] = [];
    howToApplyAndQuickLinkLookup: SchemeHowToApplyAndQuickLinkLookup[]=[];
    schemeAttachmentLookups: SchemeAttchamentLookupModel[] = [];
    schemeStartEndDate: Date[]
    faqLookups: any[] = []
    isUpcoming: boolean;
    shouldReminder: Date;
    reminderDescription: string = '';
    isCompleted: boolean = false;
    descriptionJson: string = '';
    descriptionHindiJson: string = '';
    publishedDate: any;
}

export class SchemeViewModel {
    id: number;
    title: string | null;
    titleHindi: string | null;
    departmentId: number | null;
    stateId: number | null;
    minAge: number | null;
    maxAge: number | null;
    startDate: string | Date;
    endDate: string | Date;
    extendedDate: string | null;
    correctionLastDate: string | null;
    postponeDate: string | null;
    levelType: number;
    mode: number;
    officelLink: string | null;
    applyLink: string | null;
    shortDescription: string | null;
    shortDescriptionHindi: string | null;
    keywords: string | null;
    descriptionHindi: EditorVal | null;
    description: EditorVal | null;
    thumbnail: any;
    slug: string | null;
    fee: string | null;
    howToApply: any[];
    otherLinksList: any[];
    documentIds: number[] | null;
    eligibilityIds: number[] | null;
    contactDetail: SchemeContentDetailsLookup[];
    howToApplyAndQuickLinkLookup: SchemeHowToApplyAndQuickLinkLookup[];
    schemeAttachmentLookups: SchemeAttchamentLookupModel[] = [];
    schemeStartEndDate: Date[]
    isCompleted: boolean = false;
    shouldReminder: Date;
    reminderDescription: string = ''
    descriptionJson: string = '';
    descriptionHindiJson: string = '';
}

export class SchemeViewListModel {
    id: number;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    slug: string | null;
    modifiedByName: string | null;
    modifiedDate: string;
    status: number;
    visitCount: number;
    publisherName: string;
    publishedDate: Date;
    isActive: boolean;
    copySlugUrl: string;
}

export class SchemeFilterModel extends indexModel {
    title: string | null;
    titleHindi: string | null;
    fromDate: string | null;
    toDate: string | null;
}

export class SchemeDocumentLookupModel {
    schemeId: number;
    lookupId: number;
    description: string | null;
    descriptionHindi: string | null;
}

export class SchemeAttchamentLookupModel {
    id: number = 0;
    title: string | null;
    titleHindi: string | null;
    description: string | null;
    descriptionHindi: string | null;
    type: number;
    path: any;
    attchPath: any;
    IndexNumber: number;
}

export class SchemeHowToApplyAndQuickLinkLookup {
    id: number = 0;
    title: string | null;
    titleHindi: string | null;
    description: any;
    descriptionHindi: any;;
    descriptionJson: any;;
    descriptionHindiJson: any;;
    isQuickLink: boolean;
    linkUrl: string | null;
    iconClass: string | null;
}

export class SchemeContentDetailsLookup {
    id: number = 0;
    departmentId: number;
    schemeId: number | null;
    nodalOfficerName: string | null;
    nodalOfficerNameHindi: string | null;
    phoneNo: string | null;
    email: string | null;
}
export class FaqLookup {
    id: number = 0;
    que: string | null;
    ans: string | null;
    queHindi: string | null;
    ansHindi: string | null;
    isUpdate: string | null;
}

export class CheckSchemeTitleModel {
    isActive: boolean;
    slugUrl: string;
    status: string;
    title: string;
}
