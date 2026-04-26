import { indexModel } from "../core.model";

export class UserFilterModel extends indexModel {
    name: string = '';
    email: string = '';
    fromDate: string = '';
    toDate: string = '';
}

export interface FrontUserListModel {
    id: number;
    name: string | null;
    email: string;
    mobileNumber: string | null;
    dateOfBirth: string | null;
    profileImg: string | null;
    stateName: string | null;
    isActive: boolean;
    isDelete: boolean;
    modifiedByName: string | null;
    modifiedDate: string;
}

export class FeedbackFilterModel extends indexModel {
    type: number = 0;
    status: number = 0;
    fromDate: string = '';
    toDate: string = '';
}

export interface FeedbackListModel {
    id: number;
    userName: string;
    status: number;
    type: number;
    message: string;
    isActive: boolean;
    isDelete: boolean;
    modifiedByName: string | null;
    modifiedDate: string;
}
