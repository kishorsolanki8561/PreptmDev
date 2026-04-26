
export enum ExamModeEnum {
  "Online" = 1,
  "Offline" = 2,
  "OnlineOffline" = 3
}

export const PreptmLogo = 'https://www.preptm.com/assets/img/logo%20with%20icon.svg'

export const PAGE_SIZE = 10;
export const ENABLE_LOGIN = false;

export const DATE_FORMAT = 'dd MMMM, yyyy'

export enum MixedModuleListTypeEnum {
  popular = "popular",
  latest = 'latest',

  subcategory = 'subcategory',
  subCatWiseMixedList = "subCatWiseMixedList",
  blockType = "blockType",

  recruitment = "Jobs",
  scheme = "scheme"
}

export class PostTypesSlug {
  public static RECRUITMENT = "jobs";
  public static PRIVATE_RECRUITMENT = "private-jobs";
  public static ADMITCARD = "admit-cards";
  public static RESULT = "results";
  public static EXAM = "exams";
  public static ADMISSION = "admissions";
  public static POPULAR = "popular";
  public static LATEST = "latest";
  public static Answerkey = "answer-key";
  public static Onlineform = "online-form";
  
  public static SCHEME = "schemes";
  public static CATEGORY = "category";
  public static SEARCH = "search";
  public static BOOKMARK = "bookmarks";
  public static UpCominingSoon = "upcoming";
  public static ExpireSoon = "expiredsoon";
  
  public static PAPER = "papers";
  public static NOTES = "notes";
  public static SYLLABUS = "syllabus";

}

export enum ModuleEnum {
  Recruitment = 1,
  BlockContent = 2,
  Scheme = 3
}
export enum LEVEL {
  Central = 1,
  State = 2,
}
export enum ATTACHMENT_TYPE {
  IMAGE = 1,
  VIDEO = 2,
  PDF = 3,
}
export enum AdditionalPages {
  TermsConditions = 1,
  PrivacyPolicy = 2,
  AboutUs = 3,
  Disclaimer = 4,
  ManageAccount = 5,
}

export const FeedbackTypeDdl = [
  {
    text: 'Query',
    value: 1
  },
  {
    text: 'Suggestion',
    value: 2
  },
  {
    text: 'Request',
    value: 3
  },
  {
    text: 'Feedback',
    value: 4
  },
]

export const LanguageDdl = [
  {
    text: 'English',
    value: 'en'
  },
  {
    text: 'Hindi',
    value: 'hi'
  }
]

export enum DdlLookup {
  GovtDocument = 1,
  SchemeEligibility = 2,
}

export enum DdlLookupSlug {
  Article = 'article-type',
}
