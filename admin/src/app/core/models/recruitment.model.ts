import { environment } from "src/environments/environment";
import { EditorVal, indexModel } from "./core.model";
import { FilesWithPrev } from "./FormElementsModel";

export class RecruitmentListFilter extends indexModel {
    title: string = '';
    departmentId: number = 0;
    publisherId: number = 0;
    fromDate: string = '';
    toDate: string = '';
    CategoryId: number = 0;
    subCategoryId: number = 0;
    blockTypeCode: number = 0;

}
export class RecruitmentList {
    id: number;
    title: string;
    departmentName: string;
    description: string;
    startDate: Date;
    lastDate: Date;
    publishedDate: Date;
    publisherName?: any;
    isApproved: boolean;
    totalPost: number;
    howTo?: any;
    slugUrl?: any;
    copySlugUrl?: any;
    modifiedByName: string;
    modifiedDate: Date;
    isActive: boolean;
    visitCount: number;
    categoryName: string;

}
export class Recruitment {
    recruitmentStartEndDate: Date[]
    docs: string[];
    thumbnailImage: string;
    notificationFile?: string;
    id: number;
    title: string;
    departmentId: number;
    salary: string;
    description: any;
    minAge: number;
    maxAge: number;
    startDate: Date;
    lastDate: Date;
    extendedDate: Date;
    publishedDate: Date;
    feePaymentLastDate: Date;
    correctionLastDate: Date;
    admitCardDate: Date;
    examMode: number;
    applyLink?: any;
    officialLink?: any;
    notificationLink?: any;
    publisherId: number;
    isApproved: boolean;
    totalPost: number;
    shortDesription?: any;
    slugUrl?: any;
    attachments: any[];
    thumbnail: string;
    categoryId: number;
    keywords: string;
    jobDesignations: number[];
    qualifications: number[];
    howToApplyAndQuickLinkLookup: any[] = [];
    descriptionJson: any;
    faqLookup: any[] = [];
    thumbnailCaption: string = ''
    socialMediaUrl: string = ''
    isCompleted: boolean = false;
    shouldReminder: Date;
    reminderDescription: string = ''
    upcomingCalendarCode: number;
    descriptionHindi: any;
    descriptionHindiJson: any;
    keywordsHindi: string;
    tags: any[] = [];
}

export class HowToApplyAndQuickLinkLookup {
    id: number = 0;
    title: string | null;
    titleHindi: string | null;
    description = new EditorVal();
    descriptionHindi = new EditorVal();
    isQuickLink: boolean = false;
    linkUrl: string | null;
    iconClass: string | null;
    isUpdate: boolean = false;
}

export class FaqLookup {
    id: number = 0;
    que: string | null;
    ans: string | null;
    queHindi: string | null;
    ansHindi: string | null;
    isUpdate: boolean = false;
}

export class CheckRecruitmentTitleModel {
    isActive: boolean;
    slugUrl: string;
    status: string;
    title: string;
}

