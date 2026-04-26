import { EditorVal, indexModel } from "./core.model";

export class NotesFilterDTO extends indexModel {
    title: string | null;
    titleHindi: string | null;
    fromDate: string | null;
    toDate: string | null;
}

export class NotesDetails {
    id: number
    categoryId: number
    departmentId: number
    qualificationId: number
    stateId: number
    title: string
    titleHindi: string
    slugUrl: string
    description: EditorVal
    descriptionHindi: EditorVal
    keywords: string
    keywordsHindi: string
    descriptionJson: string
    descriptionJsonHindi: string
    status: number
    paperTags: number[]
    publishedDate:string;
    papperSubjects: {
        id: number
        subjectName: string
        subjectNameHindi: string
        yearId: number
        path: string
    }[]
}

export class NotesList {
    id: number;
    title: string;
    titleHindi: string;
    slugUrl: string;
    modifiedByName: string;
    modifiedDate: string;
    status: number;
    visitCount: number;
    publisherName: string;
    publisherDate: string;
    isActive: boolean;
}
