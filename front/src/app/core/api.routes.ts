export const API_ROUTES = {
  cockpitPanelsPosts: 'front/Dashboard/GetDashboardData',
  ddl: 'dropdown/AllDropDown/AllDropDown?keys=',
  getDDLLookupDataBy: "dropdown/alldropdown/getddllookupDataByLookupTypeIdAndLookupType",
  popularSearch: 'front/Dashboard/GetPopularBySearchText',
  getBanners: 'front/Dashboard/GetBanners',
  list: 'front/Dashboard/GetFrontDashboardList',
  postDetails: 'front/BlockContent/GetBlockContentDetailsOfIdAndSlug',
  departmentDetails: 'front/dashboard/GetDepartmentDataByIdAndSlug',
  additionalPages: {
    getPage: 'front/AdditionalPage/GetAdditionalPagesByPageType',
    sendUserMessageUrl: 'front/User/AddUserFeedback'

  },
  auth: {
    login: 'front/User/Login'
  },
  post: {
    bookmark: 'front/User/AddRemoveBookmark',
    search: 'front/Dashboard/GetDashboardSearchFilter',
    bookmarkList: 'front/User/GetBookmarkPostList',
    recruitmentDetails: 'front/Recruitment/GetRecruitmentDetailsOfIdAndSlug',
    admissionDetails: 'front/Admission/GetAdmissionDetailsOfIdAndSlug',
    schemeDetails: 'front/scheme/getSchemedatabyidandslug',
    blockContaintDetails: 'front/BlockContent/GetBlockContentDetailsOfIdAndSlug',
    filterDdl: "ddlDepartment,ddlQualification,ddlJobDesignation,ddlCategory",
    tagDdl: "ddlGroup",
    articleType: "GetDDLLookupDataByLookupTypeIdAndLookupType?SlugUrl=article-type"
  },
  article : {
    list : 'front/Article/GetArticles',
    articleDetail : 'front/Article/GetArticleDetails'
  }
}
