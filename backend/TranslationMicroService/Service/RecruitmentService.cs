using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using Dapper;
using DBInfrastructure;
using Microsoft.EntityFrameworkCore;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using ModelService.Model;
using ModelService.Model.Front;
using ModelService.Model.Translation;
using ModelService.Model.Translation.Recruitment;
using ModelService.OtherModels;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using TranslationMicroService.IService;
using static Dapper.SqlMapper;

namespace TranslationMicroService.Service
{
    public class RecruitmentService : UtilityManager, IRecruitmentService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly FileUploader _fileUploader;
        private readonly FairBaseHelper _fairBaseHelper;
        private readonly IDBPreptmContext _dBContext;
        private readonly IMapper _mapper;


        public RecruitmentService(HelperService helperService, JWTAuthManager jWTAuthManager, FileUploader fileUploader, FairBaseHelper fairBaseHelper, IDBPreptmContext dBContext, IMapper mapper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _fileUploader = fileUploader;
            _fairBaseHelper = fairBaseHelper;
            _dBContext = dBContext;
            _mapper = mapper;
        }
        //public ServiceResponse<int> AddUpdate(RecruitmentModel model)
        //{
        //    SqlTransaction lSqlTrans = GetNewTransaction();
        //    List<string> attachmentList = new List<string>();
        //    try
        //    {
        //        XElement xmlData = model.ConvertJsonToXml<RecruitmentModel>();

        //        Random rnd = new Random();
        //        RecruitmentModel recruitmentData = new RecruitmentModel();
        //        XDocument attachment = new XDocument(new XElement("DocumentLookup"));
        //        XDocument howToApplyAndQuickLinkLookup = new XDocument(new XElement("HowToApplyAndQuickLinkLookup"));
        //        XDocument FAQLookup = new XDocument(new XElement("FAQLookup"));
        //        DynamicParameters parameters = new DynamicParameters();
        //        ServiceResponse<int> response = new ServiceResponse<int>();
        //        string deleteQualificationLookupids = "";
        //        string deleteJobdesignationLookupids = "";

        //        var datetime = DateTime.UtcNow;
        //        model.UserId = _jWTAuthManager.User.Id;
        //        if (model.Attachments is not null && model.Attachments.Count() > 0)
        //            model.InternalAttachments = model.Attachments.Select(s => new RecruitmentDocumentModel() { Path = s }).ToList();
        //        if (model.Id > 0)
        //        {
        //            recruitmentData = GetById((int)model.Id).Data;
        //            //delete Attachments 
        //            model.DeleteDocumentLookupIds = string.Join(",", recruitmentData.InternalAttachments.Select(x => x.Id).ToList().Where(z => !model.InternalAttachments.Where(z => z.Id != 0).Select(x => x.Id).Contains(z)).ToList());

        //            //delete Thumbnail
        //            if (model.Id > 0 && string.IsNullOrEmpty(model.thumbnail))
        //            {
        //                if (recruitmentData != null && !string.IsNullOrEmpty(recruitmentData.thumbnail))
        //                    _fileUploader.DeleteFile(recruitmentData.thumbnail);
        //            }
        //            deleteQualificationLookupids = string.Join(",", recruitmentData.Qualifications.Where(z => !model.Qualifications.Contains(z)).ToList() ?? new List<int>());
        //            model.Qualifications = model.Qualifications.Where(z => !recruitmentData.Qualifications.Contains(z)).ToList() ?? new List<int>();
        //            deleteJobdesignationLookupids = string.Join(",", recruitmentData.jobDesignations.Where(z => !model.jobDesignations.Contains(z)).ToList() ?? new List<int>());
        //            model.jobDesignations = model.jobDesignations.Where(z => !recruitmentData.jobDesignations.Contains(z)).ToList() ?? new List<int>();

        //        }
        //        foreach (var item in model.InternalAttachments)
        //        {
        //            XElement attach = new XElement("Document",
        //                new XElement("Path", item.Path)
        //                );
        //            attachment.Root.Add(attach);
        //        }

        //        // Added HowToApplyAndQuickLinkLookup
        //        if (model is not null && model.HowToApplyAndQuickLinkLookup.Count > 0)
        //        {
        //            foreach (var item in model.HowToApplyAndQuickLinkLookup)
        //            {
        //                XElement HowToApplyAndQuick = new XElement("HowToApplyAndQuickLink",
        //                    new XElement("Id", item.Id),
        //                    new XElement("Title", item.Title),
        //                    new XElement("TitleHindi", item.TitleHindi),
        //                    new XElement("LinkUrl", item.LinkUrl),
        //                    new XElement("IsQuickLink", item.IsQuickLink),
        //                    new XElement("Description", item.Description),
        //                    new XElement("Description", item.Description),
        //                    new XElement("DescriptionJson", item.DescriptionJson),
        //                    new XElement("DescriptionHindiJson", item.DescriptionHindiJson),
        //                    new XElement("IconClass", item.IconClass),
        //                    new XElement("IsUpdate", item.IsUpdate)
        //                    );
        //                howToApplyAndQuickLinkLookup.Root.Add(HowToApplyAndQuick);
        //            }
        //        }

        //        // Added FAQ
        //        if (model is not null && model.FAQLookup.Count > 0)
        //        {
        //            foreach (var item in model.FAQLookup)
        //            {
        //                XElement FAQLookupObj = new XElement("FAQ",
        //                    new XElement("Id", item.Id),
        //                    new XElement("Que", item.Que),
        //                    new XElement("Ans", item.Ans),
        //                    new XElement("QueHindi", item.QueHindi),
        //                    new XElement("AnsHindi", item.AnsHindi),
        //                    new XElement("IsUpdate", item.isUpdate)
        //                    );

        //                FAQLookup.Root.Add(FAQLookupObj);
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(model.SlugUrl))
        //            model.SlugUrl = SlugHelper.ToUrlSlug(model.SlugUrl);


        //        //Parameters 
        //        parameters.Add("@Id", model.Id);
        //        parameters.Add("@Title", model.Title);
        //        parameters.Add("@DepartmentId", model.DepartmentId);
        //        parameters.Add("@Salary", model.Salary);
        //        parameters.Add("@Description", model.Description);
        //        parameters.Add("@DescriptionJson", model.DescriptionJson);
        //        parameters.Add("@MinAge", model.MinAge);
        //        parameters.Add("@MaxAge", model.MaxAge);
        //        parameters.Add("@StartDate", model.StartDate);
        //        parameters.Add("@LastDate", model.LastDate);
        //        parameters.Add("@ExtendedDate", model.ExtendedDate);
        //        parameters.Add("@FeePaymentLastDate", model.FeePaymentLastDate);
        //        parameters.Add("@CorrectionLastDate", model.CorrectionLastDate);
        //        parameters.Add("@AdmitCardDate", model.AdmitCardDate);
        //        parameters.Add("@ExamMode", model.ExamMode);
        //        parameters.Add("@ApplyLink", model.ApplyLink);
        //        parameters.Add("@OfficialLink", model.OfficialLink);
        //        parameters.Add("@NotificationLink", model.NotificationLink);
        //        parameters.Add("@TotalPost", model.TotalPost);
        //        parameters.Add("@UserId", model.UserId);
        //        parameters.Add("@ShortDesription", model.ShortDesription);
        //        parameters.Add("@SlugUrl", model.SlugUrl);
        //        parameters.Add("@Thumbnail", model.thumbnail);
        //        parameters.Add("@CategoryId", model.CategoryId);
        //        parameters.Add("@Keywords", model.Keywords);
        //        parameters.Add("@SubCategoryId", model.SubCategoryId);
        //        parameters.Add("@StateId", model.StateId);
        //        parameters.Add("@TitleHindi", model.TitleHindi);
        //        parameters.Add("@DescriptionHindi", model.DescriptionHindi);
        //        parameters.Add("@DescriptionHindiJson", model.DescriptionHindiJson);
        //        parameters.Add("@ShortDesriptionHindi", model.ShortDesriptionHindi);
        //        parameters.Add("@BlockTypeCode", model.BlockTypeCode);
        //        parameters.Add("@QualificationLookupIds", string.Join(",", model.Qualifications));
        //        parameters.Add("@JobDesignationLookupIds", string.Join(",", model.jobDesignations));
        //        parameters.Add("@DeleteJobDesignationLookupIds", !string.IsNullOrEmpty(deleteJobdesignationLookupids) ? deleteJobdesignationLookupids : string.Empty);
        //        parameters.Add("@DeleteDocumentLookupIds", model.DeleteDocumentLookupIds);
        //        parameters.Add("@ThumbnailCaption", model.ThumbnailCaption);
        //        parameters.Add("@SocialMediaUrl", model.SocialMediaUrl);
        //        parameters.Add("@IsCompleted", model.IsCompleted);
        //        parameters.Add("@ShouldReminder", model.ShouldReminder);
        //        parameters.Add("@ReminderDescription", model.ReminderDescription);
        //        parameters.Add("@UpcomingCalendarCode", model.UpcomingCalendarCode);
        //        parameters.Add("@HowToApplyAndQuickLinkLookup", howToApplyAndQuickLinkLookup.ToString(), DbType.Xml, ParameterDirection.Input);
        //        parameters.Add("@DocumentLookup", attachment.ToString(), DbType.Xml, ParameterDirection.Input);
        //        parameters.Add("@DeleteQualificationLookupIds", !string.IsNullOrEmpty(deleteQualificationLookupids) ? deleteQualificationLookupids : string.Empty);
        //        parameters.Add("@FAQLookup", FAQLookup.ToString(), DbType.Xml, ParameterDirection.Input);
        //        parameters.Add("@KeywordsHindi", model.KeywordsHindi);

        //        var result = Execute(@"Sp_RecruitmentAddUpdate 
        //        @Id,@Title,@DepartmentId,@Salary,@Description,@MinAge,@MaxAge,@StartDate,@LastDate,
        //        @ExtendedDate,@FeePaymentLastDate,@CorrectionLastDate,@AdmitCardDate,@ExamMode,@ApplyLink
        //        ,@OfficialLink,@NotificationLink,@TotalPost,@ShortDesription,@UserId,@SlugUrl,
        //        @Thumbnail,@CategoryId,@Keywords,@SubCategoryId,@StateId,@TitleHindi,
        //        @DescriptionHindi,@ShortDesriptionHindi,@DescriptionJson,@DescriptionHindiJson,@BlockTypeCode,@QualificationLookupIds,@JobDesignationLookupIds,@DeleteJobDesignationLookupIds,
        //        @DeleteDocumentLookupIds,@DeleteQualificationLookupIds,@ThumbnailCaption,@SocialMediaUrl,@IsCompleted,@ShouldReminder,@ReminderDescription,@UpcomingCalendarCode,@HowToApplyAndQuickLinkLookup,@DocumentLookup,@FAQLookup,@KeywordsHindi", parameters, lSqlTrans);
        //        if (!result.IsSuccess && result.StatusCode == 408)//SlugUrlExist
        //        {
        //            return SetResultStatus<int>(result.Data, MessageStatus.SlugUrlExist, result.IsSuccess);
        //        }
        //        if (!result.IsSuccess)
        //        {
        //            lSqlTrans.Rollback();
        //            filesDeleteIfException(model, attachmentList);
        //            return SetResultStatus<int>(0, MessageStatus.Error, false);
        //        }
        //        if (result.IsSuccess)
        //        {
        //            lSqlTrans.Commit();
        //            if (model.Id > 0)
        //            {
        //                //delete notificationFile
        //                if (model.Id > 0 && string.IsNullOrEmpty(model.NotificationLink))
        //                {
        //                    if (recruitmentData != null && !string.IsNullOrEmpty(recruitmentData.NotificationLink))
        //                        _fileUploader.DeleteFile(recruitmentData.NotificationLink);
        //                }

        //                //delete thumbnail
        //                if (model.Id > 0 && string.IsNullOrEmpty(model.thumbnail))
        //                {
        //                    if (recruitmentData != null && !string.IsNullOrEmpty(recruitmentData.thumbnail))
        //                        _fileUploader.DeleteFile(recruitmentData.thumbnail);
        //                }

        //                // delete Attachment
        //                if (!string.IsNullOrEmpty(model.DeleteDocumentLookupIds))
        //                {
        //                    foreach (var attachId in model.DeleteDocumentLookupIds.Split(',').Select(int.Parse).ToArray())
        //                    {
        //                        var filePath = recruitmentData.InternalAttachments.Where(z => z.Id == attachId).FirstOrDefault();
        //                        _fileUploader.DeleteFile(filePath.Path);
        //                    }
        //                }
        //                response = SetResultStatus<int>(result.Data, MessageStatus.Update, result.IsSuccess);
        //            }
        //            else
        //                response = SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess);
        //        }
        //        else
        //        {
        //            filesDeleteIfException(model, attachmentList);
        //            lSqlTrans.Rollback();
        //            response = SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        filesDeleteIfException(model, attachmentList);
        //        lSqlTrans.Rollback();
        //        Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "RecruitmentService"));
        //        return SetResultStatus<int>(0, MessageStatus.Error, false, "");
        //    }
        //}

        public async Task<ServiceResponse<long>> AddUpdate(RecruitmentReqestDTO model)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            using (var transaction = await _dBContext.DbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    Recruitment_MDL Recruitment_ = await _dBContext.Recruitment.Where(s => !s.IsDelete && ((s.Id == model.Id && model.Id > 0)
                                                || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id == 0)
                                                || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id != 0))).FirstOrDefaultAsync();

                    if (model.Id > 0 && Recruitment_ is null)
                    {
                        return SetResultStatus<long>(model.Id, MessageStatus.NotExist, false);
                    }

                    else if (!string.IsNullOrEmpty(model.SlugUrl) && (Recruitment_ is not null && Recruitment_.SlugUrl == model.SlugUrl && model.Id == 0) || (Recruitment_ is not null && Recruitment_.SlugUrl == model.SlugUrl && Recruitment_.Id != model.Id))
                    {
                        return SetResultStatus<long>(model.Id, MessageStatus.SlugUrlExist, false);
                    }

                    if (model.Id > 0 && Recruitment_ is not null)
                    {
                        Recruitment_ = _mapper.Map<RecruitmentReqestDTO, Recruitment_MDL>(model, Recruitment_, opts => opts.BeforeMap((src, des) => { if (des.Status == (int)CommonEnum.ProgressStatus.published) { src.SlugUrl = des.SlugUrl; } }));
                        Recruitment_.ModifiedBy = _jWTAuthManager.User.Id;
                        _dBContext.Recruitment.Update(Recruitment_);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        // child tables
                        #region JobDesignation
                        List<RecruitmentJobDesignationLookup_MDL> rcJob = await _dBContext.RecruitmentJobDesignationLookup.Where(s => s.RecruitmentId == model.Id).ToListAsync();
                        List<int> removercJob_ = model.JobDesignations.Select(s => s).ToList();
                        List<RecruitmentJobDesignationLookup_MDL> removeJob = rcJob.Where(s => !removercJob_.Contains(s.JobDesignationId)).ToList();
                        if (removeJob.Count() > 0)
                        {
                            _dBContext.RecruitmentJobDesignationLookup.RemoveRange(removeJob);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        List<RecruitmentJobDesignationLookup_MDL> rcJob_MDL = new List<RecruitmentJobDesignationLookup_MDL>();
                        foreach (var item in model.JobDesignations.Where(s => !rcJob.Select(s => s.JobDesignationId).Contains(s)).ToList())
                        {
                            RecruitmentJobDesignationLookup_MDL rcJob_ = new RecruitmentJobDesignationLookup_MDL() { RecruitmentId = (int)Recruitment_.Id, JobDesignationId = item };
                            rcJob_MDL.Add(rcJob_);
                        }
                        if (rcJob_MDL is not null && rcJob_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentJobDesignationLookup.AddRangeAsync(rcJob_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Qualification
                        List<RecruitmentQualificationLookup_MDL> rcQual = await _dBContext.RecruitmentQualificationLookup.Where(s => s.RecruitmentId == model.Id).ToListAsync();
                        List<int> rcQual_ = model.Qualifications.Select(s => s).ToList();
                        List<RecruitmentQualificationLookup_MDL> removercQual_ = rcQual.Where(s => !rcQual_.Contains(s.QualificationId)).ToList();
                        if (removercQual_.Count() > 0)
                        {
                            _dBContext.RecruitmentQualificationLookup.RemoveRange(removercQual_);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        List<RecruitmentQualificationLookup_MDL> rcQal_MDL = new List<RecruitmentQualificationLookup_MDL>();
                        foreach (var item in model.Qualifications.Where(s => !rcQual.Select(s => s.QualificationId).Contains(s)).ToList())
                        {
                            RecruitmentQualificationLookup_MDL rcQal_ = new RecruitmentQualificationLookup_MDL() { RecruitmentId = (int)Recruitment_.Id, QualificationId = item };
                            rcQal_MDL.Add(rcQal_);
                        }
                        if (rcQal_MDL is not null && rcQal_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentQualificationLookup.AddRangeAsync(rcQal_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Document
                        //remove
                        List<RecruitmentDocumentLookup_MDL> rcAttachments = await _dBContext.RecruitmentDocumentLookups.Where(s => s.RecruitmentId == model.Id).ToListAsync();
                        List<string> rcAttachPaths = model.Attachments.Select(s => s).ToList();
                        List<RecruitmentDocumentLookup_MDL> removeRCAttachPaths = rcAttachments.Where(s => !rcAttachPaths.Contains(s.Path)).ToList();
                        if (removeRCAttachPaths.Count() > 0)
                        {
                            _dBContext.RecruitmentDocumentLookups.RemoveRange(removeRCAttachPaths);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add
                        List<RecruitmentDocumentLookup_MDL> rcAttachment_MDL = new List<RecruitmentDocumentLookup_MDL>();
                        model.Attachments = model.Attachments.Where(s => !rcAttachments.Select(s => s.Path).Contains(s)).ToList();
                        foreach (var item in model.Attachments)
                        {
                            RecruitmentDocumentLookup_MDL rcAttachment_ = new RecruitmentDocumentLookup_MDL() { RecruitmentId = model.Id, Path = item };
                            rcAttachment_MDL.Add(rcAttachment_);
                        }
                        if (rcAttachment_MDL is not null && rcAttachment_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentDocumentLookups.AddRangeAsync(rcAttachment_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region HowToApplyAndQuickLink
                        //remove 
                        List<RecruitmentHowToApplyAndQuickLinkLookup_MDL> HWQKs = await _dBContext.RecruitmentHowToApplyAndQuickLinkLookup.Where(s => s.RecruitmentId == model.Id).ToListAsync();
                        List<int> HWQKIds = model.HowToApplyAndQuickLinkLookup.Where(s => s.Id > 0).Select(s => s.Id).ToList();
                        List<RecruitmentHowToApplyAndQuickLinkLookup_MDL> removeHWQKs = HWQKs.Where(s => !HWQKIds.Contains(s.Id)).ToList();
                        if (removeHWQKs.Count() > 0)
                        {
                            _dBContext.RecruitmentHowToApplyAndQuickLinkLookup.RemoveRange(removeHWQKs);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add
                        //model.HowToApplyAndQuickLinkLookup = model.HowToApplyAndQuickLinkLookup.Where(s => s.Id >= 0 && !HWQKs.Select(s => s.Id).Contains(s.Id)).ToList();
                        List<RecruitmentHowToApplyAndQuickLinkLookup_MDL> HWQKs_MDL = new List<RecruitmentHowToApplyAndQuickLinkLookup_MDL>();
                        List<RecruitmentHowToApplyAndQuickLinkLookup_MDL> HWQKsUpdate_MDL = new List<RecruitmentHowToApplyAndQuickLinkLookup_MDL>();
                        foreach (var item in model.HowToApplyAndQuickLinkLookup.Where(s => s.Id >= 0))
                        {
                            item.RecruitmentId = (int)Recruitment_.Id;
                            if (item.Id > 0)
                            {
                                RecruitmentHowToApplyAndQuickLinkLookup_MDL HWQKEntity = _mapper.Map<RecruitmentHowToApplyAndQuickLinkLookup_MDL>(item);
                                HWQKsUpdate_MDL.Add(HWQKEntity);
                            }
                            else
                            {
                                RecruitmentHowToApplyAndQuickLinkLookup_MDL HWQK = _mapper.Map<RecruitmentHowToApplyAndQuickLinkLookup_MDL>(item);
                                HWQKs_MDL.Add(HWQK);
                            }

                        }
                        if (HWQKsUpdate_MDL is not null && HWQKsUpdate_MDL.Count() > 0)
                        {
                            _dBContext.RecruitmentHowToApplyAndQuickLinkLookup.UpdateRange(HWQKsUpdate_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (HWQKs_MDL is not null && HWQKs_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentHowToApplyAndQuickLinkLookup.AddRangeAsync(HWQKs_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        #region FAQ
                        List<FAQ_MDL> BCFaqs = await _dBContext.FAQ.Where(s => s.ModuleId == model.Id && s.BlockTypeId == model.BlockTypeCode).ToListAsync();
                        List<int> faqIds = model.FAQLookup.Where(s => s.Id > 0).Select(s => s.Id).ToList();
                        List<FAQ_MDL> removeFaqs = BCFaqs.Where(s => !faqIds.Contains(s.Id)).ToList();
                        if (removeFaqs.Count() > 0)
                        {
                            _dBContext.FAQ.RemoveRange(removeFaqs);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        List<FAQ_MDL> faqUpdate_MDL = new List<FAQ_MDL>();
                        List<FAQ_MDL> faq_MDL = new List<FAQ_MDL>();
                        foreach (var item in model.FAQLookup.Where(s => s.Id >= 0))
                        {
                            item.ModuleId = (int)Recruitment_.Id;
                            item.BlockTypeId = (int)Recruitment_.BlockTypeCode;
                            if (item.Id > 0)
                            {
                                FAQ_MDL faqUpdateEntity = _mapper.Map<FAQ_MDL>(item);
                                faqUpdate_MDL.Add(faqUpdateEntity);
                            }
                            else
                            {
                                FAQ_MDL faq = _mapper.Map<FAQ_MDL>(item);
                                faq_MDL.Add(faq);
                            }
                        }
                        if (faqUpdate_MDL is not null && faqUpdate_MDL.Count() > 0)
                        {
                            _dBContext.FAQ.UpdateRange(faqUpdate_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (faq_MDL is not null && faq_MDL.Count() > 0)
                        {
                            await _dBContext.FAQ.AddRangeAsync(faq_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Tags
                        List<RecruitmentTags_MDL> BCTags = await _dBContext.RecruitmentTags.Where(s => s.RecruitmentId == model.Id).ToListAsync();
                        // remove tags 
                        List<int> tagsIds = model.Tags.Select(s => s).ToList();
                        List<RecruitmentTags_MDL> removetags = BCTags.Where(s => !tagsIds.Contains(s.TagsId)).ToList();
                        if (removetags.Count > 0)
                        {
                            _dBContext.RecruitmentTags.RemoveRange(removetags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        List<RecruitmentTags_MDL> BCTags_MDL = new List<RecruitmentTags_MDL>();
                        model.Tags = model.Tags.Where(s => !BCTags.Select(s => s.TagsId).Contains(s)).ToList();
                        foreach (var item in model.Tags)
                        {
                            RecruitmentTags_MDL BCTags_ = new RecruitmentTags_MDL() { RecruitmentId = (int)Recruitment_.Id, TagsId = item };
                            BCTags_MDL.Add(BCTags_);
                        }
                        if (BCTags_MDL is not null && BCTags_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentTags.AddRangeAsync(BCTags_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        await transaction.CommitAsync();
                        return SetResultStatus<long>(Recruitment_.Id, MessageStatus.Update, true);
                    }
                    else
                    {
                        Recruitment_MDL entity = _mapper.Map<Recruitment_MDL>(model);
                        entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Status = (int)CommonEnum.ProgressStatus.unapproved;
                        await _dBContext.Recruitment.AddAsync(entity);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        // child tables
                        #region JobDesignation
                        List<RecruitmentJobDesignationLookup_MDL> rcJob_MDL = new List<RecruitmentJobDesignationLookup_MDL>();
                        foreach (var item in model.JobDesignations)
                        {
                            RecruitmentJobDesignationLookup_MDL rcJob_ = new RecruitmentJobDesignationLookup_MDL() { RecruitmentId = (int)entity.Id, JobDesignationId = item };
                            rcJob_MDL.Add(rcJob_);
                        }
                        if (rcJob_MDL is not null && rcJob_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentJobDesignationLookup.AddRangeAsync(rcJob_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Qualification
                        List<RecruitmentQualificationLookup_MDL> rcQal_MDL = new List<RecruitmentQualificationLookup_MDL>();
                        foreach (var item in model.Qualifications)
                        {
                            RecruitmentQualificationLookup_MDL rcQal_ = new RecruitmentQualificationLookup_MDL() { RecruitmentId = (int)entity.Id, QualificationId = item };
                            rcQal_MDL.Add(rcQal_);
                        }
                        if (rcQal_MDL is not null && rcQal_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentQualificationLookup.AddRangeAsync(rcQal_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Document
                        List<RecruitmentDocumentLookup_MDL> rcAttachment_MDL = new List<RecruitmentDocumentLookup_MDL>();
                        foreach (var item in model.Attachments)
                        {
                            RecruitmentDocumentLookup_MDL rcAttachment_ = new RecruitmentDocumentLookup_MDL() { RecruitmentId = entity.Id, Path = item };
                            rcAttachment_MDL.Add(rcAttachment_);
                        }
                        if (rcAttachment_MDL is not null && rcAttachment_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentDocumentLookups.AddRangeAsync(rcAttachment_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region HowToApplyAndQuickLink
                        List<RecruitmentHowToApplyAndQuickLinkLookup_MDL> HWQKs_MDL = new List<RecruitmentHowToApplyAndQuickLinkLookup_MDL>();
                        foreach (var item in model.HowToApplyAndQuickLinkLookup)
                        {
                            item.RecruitmentId = (int)entity.Id;
                            RecruitmentHowToApplyAndQuickLinkLookup_MDL HWQK = _mapper.Map<RecruitmentHowToApplyAndQuickLinkLookup_MDL>(item);
                            HWQKs_MDL.Add(HWQK);
                        }

                        if (HWQKs_MDL is not null && HWQKs_MDL.Count() > 0)
                        {
                            await _dBContext.RecruitmentHowToApplyAndQuickLinkLookup.AddRangeAsync(HWQKs_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        #region FAQ
                        List<FAQ_MDL> faq_MDL = new List<FAQ_MDL>();
                        foreach (var item in model.FAQLookup)
                        {
                            item.ModuleId = (int)entity.Id;
                            item.BlockTypeId = (int)entity.BlockTypeCode;
                            FAQ_MDL faq = _mapper.Map<FAQ_MDL>(item);
                            faq_MDL.Add(faq);
                        }

                        if (faq_MDL is not null && faq_MDL.Count() > 0)
                        {
                            await _dBContext.FAQ.AddRangeAsync(faq_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Tags
                        if (model.Tags is not null && model.Tags.Count() > 0)
                        {
                            List<RecruitmentTags_MDL> BCTags_MDL = new List<RecruitmentTags_MDL>();
                            foreach (var item in model.Tags)
                            {
                                RecruitmentTags_MDL BCTags_ = new RecruitmentTags_MDL() { RecruitmentId = (int)entity.Id, TagsId = item };
                                BCTags_MDL.Add(BCTags_);
                            }
                            await _dBContext.RecruitmentTags.AddRangeAsync(BCTags_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        await transaction.CommitAsync();
                        await _dBContext.DbContext.DisposeAsync();
                        return SetResultStatus<long>(entity.Id, MessageStatus.Save, true);
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _dBContext.DbContext.DisposeAsync();
                    Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "addupdate"));
                    return SetResultStatus<long>(0, MessageStatus.Error, false, "");
                }
            }
        }
        private void filesDeleteIfException(RecruitmentModel model, List<string> attachments)
        {
            if (!string.IsNullOrEmpty(model.NotificationLink))
                _fileUploader.DeleteFile(model.NotificationLink);
            if (!string.IsNullOrEmpty(model.thumbnail))
                _fileUploader.DeleteFile(model.thumbnail);
            foreach (var path in attachments)
            {
                _fileUploader.DeleteFile(path);
            }
        }
        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"Sp_RecruitmentDeleteUpdateStatus @Type='Delete',@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
                if (deleteResult.IsSuccess)
                {
                    if (deleteResult.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.Delete, true);
                    else if (deleteResult.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, false);
                    else
                        return SetResultStatus<bool>(false, MessageStatus.Error, true);
                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.Error, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<RecruitmentResponseDTO> GetById(int Id)
        {
            RecruitmentResponseDTO resultModel = new RecruitmentResponseDTO();
            try
            {
                var result = QueryMultiple(@"SP_GetDetailOfRecruitmentById @Id", new { Id = Id });

                if (result.Data is not null && SPResultHandler.GetCount(result.Data[0]) > 0)
                {
                    resultModel = SPResultHandler.GetObject<RecruitmentResponseDTO>(result.Data[0]);
                    if (SPResultHandler.GetCount(result.Data[1]) > 0)
                        resultModel.JobDesignations = SPResultHandler.GetList<RecruitmentJobDesignationResponseDTO>(result.Data[1]).Select(s => s.JobDesignationId).ToList();
                    if (SPResultHandler.GetCount(result.Data[2]) > 0)
                        resultModel.Qualifications = SPResultHandler.GetList<RecruitmentQualificationResponseDTO>(result.Data[2]).Select(s => s.QualificationId).ToList();
                    if (SPResultHandler.GetCount(result.Data[3]) > 0)
                        resultModel.Attachments = SPResultHandler.GetList<RecruitmentDocumentResponseDTO>(result.Data[3]).Select(s => s.Path).ToList();
                    if (SPResultHandler.GetCount(result.Data[4]) > 0)
                        resultModel.HowToApplyAndQuickLinkLookup = SPResultHandler.GetList<RHowToApplyAndQuickLinkResponseDTO>(result.Data[4]);
                    if (SPResultHandler.GetCount(result.Data[5]) > 0)
                        resultModel.FAQLookup = SPResultHandler.GetList<FAQResponseDTO>(result.Data[5]);

                    if (SPResultHandler.GetCount(result.Data[6]) > 0)
                        resultModel.Tags = SPResultHandler.GetList<RecruitmentTagsResponseDTO>(result.Data[6]).Select(d => d.TagsId).ToList();

                    return SetResultStatus<RecruitmentResponseDTO>(resultModel, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<RecruitmentResponseDTO>(null, MessageStatus.NoRecord, true);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "GetById"));
                return SetResultStatus<RecruitmentResponseDTO>(null, MessageStatus.Error, false, ex.Message);
            }
        }
        //public ServiceResponse<RecruitmentModel> GetById(int Id)
        //{
        //    RecruitmentModel resultModel = new RecruitmentModel();
        //    try
        //    {
        //        var result = QueryMultiple(@"SP_GetDetailOfRecruitmentById @Id", new { Id = Id });

        //        if (result.Data is not null && SPResultHandler.GetCount(result.Data[0]) > 0)
        //        {
        //            resultModel = SPResultHandler.GetObject<RecruitmentModel>(result.Data[0]);
        //            if (SPResultHandler.GetCount(result.Data[1]) > 0)
        //                resultModel.jobDesignations = SPResultHandler.GetList<RecruitmentJobDesignationModel>(result.Data[1]).Select(s => s.JobDesignationId).ToList();
        //            if (SPResultHandler.GetCount(result.Data[2]) > 0)
        //                resultModel.Qualifications = SPResultHandler.GetList<RecruitmentQualificationModel>(result.Data[2]).Select(s => s.QualificationId).ToList();
        //            if (SPResultHandler.GetCount(result.Data[3]) > 0)
        //            {
        //                resultModel.InternalAttachments = SPResultHandler.GetList<RecruitmentDocumentModel>(result.Data[3]);
        //                resultModel.Attachments = resultModel.InternalAttachments.Select(s => s.Path).ToList();
        //            }
        //            if (SPResultHandler.GetCount(result.Data[4]) > 0)
        //                resultModel.HowToApplyAndQuickLinkLookup = SPResultHandler.GetList<RecruitmentHowToApplyAndQuickLinkLookup>(result.Data[4]);
        //            if (SPResultHandler.GetCount(result.Data[5]) > 0)
        //                resultModel.FAQLookup = SPResultHandler.GetList<RecruitmentFAQLookup>(result.Data[5]);

        //            return SetResultStatus<RecruitmentModel>(resultModel, MessageStatus.Success, true);
        //        }
        //        else
        //        {
        //            return SetResultStatus<RecruitmentModel>(null, MessageStatus.NoRecord, true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "GetById"));
        //        return SetResultStatus<RecruitmentModel>(null, MessageStatus.Error, false, ex.Message);
        //    }
        //}
        public ServiceResponse<List<RecruitmentViewModel>> GetPagination(RecruitmentFilterModel filterModel)
        {
            try
            {
                filterModel.UserId = _jWTAuthManager.User.Id;
                var result = QueryList<RecruitmentViewModel>(@"Sp_RecruitmentPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@PublisherId,@DepartmentId,@IsApproved,@CategoryId,@SubCategoryId,@BlockTypeCode,@UserId", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<RecruitmentViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    result.StatusCode = StatusCodes.Status404NotFound;
                    return SetResultStatus<List<RecruitmentViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "GetPagination"));
                return SetResultStatus<List<RecruitmentViewModel>>(null, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(@"Sp_RecruitmentDeleteUpdateStatus @Type='Status',@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
                    else if (result.StatusCode == StatusCodes.Status404NotFound)
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, true);
                    else
                        return SetResultStatus<bool>(false, MessageStatus.Error, true);
                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.Error, false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
        public ServiceResponse<List<RecruitmentTitleCheckModel>> CheckRecruitmentTitle(string SearchText)
        {
            ServiceResponse<List<RecruitmentTitleCheckModel>> serviceResponse = new ServiceResponse<List<RecruitmentTitleCheckModel>>();
            List<RecruitmentTitleCheckModel> result = new List<RecruitmentTitleCheckModel>();
            try
            {
                serviceResponse.Data = QueryList<RecruitmentTitleCheckModel>(@"Sp_CheckRecruitmentTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "CheckRecruitmentTitle"));
                return serviceResponse;
            }
        }
        public ServiceResponse<bool> RecruitmentProgressStatus(int Id, int ProgressStatus)
        {
            try
            {
                if (Id > 0 && ProgressStatus > 0)
                {
                    var recruitmentData = QueryFast<RecruitmentViewModel>(@"select * from Vw_Recruitment where Id=@Id", new { Id = Id }); ;

                    if (recruitmentData.Data != null)
                    {
                        // Create Deeplink 
                        //string Deeplink = string.Empty;
                        //ResponseDeepLinkModel res = new ResponseDeepLinkModel();
                        //if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                        //{
                        //    DeeplinkModel link = new DeeplinkModel()
                        //    {
                        //        isRecruitment = true,
                        //        moduleName = recruitmentData.Data.ModuleSlug,
                        //        type = "post",
                        //        slugUrl = recruitmentData.Data.SlugUrl,
                        //        id = (int)recruitmentData.Data.Id,
                        //        thumbnail = !string.IsNullOrEmpty(recruitmentData.Data.DepartmentLogo) ? recruitmentData.Data.DepartmentLogo : string.Empty,
                        //        title = recruitmentData.Data.Title
                        //    };
                        //    Deeplink = JsonConvert.SerializeObject(link);
                        //    _fairBaseHelper.PostPushNotification(link, link.title, link.title);
                        //    var getDeeplink = Deeplink.GenerateDeeplink(link.title, recruitmentData.Data.ShortDesription, link.thumbnail);
                        //    var deeplink = _fairBaseHelper.Post(getDeeplink);
                        //    if (!string.IsNullOrEmpty(deeplink))
                        //    {
                        //        res = JsonConvert.DeserializeObject<ResponseDeepLinkModel>(deeplink);
                        //    }
                        //}
                        var result = ExecuteReturnData(@"Sp_RecruitmentDeleteUpdateStatus @Type='ProgressStatus',@StatusCode=" + ProgressStatus + ",@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + ",@SortLinks='" + null + "'", null);
                        if (result.IsSuccess)
                        {
                            if (result.StatusCode == StatusCodes.Status200OK)
                                return SetResultStatus<bool>(true, MessageStatus.StatusProgressUpdate, true);

                            if (result.StatusCode == StatusCodes.Status203NonAuthoritative)
                                return SetResultStatus<bool>(true, MessageStatus.StatusProgressNotValidCodeUpdate, true);

                            else
                                return SetResultStatus<bool>(false, MessageStatus.Error, true);
                        }
                        else
                        {
                            result.StatusCode = StatusCodes.Status404NotFound;
                            return SetResultStatus<bool>(false, MessageStatus.Error, false);
                        }
                    }
                    else
                    {
                        return SetResultStatus<bool>(false, MessageStatus.NoRecord, false);
                    }
                }
                else
                {
                    return SetResultStatus<bool>(false, MessageStatus.StatusProgressRequiredUpdate, false);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("RecruitmentService.cs", "RecruitmentProgressStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
