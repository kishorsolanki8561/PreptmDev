import { Index } from "./index.model";
import { PAGE_SIZE } from "../fixed-values";

export class Post {
    id: number;
    title: string;
    thumbnail: string;
    departmentLogo?: string;
    startDate: string;
    lastDate: any;
    totalPost: number;
    slugUrl: string;
    blockTypeSlug: string;
    moduleName: string;
    totalRows: number;
    moduleText: string;
    bookmarkId?: number;
    BlockTypeId?: number;
    publishedDate?: any;
    reminingFewDays?: number;
    daysDifference?: boolean;
    isNew?: boolean;

}

export class CockpitPanelsPosts {
    recentPosts: Post[];
    popularPosts: Post[];
    recruitments: Post[];
    privateRecruitments: Post[];
    admissions: Post[];
    exams: Post[];
    papers: Post[];
    results: Post[];
    syllabus: Post[];
    admitCards: Post[];
    schemes: Post[];
    upComingPosts: Post[];
    expiredSoonPosts: Post[];
}

export class PostListFilter extends Index {
    title: string = '';
    departmentId: number = 0;
    fromDate: string = '';
    toDate: string = '';
    qualificationId: number = 0;
    jobDesignationId: number = 0;
    categorySlug: string = '';
    categoryId: number = 0;
    isPrivate?: boolean | null = false;
    subCategorySlug: string = '';
    blockTypeSlug: string = '';
    eligibilityId: number = 0;
    schemelevelCode: number = 0;
    searchText: string = '';
}

export class ArticleListFilter extends Index {

    title: string = ""
    articleTypeSlug: string = ""
    searchText: string = ""
    stateId: number = 0
    tagTypeSlug: string = ""
}


export class Search {
    page: number = 1
    pageSize = PAGE_SIZE
    searchText: string = '';
}
export class BookmarkFilter {
    page: number = 1
    pageSize = PAGE_SIZE
}

export class PostDetailsFilter {
    isRecruitment: boolean;
    slugUrl: string;

}


export class PostDetails {
    id: number;
    title: string;
    departmentName: string;
    departmentSlugUrl: string;
    departmentLogo: string;
    salary: string;
    description: string;
    minAge: number;
    maxAge: number;
    startDate: string;
    lastDate: string;
    sortLinks: string;
    publishedDate: Date;
    extendedDate: string;
    feePaymentLastDate: Date;
    correctionLastDate: Date;
    admitCardDate: string;
    examMode: number;
    applyLink: string;
    officialLink: string;
    notificationLink: string;
    totalPost: number;
    howToApply: {
        desc: string,
        image: string,
        title: string
    }[];
    otherLinksList: { link: string, title: string }[]
    shortDesription: string;
    slugUrl: string;
    thumbnail: string;
    documents: string[];
    recruitmentJobDesignations: string[];
    recruitmentQualifications: string[];
    relatedPost: Post[];
    bookmarkId: number | null;
    moduleName: string;
    relatedBlockContent: Post[]
    relatedCatSubCat: Post[];
    stateName: string;
    blockTypeId: number;
}
export class RecruitmentDetails {
    id: number
    title: string
    departmentName: string
    departmentSlugUrl: string
    departmentLogo: string
    thumbnail: string
    thumbnailCredit: string
    salary: number
    description: string
    minAge: number
    maxAge: number
    startDate: string
    lastDate: string
    extendedDate: string
    feePaymentLastDate: string
    correctionLastDate: string
    admitCardDate: string
    publishedDate: any
    examMode: number
    applyLink: string
    officialLink: string
    notificationLink: string
    totalPost: number
    bookmarkId: number
    shortDesription: string
    sortLinks: string
    keywords: string;
    howToApply: {
        title: string
        description: string
        url: string
    }[]
    otherLinksList: {
        title: string
        description: string
        url: string
        iconClass: string
    }[]
    slugUrl: string
    documents: string[]
    designation: string[]
    qualification: string[]
    stateName: string
    relatedCatSubCat: Post[]
    relatedBlockContent: Post[]
    moduleName: string
    moduleText: string
    isPrivate: boolean;
    moduleSlug: string
    categoryName: string
    subCategoryName: string
    faQs: any[] = []
    blockTypeId: number;
}

export class AdmissionDetails {
    id: number
    title: string
    departmentName: string
    departmentSlugUrl: string
    departmentLogo: string
    salary: number
    description: string
    minAge: number
    maxAge: number
    thumbnail: string;
    keywords: string;
    thumbnailCredit: string;
    startDate: string
    lastDate: string
    extendedDate: string
    feePaymentLastDate: string
    correctionLastDate: string
    admitCardDate: string
    publishedDate: any
    examMode: number
    applyLink: string
    officialLink: string
    notificationLink: string
    totalPost: number
    bookmarkId: number
    shortDesription: string
    sortLinks: string
    howToApply: {
        title: string
        description: string
        url: string
    }[]
    otherLinksList: {
        title: string
        description: string
        url: string
    }[]
    slugUrl: string
    documents: string[]
    designation: string[]
    qualification: string[]
    stateName: string
    relatedCatSubCat: Post[]
    relatedBlockContent: Post[]
    moduleName: string
    moduleText: string
    moduleSlug: string
    categoryName: string
    subCategoryName: string
    blockTypeId: number;

}


export class BlockContaintDetails {
    id: number
    title: string
    description: string
    url: string
    departmentLogo: string
    thumbnail: string;
    publishedDate: string;
    thumbnailCredit: string;
    departmentName: string
    departmentSlugUrl: string
    sortLinks: string
    stateName: string
    notificationLink: string
    startDate: string
    moduleName: string
    moduleText: string
    moduleSlug: string
    bookmarkId: null | number
    lastDate: string;
    extendedDate: string
    correctionLastDate: string;
    summary: string;
    feePaymentLastDate: string;
    urlLabel: string;
    recruitment: ShortRecruitment
    documents: string[]
    howToApply: HowToApply[]
    otherLinksList: OtherLinks[]
    relatedRecruitment: Post[]
    relatedBlockContent: Post[]
    keywords: string
    blockTypeId: number;
    faQs: any[] = []
}


export class ArticleDetails {
    id: number
    title: string
    thumbnail: string;
    description: string
    articleType: string
    summary: string
    keywords: string
    thumbnailCredit: string
    tags: { tag: string, slugUrl: string }[]
    articleFaqs: any[] = []
    recruitments: Post[]
    blockContents: Post[]
    modifiedDate:string
}

export class ArticleList {
    id: number
    title: string
    thumbnail: string
    moduleName: string
    slugUrl: string
    modifiedDate: string
    totalRows: number
}

export interface ShortRecruitment {
    tittle: any
    slugUrl: string
    shortDesription: string
    moduleSlug: string
    jobDesignation: string
    qualification: string
}


export class BookmarkReq {
    bookmarkId: number;
    postId: number;
    blockTypeId?: number;
}


export class SchemeModel {
    title: string
    departmentName: string
    state: string
    publishedDate: string
    minAge: number
    maxAge: number
    startDate: string
    endDate: string
    sortLinks: string
    extendedDate: string
    correctionLastDate: string
    postponeDate: string
    levelType: number
    mode: number
    officialLink: string
    applyLink: string
    shortDescription: string
    keywords: string
    description: string
    thumbnail: string
    thumbnailCredit: string;
    fee: string
    documents: string[]
    eligibilitys: string[]
    attachments: Attachment[]
    otherSchemes: Post[]
    contactDetails: ContactDetails[]
    howToApplys: HowToApply[]
    quickLinks: QuickLink[]
    moduleText: string
    moduleName: string
    moduleSlug: string
    departmentSlugUrl: string
    bookmarkId: number | null;
    blockTypeId: number;
    faQs: any[] = []
}

export class Attachment {
    title: string
    description: string
    path: string
    type: number
}

export class ContactDetails {
    nodalOfficerName: string
    departmentName: string
    phoneNo: string
    email: string
}

export class HowToApply {
    description: string
    title: string
    url: string
}
export class OtherLinks {
    description: string
    title: string
    url: string
    iconClass: string
}

export class QuickLink {
    description: string
    title: string
    url: string
    iconClass: string
}



