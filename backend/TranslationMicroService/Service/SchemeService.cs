using CommonService.JWT;
using CommonService.Other;
using Dapper;
using Microsoft.AspNetCore.Http;
using ModelService.CommonModel;
using ModelService.Model.Front;
using ModelService.Model.MastersModel;
using ModelService.Model.Translation;
using ModelService.OtherModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TranslationMicroService.IService;

namespace TranslationMicroService.Service
{
    public class SchemeService : UtilityManager, ISchemeService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly FileUploader _fileUploader;
        private readonly FairBaseHelper _fairBaseHelper;

        public SchemeService(HelperService helperService, JWTAuthManager jWTAuthManager, FileUploader fileUploader, FairBaseHelper fairBaseHelper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _fileUploader = fileUploader;
            _fairBaseHelper = fairBaseHelper;
        }
        public ServiceResponse<int> AddUpdate(SchemeRequestModel model)
        {
            Random rnd = new Random();
            SchemeViewModel viewModel = new SchemeViewModel();
            SqlTransaction lSqlTrans = GetNewTransaction();
            XDocument attachment = new XDocument(new XElement("SchemeAttachmentLookup"));
            XDocument contactDetail = new XDocument(new XElement("ContactDedtailLookup"));
            XDocument howToApplyAndQuickLinkLookup = new XDocument(new XElement("HowToApplyAndQuickLinkLookup"));
            XDocument FAQLookup = new XDocument(new XElement("FAQLookup"));
            DynamicParameters parameters = new DynamicParameters();
            List<string> attachmentPath = new List<string>();
            var datetime = DateTime.UtcNow;
            try
            {
                viewModel = model.Id > 0 ? GetById((int)model.Id).Data : new SchemeViewModel();
                model.UserId = _jWTAuthManager.User.Id;
                //Delete Lookups
                if (viewModel is not null && viewModel.Id > 0)
                {
                    //Delele Attachment
                    model.DBDeleteSchemeAttachmentLookupIds = string.Join(",", viewModel.SchemeAttachmentLookups
                               .Where(z => !model.SchemeAttachmentLookups.Where(a => a.Id.HasValue == true).Select(a => a.Id).Contains(z.Id)).Select(a => a.Id).ToList());

                    //delete ContactDetail
                    model.DBDeleteSchemeContactDetailsLookupIds = string.Join(string.Empty, viewModel.ContactDetail
                                  .Where(z => !model.ContactDetail.Where(a => a.Id.HasValue == true).Select(a => a.Id).Contains(z.Id)).Select(a => a.Id.ToString()).ToList());

                    //delete HowToApplyAndQuickLinkLookup
                    model.DBDeleteHowToApplyAndQuickLinkLookupIds = string.Join(string.Empty, viewModel.HowToApplyAndQuickLinkLookup
                                  .Where(z => !model.HowToApplyAndQuickLinkLookup.Where(a => a.Id.HasValue == true).Select(a => a.Id).Contains(z.Id)).Select(a => a.Id.ToString()).ToList());

                    //Delete Document
                    List<int> docIds = !string.IsNullOrEmpty(model.DocumentIds) ? model.DocumentIds.Split(',').Select(int.Parse).ToList() : new List<int>();
                    model.DBDeleteDocumentIds = string.Join(',', viewModel.SchemeDocuments.Where(z => !docIds.Contains(z.LookupId)).Select(a => a.Id.ToString()).ToList());

                    //Delete EligibilityId
                    List<int> eligIds = !string.IsNullOrEmpty(model.EligibilityIds) ? model.EligibilityIds.Split(',').Select(int.Parse).ToList() : new List<int>();
                    model.DBDeleteEligibilityIds = string.Join(',', viewModel.SchemeEligibilitys.Where(z => !eligIds.Contains(z.EligibilityId)).Select(a => a.Id.ToString()).ToList());


                }

                // Added DocumentIds
                if (!string.IsNullOrEmpty(model.DocumentIds))
                {
                    int[] docIds = model.DocumentIds.Split(',').Select(int.Parse).ToArray();
                    model.DocumentIds = string.Join(',', docIds.Where(z => !viewModel.SchemeDocuments.Select(a => a.LookupId).Contains(z)).ToList());
                }

                // Added EligibilityIds
                if (!string.IsNullOrEmpty(model.EligibilityIds))
                {
                    int[] eligIds = model.EligibilityIds.Split(',').Select(int.Parse).ToArray();
                    model.EligibilityIds = string.Join(',', eligIds.Where(z => !viewModel.SchemeEligibilitys.Select(a => a.EligibilityId).Contains(z)).ToList());
                }
                // Added Attachment
                if (model.SchemeAttachmentLookups is not null && model.SchemeAttachmentLookups.Count > 0)
                {
                    foreach (var item in model.SchemeAttachmentLookups)
                    {
                        //FileUploadModel file = new FileUploadModel();
                        //if (item.AttchPath != null)
                        //{
                        //    file.file = item.AttchPath;
                        //    file.filename = rnd.Next(0, 100000) + "_" + datetime.Second.ToString() + System.IO.Path.GetExtension(file.file.FileName); //file.file.
                        //    file.path = "scheme/attachment";
                        //    item.Path = _fileUploader.PostFile(file);
                        //}
                        //else
                        //{
                        //    item.Path = viewModel.SchemeAttachmentLookups.Where(s => s.Id == item.Id).FirstOrDefault().Path;
                        //}
                        XElement attach = new XElement("AttachmentLookup",
                            new XElement("Id", item.Id),
                            new XElement("Title", item.Title),
                            new XElement("TitleHindi", item.TitleHindi),
                            new XElement("Path", item.Path),
                            new XElement("Type", item.Type),
                            new XElement("Description", item.Description),
                            new XElement("DescriptionHindi", item.DescriptionHindi),
                            new XElement("IsUpdate", item.IsUpdate)
                            );
                        attachmentPath.Add(item.Path);
                        attachment.Root.Add(attach);
                    }
                    model.DBSchemeAttachmentLookups = attachment;
                }

                // Added ContactDetail
                if (model is not null && model.ContactDetail.Count > 0)
                {
                    foreach (var item in model.ContactDetail)
                    {
                        XElement contact = new XElement("ContactDedtail",
                            new XElement("Id", item.Id),
                            new XElement("DepartmentId", item.DepartmentId),
                            new XElement("SchemeId", item.SchemeId),
                            new XElement("NodalOfficerName", item.NodalOfficerName),
                            new XElement("NodalOfficerNameHindi", item.NodalOfficerNameHindi),
                            new XElement("PhoneNo", item.PhoneNo),
                            new XElement("Email", item.Email),
                            new XElement("IsUpdate", item.IsUpdate)

                            );
                        contactDetail.Root.Add(contact);
                    }
                    model.DBSchemeContactDetailsLookups = contactDetail;
                }

                // Added HowToApplyAndQuickLinkLookup
                if (model is not null && model.HowToApplyAndQuickLinkLookup.Count > 0)
                {
                    foreach (var item in model.HowToApplyAndQuickLinkLookup)
                    {
                        XElement HowToApplyAndQuick = new XElement("HowToApplyAndQuickLink",
                            new XElement("Id", item.Id),
                            new XElement("Title", item.Title),
                            new XElement("TitleHindi", item.TitleHindi),
                            new XElement("LinkUrl", item.LinkUrl),
                            new XElement("IsQuickLink", item.IsQuickLink),
                            new XElement("Description", item.Description),
                            new XElement("DescriptionHindi", item.DescriptionHindi),
                            new XElement("DescriptionJson", item.DescriptionJson),
                            new XElement("DescriptionHindiJson", item.DescriptionHindiJson),
                            new XElement("IconClass", item.IconClass),
                            new XElement("IsUpdate", item.IsUpdate)
                            );
                        howToApplyAndQuickLinkLookup.Root.Add(HowToApplyAndQuick);
                    }
                    model.DBHowToApplyAndQuickLinkLookups = howToApplyAndQuickLinkLookup;
                }

                // Added FAQ
                if (model is not null && model.FAQLookups.Count > 0)
                {
                    foreach (var item in model.FAQLookups)
                    {
                        XElement FAQLookupObj = new XElement("FAQ",
                            new XElement("Id", item.Id),
                            new XElement("Que", item.Que),
                            new XElement("Ans", item.Ans),
                            new XElement("QueHindi", item.QueHindi),
                            new XElement("AnsHindi", item.AnsHindi),
                            new XElement("IsUpdate", item.IsUpdate)
                            );

                        FAQLookup.Root.Add(FAQLookupObj);
                    }
                }

                ////Added thumbnail
                //if (model.ThumbnailFile != null)
                //{
                //    FileUploadModel file = new FileUploadModel();
                //    file.file = model.ThumbnailFile;
                //    file.filename = file.file.FileName + "scheme_" + datetime.Second.ToString() + ".webp";//System.IO.Path.GetExtension(file.file.FileName); //file.file.
                //    file.path = "scheme/thumbnail";
                //    model.Thumbnail = _fileUploader.PostFile(file);
                //}
                //Parameters 
                parameters.Add("@Id", model.Id);
                parameters.Add("@Title", model.Title);
                parameters.Add("@TitleHindi", model.TitleHindi);
                parameters.Add("@DepartmentId", model.DepartmentId);
                parameters.Add("@StateId", model.StateId);
                parameters.Add("@MinAge", model.MinAge);
                parameters.Add("@MaxAge", model.MaxAge);
                parameters.Add("@StartDate", model.StartDate);
                parameters.Add("@EndDate", model.EndDate);
                parameters.Add("@ExtendedDate", model.ExtendedDate);
                parameters.Add("@CorrectionLastDate", model.CorrectionLastDate);
                parameters.Add("@PostponeDate", model.PostponeDate);
                parameters.Add("@LevelType", model.LevelType);
                parameters.Add("@Mode", model.Mode);
                parameters.Add("@OfficelLink", model.OfficelLink);
                parameters.Add("@ApplyLink", model.ApplyLink);
                parameters.Add("@ShortDescription", model.ShortDescription);
                parameters.Add("@ShortDescriptionHindi", model.ShortDescriptionHindi);
                parameters.Add("@keywords", model.keywords);
                parameters.Add("@Description", model.Description);
                parameters.Add("@DescriptionHindi", model.DescriptionHindi);
                parameters.Add("@DescriptionJson", model.DescriptionJson);
                parameters.Add("@DescriptionHindiJson", model.DescriptionHindiJson);
                parameters.Add("@Thumbnail", model.Thumbnail);
                parameters.Add("@Fee", model.Fee);
                parameters.Add("@Slug", model.Slug);
                parameters.Add("@UserId", model.UserId);
                parameters.Add("@DocumentIds", model.DocumentIds);
                parameters.Add("@EligibilityIds", model.EligibilityIds);
                parameters.Add("@DBDeleteEligibilityIds", model.DBDeleteEligibilityIds);
                parameters.Add("@DBDeleteDocumentIds", model.DBDeleteDocumentIds);
                parameters.Add("@DBSchemeContactDetailsLookups", contactDetail.ToString(), DbType.Xml, ParameterDirection.Input);
                parameters.Add("@DBHowToApplyAndQuickLinkLookups", howToApplyAndQuickLinkLookup.ToString(), DbType.Xml, ParameterDirection.Input);
                parameters.Add("@DBSchemeAttachmentLookups", attachment.ToString(), DbType.Xml, ParameterDirection.Input);
                parameters.Add("@DBDeleteSchemeContactDetailsLookupIds", model.DBDeleteSchemeContactDetailsLookupIds);
                parameters.Add("@DBDeleteHowToApplyAndQuickLinkLookupIds", model.DBDeleteHowToApplyAndQuickLinkLookupIds);
                parameters.Add("@DBDeleteSchemeAttachmentLookupIds", model.DBDeleteSchemeAttachmentLookupIds);
                parameters.Add("@FAQLookup", FAQLookup.ToString(), DbType.Xml, ParameterDirection.Input);
                parameters.Add("@SocialMediaUrl", model.SocialMediaUrl);
                parameters.Add("@ThumbnailCredit", model.ThumbnailCredit);
                parameters.Add("@IsCompleted", model.IsCompleted);
                parameters.Add("@ShouldReminder", model.ShouldReminder);
                parameters.Add("@ReminderDescription", model.ReminderDescription);
                parameters.Add("@UpcomingCalendarCode", model.UpcomingCalendarCode);
                parameters.Add("@keywordsHindi", model.keywordsHindi);
                var result = Execute(QueriesPaths.SchemeQueries.QAddUpdate, parameters, lSqlTrans);

                if (!result.IsSuccess && result.StatusCode == 408)//SlugUrlExist
                {
                    return SetResultStatus<int>(result.Data, MessageStatus.SlugUrlExist, result.IsSuccess, "", "", 0, StatusCodes.Status408RequestTimeout, null);
                }
                else if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        lSqlTrans.Commit();
                        if (model.Id > 0)
                        {
                            //delete thumbnail
                            if (string.IsNullOrEmpty(model.Thumbnail))
                                if (!string.IsNullOrEmpty(viewModel.Thumbnail))
                                    _fileUploader.DeleteFile(viewModel.Thumbnail);

                            if (!string.IsNullOrEmpty(model.DBDeleteSchemeAttachmentLookupIds))
                            {
                                var attchIds = model.DBDeleteSchemeAttachmentLookupIds.Split(',').Select(int.Parse).ToArray();
                                foreach (var deleteAttch in attchIds)
                                {
                                    var filePath = viewModel.SchemeAttachmentLookups.Where(z => z.Id == Convert.ToInt32(deleteAttch)).FirstOrDefault();
                                    if (!string.IsNullOrEmpty(filePath.Path))
                                        _fileUploader.DeleteFile(filePath.Path);
                                }
                            }
                            return SetResultStatus<int>(0, MessageStatus.Update, true, "", "", 0, StatusCodes.Status200OK, null);
                        }
                        return SetResultStatus<int>(0, MessageStatus.Save, true, "", "", 0, StatusCodes.Status200OK, null);
                    }
                }

                lSqlTrans.Rollback();
                return SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);

            }
            catch (Exception ex)
            {
                foreach (var deleteAttch in attachmentPath)
                {
                    if (!string.IsNullOrEmpty(deleteAttch))
                        _fileUploader.DeleteFile(deleteAttch);
                }
                if (string.IsNullOrEmpty(model.Thumbnail) && !string.IsNullOrEmpty(viewModel.Thumbnail))
                    _fileUploader.DeleteFile(model.Thumbnail);

                lSqlTrans.Rollback();
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false, ex.Message, "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }
        public ServiceResponse<SchemeViewModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<SchemeViewModel>(QueriesPaths.SchemeQueries.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    var lookupData = GetSchemeDocumentAndAttachmentLookupDataBySchemeId(Id);
                    result.Data.SchemeDocuments = lookupData.SchemeDocumentLookups;
                    result.Data.DocumentIds = lookupData.SchemeDocumentLookups.Select(c => c.LookupId).ToList();
                    result.Data.SchemeEligibilitys = lookupData.SchemeEligibilityLookups;
                    result.Data.EligibilityIds = lookupData.SchemeEligibilityLookups.Select(c => c.EligibilityId).ToList();
                    result.Data.ContactDetail = lookupData.ContactDetail;
                    result.Data.SchemeAttachmentLookups = lookupData.SchemeAttachmentLookups;
                    result.Data.HowToApplyAndQuickLinkLookup = lookupData.HowToApplyAndQuickLinkLookup;
                    result.Data.FAQLookups = lookupData.FAQLookups;
                    return SetResultStatus<SchemeViewModel>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<SchemeViewModel>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "GetById"));
                return SetResultStatus<SchemeViewModel>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }
        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(QueriesPaths.SchemeQueries.QDelete, new { Id = Id, UserId = _jWTAuthManager.User.Id });
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(deleteResult.IsSuccess, MessageStatus.Delete, deleteResult.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(deleteResult.IsSuccess, MessageStatus.NoRecord, !deleteResult.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                    else
                        return SetResultStatus<bool>(!deleteResult.IsSuccess, MessageStatus.Error, !deleteResult.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
                else
                {
                    return SetResultStatus<bool>(true, MessageStatus.Error, false, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<List<SchemeViewListModel>> GetPagination(SchemeFilterModel filterModel)
        {
            try
            {
                var result = QueryList<SchemeViewListModel>(QueriesPaths.SchemeQueries.QPagination, _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<SchemeViewListModel>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<SchemeViewListModel>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "GetPagination"));
                return SetResultStatus<List<SchemeViewListModel>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }
        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(QueriesPaths.SchemeQueries.QUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id });
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(result.IsSuccess, MessageStatus.StatusUpdate, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                    else if (result.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(result.IsSuccess, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                    else
                        return SetResultStatus<bool>(!result.IsSuccess, MessageStatus.Error, !result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
                else
                {
                    return SetResultStatus<bool>(!result.IsSuccess, MessageStatus.Error, !result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }
        public ServiceResponse<bool> SchemeProgressStatus(int Id, int ProgressStatus)
        {
            try
            {
                if (Id > 0 && ProgressStatus > 0)
                {
                    var viewData = GetById(Id);
                    if (viewData.Data != null)
                    {
                        // Create Deeplink 
                        //string Deeplink = string.Empty;
                        //ResponseDeepLinkModel res = new ResponseDeepLinkModel();
                        //if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                        //{
                        //    DeeplinkModel link = new DeeplinkModel()
                        //    {
                        //        moduleName = viewData.Data.ModuleSlug,
                        //        type = "post",
                        //        slugUrl = viewData.Data.Slug,
                        //        id = (int)viewData.Data.Id,
                        //        thumbnail = viewData.Data.Thumbnail,
                        //        title = viewData.Data.Title
                        //    };
                        //    Deeplink = JsonConvert.SerializeObject(link);
                        //    _fairBaseHelper.PostPushNotification(link, link.title, link.title);
                        //    var getDeeplink = Deeplink.GenerateDeeplink(link.title, viewData.Data.ShortDescription, link.thumbnail);
                        //    var deeplink = _fairBaseHelper.Post(getDeeplink);
                        //    if (!string.IsNullOrEmpty(deeplink))
                        //    {
                        //        res = JsonConvert.DeserializeObject<ResponseDeepLinkModel>(deeplink);
                        //    }
                        //}
                        var result = ExecuteReturnData(QueriesPaths.SchemeQueries.QProgressUpdateStatus, new { Id = Id, UserId = _jWTAuthManager.User.Id, StatusCode = ProgressStatus, SortLinks = string.Empty });
                        if (result.IsSuccess)
                        {
                            if (result.StatusCode == StatusCodes.Status200OK)
                                return SetResultStatus<bool>(result.IsSuccess, MessageStatus.StatusProgressUpdate, result.IsSuccess, "", "", 0, StatusCodes.Status200OK, null);
                            if (result.StatusCode == StatusCodes.Status203NonAuthoritative)
                                return SetResultStatus<bool>(result.IsSuccess, MessageStatus.StatusProgressNotValidCodeUpdate, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                            else
                                return SetResultStatus<bool>(!result.IsSuccess, MessageStatus.Error, !result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                        }
                        else
                        {
                            result.StatusCode = StatusCodes.Status404NotFound;
                            return SetResultStatus<bool>(!result.IsSuccess, MessageStatus.Error, !result.IsSuccess, "", "", 0, StatusCodes.Status500InternalServerError, null);
                        }
                    }
                    else
                    {
                        return SetResultStatus<bool>(true, MessageStatus.NoRecord, true, "", "", 0, StatusCodes.Status404NotFound, null);
                    }

                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.StatusProgressRequiredUpdate, false, "", "", 0, StatusCodes.Status404NotFound, null);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "SchemeProgressStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, "", "", 0, StatusCodes.Status500InternalServerError, null);
            }
        }
        public ServiceResponse<List<SchemeTitleCheckModel>> CheckSchemeTitle(string SearchText)
        {
            ServiceResponse<List<SchemeTitleCheckModel>> serviceResponse = new ServiceResponse<List<SchemeTitleCheckModel>>();
            List<SchemeTitleCheckModel> result = new List<SchemeTitleCheckModel>();
            try
            {
                serviceResponse.Data = QueryList<SchemeTitleCheckModel>(@"Sp_CheckSchemeTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "CheckSchemeTitle"));
                return null;
            }
        }
        #region Private Function
        private GetSchemeDocumentAndAttachmentLookupDataModel GetSchemeDocumentAndAttachmentLookupDataBySchemeId(int Id)
        {
            try
            {
                GetSchemeDocumentAndAttachmentLookupDataModel model = new GetSchemeDocumentAndAttachmentLookupDataModel();
                var result = QueryMultiple(QueriesPaths.SchemeQueries.QGetSchemeDocumentAndAttachmentLookupDataBySchemeId, new { SchemeId = Id });
                model.SchemeDocumentLookups = JsonConvert.DeserializeObject<List<SchemeDocumentLookupModel>>(JsonConvert.SerializeObject(result.Data[0]));// result.Data[0] as List<SchemeDocumentLookupModel>;
                model.SchemeAttachmentLookups = JsonConvert.DeserializeObject<List<SchemeAttachmentLookupModel>>(JsonConvert.SerializeObject(result.Data[1]));// result.Data[1] as List<SchemeAttachmentLookupModel>;
                model.HowToApplyAndQuickLinkLookup = JsonConvert.DeserializeObject<List<SchemeHowToApplyAndQuickLinkLookup>>(JsonConvert.SerializeObject(result.Data[2]));// result.Data[0] as List<SchemeDocumentLookupModel>;
                model.ContactDetail = JsonConvert.DeserializeObject<List<SchemeContactDetailsLookup>>(JsonConvert.SerializeObject(result.Data[3]));// result.Data[0] as List<SchemeDocumentLookupModel>;
                model.SchemeEligibilityLookups = JsonConvert.DeserializeObject<List<SchemeEligibilityLookupModel>>(JsonConvert.SerializeObject(result.Data[4]));// result.Data[0] as List<SchemeDocumentLookupModel>;
                model.FAQLookups = JsonConvert.DeserializeObject<List<SchemeFAQLookup>>(JsonConvert.SerializeObject(result.Data[5]));// result.Data[0] as List<SchemeDocumentLookupModel>;
                return model;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SchemeService.cs", "GetSchemeDocumentAndAttachmentLookupDataBySchemeId"));
                throw ex;
            }

        }
        #endregion
    }
}
