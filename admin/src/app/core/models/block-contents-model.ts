import { FilesWithPrev } from './FormElementsModel';
import { EditorVal, indexModel } from './core.model';
export class BlockContentsModel {
  docs: FilesWithPrev;
  documents: any[]
  id: number;
  title: string;
  blockTypeId: number;
  recruitmentId: number | null;
  departmentId: number;
  categoryId: number;
  slugUrl: string;
  url: string | null;
  description: EditorVal;
  howTo: string | null;
  notificationLink?: any;
  descriptionJson?: string;
  descriptionHindiJson?: string;
  descriptionHindi?: EditorVal;
  notificationFile?: FilesWithPrev;
  howToApplyAndQuickLinkLookup: any[] = [];
  lastDate: Date | string;
  extendedDate: Date | string;
  feePaymentLastDate: Date | string;
  correctionLastDate: Date | string;
  urlLabelId: number;
  examMode: number;
  faqLookup: any[] = [];
  shouldReminder: Date;
  reminderDescription: string = ''
  isCompleted: boolean = false;
  publishedDate: any;
  keywords: string;
  keywordsHindi: string;
  summary: string;
  summaryHindi: string;
  blockContentTags:number[];
}

export class BlockContentsViewModel {
  id: number;
  title: string;
  blockTypeName: string;
  recruitmentTitle: string;
  departmentName: string;
  categoryName: string;
  modifiedByName: string;
  slugUrl: string;
  copySlugUrl: string;
  modifiedDate: string;
  visitCount: number;
  isActive: boolean;
}

export class BlockContentsFilterModel extends indexModel {
  title: string = '';
  slugUrl: string = '';
  blockTypeId: number = 0;
  recruitmentId: number = 0;
  departmentId: number = 0;
  categoryId: number = 0;
  fromDate: string = '';
  toDate: string = '';
  status: number = -1;
  groupId: number = 0;
}


export class HowToApplyAndQuickLinkLookup {
  id: number = 0;
  title: string | null;
  titleHindi: string | null;
  description = new EditorVal();;
  descriptionHindi = new EditorVal();;
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

export class CheckBlockTitleModel {
  isActive: boolean;
  slugUrl: string;
  status: string;
  title: string;
}
