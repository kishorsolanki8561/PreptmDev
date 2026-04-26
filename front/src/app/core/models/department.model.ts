export class DepartmentDetail {
    id: number
    name: string
    url: string
    shortName: string
    logo: string
    stateName: string
    phoneNumber: string
    description: string
    faceBookLink: string
    twitterLink: string
    mapUrl: string
    address: string
    slugUrl: string
    email: string
    wikipediaUrl: any
    relatedData: RelatedRecruitment[]

}

export class RelatedRecruitment {
    id: number
    title: string
    thumbnail: any
    startDate: string
    lastDate: string
    totalPost: number
    slugUrl: string
    moduleName: any
    moduleText: any
    blockTypeSlug: any
    totalRows: number
}

export class DepartmentDetailsFilter {
    isRecruitment: boolean;
    slugUrl: string;

}