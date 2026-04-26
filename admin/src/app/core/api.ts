import { environment } from 'src/environments/environment';

export class EndPoints {
  static loginUrl = environment.apiEndPoint + 'master/Account/Login';
  //#region <user>
  static user = {
    getAdminUserPaginationUrl: environment.apiEndPoint + 'master/User/GetPagination',
    getAdminUserByIdUrl: environment.apiEndPoint + 'master/User/GetById',
    addUpdateAdminUserUrl: environment.apiEndPoint + 'master/User/AddUpdate',
    deleteAdminUserUrl: environment.apiEndPoint + 'master/User/Delete',
    changeStatusAdminUserUrl: environment.apiEndPoint + 'master/User/UpdateStatus',
    PagePermissionListByUserTypeCodeUrl: environment.apiEndPoint + 'master/PageMaster/PagePermissionListByUserTypeCode',
    PageMasterPermissionModifiedByIdUrl: environment.apiEndPoint + 'master/PageMaster/PageMasterPermissionModifiedById?UserTypeCode=',

  };
  //#endregion

  //#region <FileUpload>
  static file = {
    uploadFile: environment.fileUploadEndPoint + '/api/FileUploader/Upload',
    deleteFile: environment.fileUploadEndPoint + '/api/FileUploader/DeleteFile',
  }
  //#endregion

  //#region <master>
  static menuMaster = {
    getMenuMasterListUrl:
      environment.apiEndPoint + 'master/MenuMaster/GetPagination',
    getMenuMasterByIdUrl: environment.apiEndPoint + 'master/MenuMaster/GetById',
    addUpdateMenuMaster:
      environment.apiEndPoint + 'master/MenuMaster/AddUpDate',
    deleteMenuMasterUrl: environment.apiEndPoint + 'master/MenuMaster/Delete',
    changeStatusMenuMasterUrl:
      environment.apiEndPoint + 'master/MenuMaster/UpdateStatus',
    getDynamicMenuUrl:
      environment.apiEndPoint + 'master/MenuMaster/GetDynamicMenuList',
  };

  static pageMaster = {
    getPageMasterListUrl: environment.apiEndPoint + 'master/PageMaster/GetPagination',
    getPageMasterByIdUrl: environment.apiEndPoint + 'master/PageMaster/GetById',
    addUpdatePageMaster: environment.apiEndPoint + 'master/PageMaster/AddUpDate',
    deletePageMasterUrl: environment.apiEndPoint + 'master/PageMaster/Delete',
    changeStatusPageMasterUrl: environment.apiEndPoint + 'master/PageMaster/UpdateStatus',
  };

  static departmentMaster = {
    getDepartmentMasterListUrl: environment.apiEndPoint + 'master/Department/GetPagination',
    getDepartmentMasterByIdUrl: environment.apiEndPoint + 'master/Department/GetById',
    addUpdateDepartmentMaster: environment.apiEndPoint + 'master/Department/AddUpDate',
    deleteDepartmentMasterUrl: environment.apiEndPoint + 'master/Department/Delete',
    changeStatusDepartmentMasterUrl: environment.apiEndPoint + 'master/Department/UpdateStatus',
  };

  static JobDesignation = {
    getJobDesignationListUrl: environment.apiEndPoint + 'master/JobDesignationMaster/GetPagination',
    getJobDesignationByIdUrl: environment.apiEndPoint + 'master/JobDesignationMaster/GetById',
    addUpdateJobDesignationMaster: environment.apiEndPoint + 'master/JobDesignationMaster/AddUpDate',
    deleteJobDesignationUrl: environment.apiEndPoint + 'master/JobDesignationMaster/Delete',
    changeStatusJobDesignationUrl: environment.apiEndPoint + 'master/JobDesignationMaster/UpdateStatus',
  };

  static QualificationMaster = {
    getJQualificationMasterListUrl: environment.apiEndPoint + 'master/QualificationMaster/GetPagination',
    getQualificationMasterByIdUrl: environment.apiEndPoint + 'master/QualificationMaster/GetById',
    addUpdateQualificationMaster: environment.apiEndPoint + 'master/QualificationMaster/AddUpDate',
    deleteQualificationMasterUrl: environment.apiEndPoint + 'master/QualificationMaster/Delete',
    changeQualificationMasterUrl: environment.apiEndPoint + 'master/QualificationMaster/UpdateStatus',
  };

  static CategoryMaster = {
    getCategoryMasterListUrl: environment.apiEndPoint + 'master/CategoryMaster/GetList',
    getCategoryMasterByIdUrl: environment.apiEndPoint + 'master/CategoryMaster/GetById',
    addUpdateCategoryMaster: environment.apiEndPoint + 'master/CategoryMaster/AddUpdate',
    deleteCategoryMasterUrl: environment.apiEndPoint + 'master/CategoryMaster/Delete',
    changeCategoryMasterUrl: environment.apiEndPoint + 'master/CategoryMaster/UpdateStatus',
  };

  static SubCategoryMaster = {
    getSubCategoryMasterListUrl: environment.apiEndPoint + 'master/SubCategory/GetList',
    getSubCategoryMasterByIdUrl: environment.apiEndPoint + 'master/SubCategory/GetById',
    addUpdateSubCategoryMaster: environment.apiEndPoint + 'master/SubCategory/AddUpdate',
    deleteSubCategoryMasterUrl: environment.apiEndPoint + 'master/SubCategory/Delete',
    changeSubCategoryMasterUrl: environment.apiEndPoint + 'master/SubCategory/UpdateStatus',
    getSubCategoryByCategoryIdurl: environment.apiEndPoint + 'dropdown/AllDropDown/GetSubCategory'
  };

  static BlockTypeMaster = {
    getBlockTypeMasterListUrl: environment.apiEndPoint + 'master/BlockType/GetList',
    getBlockTypeMasterByIdUrl: environment.apiEndPoint + 'master/BlockType/GetById',
    addUpdateBlockTypeMaster: environment.apiEndPoint + 'master/BlockType/AddUpdate',
    deleteBlockTypeMasterUrl: environment.apiEndPoint + 'master/BlockType/Delete',
    changeBlockTypeMasterUrl: environment.apiEndPoint + 'master/BlockType/UpdateStatus',
  };

  static UserTypeMaster = {
    getUserTypeMasterListUrl: environment.apiEndPoint + 'master/UserTypeMaster/GetList',
    getUserTypeMasterByIdUrl: environment.apiEndPoint + 'master/UserTypeMaster/GetById',
    addUpdateUserTypeMasterUrl: environment.apiEndPoint + 'master/UserTypeMaster/AddUpdate',
    deleteUserTypeMasterUrl: environment.apiEndPoint + 'master/UserTypeMaster/Delete',
    changeUserTypeMasterUrl: environment.apiEndPoint + 'master/UserTypeMaster/UpdateStatus',
  };


  static GroupMaster = {
    getGroupMasterListUrl: environment.apiEndPoint + 'master/GroupMaster/GetList',
    getGroupMasterByIdUrl: environment.apiEndPoint + 'master/GroupMaster/GetById',
    addUpdateGroupMaster: environment.apiEndPoint + 'master/GroupMaster/AddUpdate',
    deleteGroupMasterUrl: environment.apiEndPoint + 'master/GroupMaster/Delete',
    changeGroupMasterUrl: environment.apiEndPoint + 'master/GroupMaster/UpdateStatus',
  };

  static AssetsMaster = {
    getAssetsMasterListUrl: environment.apiEndPoint + 'master/AssetsMaster/GetList',
    getAssetsMasterByIdUrl: environment.apiEndPoint + 'master/AssetsMaster/GetById',
    addAssetsCategoryMaster: environment.apiEndPoint + 'master/AssetsMaster/AddUpdate',
    deleteAssetsMasterUrl: environment.apiEndPoint + 'master/AssetsMaster/Delete',
    changeAssetsMasterUrl: environment.apiEndPoint + 'master/AssetsMaster/UpdateStatus',
  };
  //#endregion

  //#region <Transaction>
  static recruitment = {
    getRecruitmentListUrl: environment.apiEndPoint + 'translation/Recruitment/GetPagination',
    getRecruitmentByIdUrl: environment.apiEndPoint + 'translation/Recruitment/GetById',
    addUpdateRecruitment: environment.apiEndPoint + 'translation/Recruitment/AddUpDate',
    deleteRecruitmentUrl: environment.apiEndPoint + 'translation/Recruitment/Delete',
    checkRecruitmentTitleUrl: environment.apiEndPoint + 'translation/Recruitment/CheckRecruitmentTitle',
    changeStatusRecruitmentUrl: environment.apiEndPoint + 'translation/Recruitment/UpdateStatus',
    updateProgressRecruitmentUrl: environment.apiEndPoint + 'translation/Recruitment/RecruitmentProgressStatus',
  };
  //#endregion


  //#region <BlockContents>
  static BlockContents = {
    getBlockContentsListUrl: environment.apiEndPoint + 'translation/BlockContents/GetList',
    getBlockContentsByIdUrl: environment.apiEndPoint + 'translation/BlockContents/GetById',
    addUpdateBlockContents: environment.apiEndPoint + 'translation/BlockContents/AddUpDate',
    deleteBlockContentsUrl: environment.apiEndPoint + 'translation/BlockContents/Delete',
    checkTitleUrl: environment.apiEndPoint + 'translation/BlockContents/CheckBlockContentTitle',
    changeStatusBlockContentsUrl: environment.apiEndPoint + 'translation/BlockContents/UpdateStatus',
    updateProgressBlockContentsUrl: environment.apiEndPoint + 'translation/BlockContents/GetList',
    BlockContentProgressStatusUrl: environment.apiEndPoint + 'translation/BlockContents/BlockContentProgressStatus',
  };
  //#endregion


  //#region <ddl>
  static ddl = {
    getAllDdl: environment.apiEndPoint + 'dropdown/AllDropDown/AllDropDown',
    GetDDLLookupDataByLookupTypeIdAndLookupTypeUrl: environment.apiEndPoint + 'dropdown/AllDropDown/GetDDLLookupDataByLookupTypeIdAndLookupType',
  };

  //#endregion
  static FrontUserReport = {
    list: 'ddlState',
    FrontUserReportUrl: environment.apiEndPoint + 'master/user/GetFrontUserReport',
    GetFrontUserFeedbackReportUrl: environment.apiEndPoint + 'master/user/GetFrontUserFeedbackReport',

  }

  //#region <AdditionalPages>
  static additionalpages = {
    getadditionalpagesListUrl: environment.apiEndPoint + 'master/AdditionalPages/GetList',
    getadditionalpagesByIdUrl: environment.apiEndPoint + 'master/AdditionalPages/GetById',
    addUpdateadditionalpages: environment.apiEndPoint + 'master/AdditionalPages/AddUpDate',
  };
  //#endregion

  //#region <LookupTypeMaster>
  static LookupTypeMaster = {
    getLookupTypeMasterListUrl: environment.apiEndPoint + 'master/LookupType/GetList',
    getLookupTypeMasterByIdUrl: environment.apiEndPoint + 'master/LookupType/GetById',
    addUpdateLookupTypeMaster: environment.apiEndPoint + 'master/LookupType/AddUpdate',
    deleteLookupTypeMasterUrl: environment.apiEndPoint + 'master/LookupType/Delete',
    changeLookupTypeMasterUrl: environment.apiEndPoint + 'master/LookupType/UpdateStatus',
  };
  //#endregion
  //#region <LookupMaster>
  static LookupMaster = {
    getlookupMasterListUrl: environment.apiEndPoint + 'master/lookup/GetList',
    getlookupMasterByIdUrl: environment.apiEndPoint + 'master/lookup/GetById',
    addUpdatelookupMaster: environment.apiEndPoint + 'master/lookup/AddUpdate',
    deletelookupMasterUrl: environment.apiEndPoint + 'master/lookup/Delete',
    changelookupMasterUrl: environment.apiEndPoint + 'master/lookup/UpdateStatus',
  };
  //#region <BannerMaster>
  static BannerMaster = {
    getBannerMasterListUrl: environment.apiEndPoint + 'master/Banner/GetList',
    getBannerMasterByIdUrl: environment.apiEndPoint + 'master/Banner/GetById',
    addUpdateBannerMaster: environment.apiEndPoint + 'master/Banner/AddUpdate',
    deleteBannerMasterUrl: environment.apiEndPoint + 'master/Banner/Delete',
    changeBannerMasterUrl: environment.apiEndPoint + 'master/Banner/UpdateStatus',
  };
  //#endregion


  //#region <Scheme>
  static Scheme = {
    getSchemeListUrl: environment.apiEndPoint + 'translation/Scheme/GetList',
    getSchemeByIdUrl: environment.apiEndPoint + 'translation/Scheme/GetById',
    addUpdateScheme: environment.apiEndPoint + 'translation/Scheme/AddUpdate',
    deleteSchemetUrl: environment.apiEndPoint + 'translation/Scheme/Delete',
    changeStatusSchemeUrl: environment.apiEndPoint + 'translation/Scheme/UpdateStatus',
    updateProgressSchemeUrl: environment.apiEndPoint + 'translation/Scheme/SchemeProgressStatus',
    CheckSchemeTitleURL: environment.apiEndPoint + 'translation/Scheme/CheckSchemeTitle',
  };
  //#endregion

  static Article = {
    getArticleListUrl: environment.apiEndPoint + 'translation/article/GetList',
    getArticleByIdUrl: environment.apiEndPoint + 'translation/article/GetById',
    addUpdateArticle: environment.apiEndPoint + 'translation/article/AddUpdate',
    deleteArticleUrl: environment.apiEndPoint + 'translation/article/Delete',
    changeStatusArticleUrl: environment.apiEndPoint + 'translation/article/UpdateStatus',
    updateProgressArticleUrl: environment.apiEndPoint + 'translation/article/ProgressStatus',
    CheckArticleTitleURL: environment.apiEndPoint + 'translation/article/CheckArticleTitle',
  }

  static Paper = {
    getListUrl: environment.apiEndPoint + 'translation/paper/GetList',
    getByIdUrl: environment.apiEndPoint + 'translation/paper/GetById',
    addUpdate: environment.apiEndPoint + 'translation/paper/AddUpdate',
    deleteUrl: environment.apiEndPoint + 'translation/paper/Delete',
    changeStatusUrl: environment.apiEndPoint + 'translation/paper/UpdateStatus',
    updateProgressUrl: environment.apiEndPoint + 'translation/paper/ProgressStatus',
    CheckTitleURL: environment.apiEndPoint + 'translation/paper/CheckArticleTitle',
  }
  static Notes = {
    getListUrl: environment.apiEndPoint + 'translation/note/GetList',
    getByIdUrl: environment.apiEndPoint + 'translation/note/GetById',
    addUpdate: environment.apiEndPoint + 'translation/note/AddUpdate',
    deleteUrl: environment.apiEndPoint + 'translation/note/Delete',
    changeStatusUrl: environment.apiEndPoint + 'translation/note/UpdateStatus',
    updateProgressUrl: environment.apiEndPoint + 'translation/note/ProgressStatus',
    CheckTitleURL: environment.apiEndPoint + 'translation/note/CheckArticleTitle',
  }
  static Syllabus = {
    getListUrl: environment.apiEndPoint + 'translation/syllabus/GetList',
    getByIdUrl: environment.apiEndPoint + 'translation/syllabus/GetById',
    addUpdate: environment.apiEndPoint + 'translation/syllabus/AddUpdate',
    deleteUrl: environment.apiEndPoint + 'translation/syllabus/Delete',
    changeStatusUrl: environment.apiEndPoint + 'translation/syllabus/UpdateStatus',
    updateProgressUrl: environment.apiEndPoint + 'translation/syllabus/ProgressStatus',
    CheckTitleURL: environment.apiEndPoint + 'translation/syllabus/CheckArticleTitle',
  }
}

export class DdlsList {
  static masters = {
    adminUserAddUpdateDdls: 'ddlUserType',
    menuMasterAddUpdateDdls: 'ddlUserType,ddlMenu',
    pageMasterAddUpdateDdls: 'ddlMenu',
    departmentMasterAddUpdateDdls: 'ddlState',
    assetsAddUpdate: 'ddlBlockType',
    SubCategory: 'ddlCategory',
    lookupType: 'ddlLookupType'
  };
  static recruitment = {
    list: 'ddlDepartment,ddlPublisher,ddlSubCategory',
    addUpdate: 'ddlGroup,ddlBlockTypeByForRecruitment,ddlDepartment,ddlJobDesignation,ddlQualification,ddlCategory,ddlSubCategory,ddlState'
  }
  static blockContents = {
    list: 'ddlBlockType,ddlDepartment,ddlRecruitment,ddlCategory,ddlGroup,ddlSubCategory',
    addUpdate: 'ddlBlockType,ddlDepartment,ddlRecruitment,ddlCategory,ddlGroup,ddlSubCategory,ddlState'
  }
  static FrontUserReport = {
    list: 'ddlState',
  }
  static scheme = {
    list: 'ddlDepartment,ddlPublisher',
    addUpdate: 'ddlDepartment,ddlState'
  }
  static article = {
    list: 'ddlPublisher',
    addUpdate: 'ddlBlockType,ddlGroup'
  }
  static paper = {
    addUpdate: 'ddlDepartment,ddlState,ddlCategory,ddlGroup,ddlQualification'
  }
  static notes = {
    addUpdate: 'ddlDepartment,ddlState,ddlCategory,ddlGroup,ddlQualification'
  }
  static syllabus = {
    addUpdate: 'ddlDepartment,ddlState,ddlCategory,ddlGroup,ddlQualification'
  }
}
