import { DdlItem } from './core.model';

export const DefaultTableLength = 10;
export const MaxFileSize = 1000000; //KB
export const MinFileSize = 0.0001; // KB

//file format
export class FileFormat {
  static imageFormat = ['jpeg', 'jpg', 'png','webp','svg'];
  static videoFormat = ['mp4'];
  static pdfFormat = ['pdf'];
}
//file format
export class Pattern {
  static urlPattern = /^(http|https):\/\/(?:[\w-]+\.)+[a-z]{3,6}\S*$/;
  static mobileNumber = /^([0|\+[0-9]{1,5})?-?([0-9]{10})$/;
  static onlyNumber = /^[0-9]+$/;
  static email = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
}

//messages

export class Message {
  static error = {
    server: 'Server Error occured!!, Please contact to Kishor',
    notFound: 'Service Not Found',
    unauthorized: 'Please Login First',
  };
  static confirm = {
    delete: 'Do you want to delete this ?',
  };

  static formError = {
    invalidForm: 'Some fields are incorrect in the form',
  };

  //#region <<Alert Message >>
  static incorrectForm = 'Some fields are incorrect in the form';
  // static SaveSuccess = 'Record successfully saved...!';
  // static SaveFail = 'Record failed to save...!';
  // static UpdateSuccess = 'Record successfully updated...!';
  // static UpdateFail = 'Record failed to update...!';
  // static ConfirmUpdate = 'Are you Sure update this record?';
  //#endregion
}
export enum Lookup {
  GovtDocument = 1,
  SchemeEligibility = 2,
  BlockContentUrlLabel = 3,
  IconClassQuickLinks = 4,
  UpcomingCalendarGroup = 5
}

export enum ActionTypes {
  ADD = 1,
  EDIT = 2,
  DELETE = 3,
  LIST = 4,
  STATUS_CHANGE = 5,
  UPDATE_PROGRESS = 6,
  VIEW_DETAILS = 7

}
export enum ContentTypeLocal {
  RECRUITMENT = 11,
  ADMISSION = 10
}
export enum ContentTypeProd {
  RECRUITMENT = 11,
  ADMISSION = 10
}
export enum fileType {
  IMAGE = 'image/*',
  APPLICATIONPDF = 'application/pdf',
  VIDEO = 'video/*',
  ALL = '*'
}
export const AttachmentTypeDdl: DdlItem[] = [
  {
    text: 'Image',
    value: 1,
  },
  {
    text: 'Video',
    value: 2,
  },
  {
    text: 'PDF',
    value: 3,
  },
  // {
  //   text: 'All',
  //   value: 4,
  // }
];

export const actionsDdl: DdlItem[] = [
  {
    text: 'Add',
    value: ActionTypes.ADD,
  },
  {
    text: 'Edit',
    value: ActionTypes.EDIT,
  },
  {
    text: 'Delete',
    value: ActionTypes.DELETE,
  },
  {
    text: 'List',
    value: ActionTypes.LIST,
  },
  {
    text: 'Status Change',
    value: ActionTypes.STATUS_CHANGE,
  },
  {
    text: 'Update Progress',
    value: ActionTypes.UPDATE_PROGRESS,
  },
  {
    text: 'View Details',
    value: ActionTypes.VIEW_DETAILS,
  },
];

export const ExamModeDdl = [
  {
    text: 'Online',
    value: 1,
  },
  {
    text: 'Offline',
    value: 2,
  },
  {
    text: 'Online/Offline',
    value: 3,
  },

]

export const LevelType = [
  {
    text: 'Center',
    value: 1,
  },
  {
    text: 'State',
    value: 2,
  }

]
export const AdditionalPagesTypeDdl = [
  {
    text: 'Terms and Conditions',
    value: 1,
  },
  {
    text: 'Privacy Policy',
    value: 2,
  },
  {
    text: 'About us',
    value: 3,
  },
  {
    text: 'Disclaimer',
    value: 4,
  },
  {
    text: 'Manage Account',
    value: 5,
  }

]

export const progressDdl: DdlItem[] = [
  {
    text: 'Unapproved',
    value: 1
  },
  {
    text: 'Approved',
    value: 2
  },
  {
    text: 'Published',
    value: 3
  }
]



