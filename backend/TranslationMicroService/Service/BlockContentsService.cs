using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using Dapper;
using DBInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModelService.CommonModel;
using ModelService.MDL.Translation;
using ModelService.Model;
using ModelService.Model.Front;
using ModelService.OtherModels;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Reflection;
using System.Xml.Linq;
using TranslationMicroService.IService;
using static CommonService.Other.Deeplink;

namespace TranslationMicroService.Service
{
    public class BlockContentsService : UtilityManager, IBlockContentsService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly FairBaseHelper _fairBaseHelper;
        private readonly IDBPreptmContext _dBContext;
        private readonly IMapper _mapper;

        public BlockContentsService(HelperService helperService, JWTAuthManager jWTAuthManager, FairBaseHelper fairBaseHelper, IDBPreptmContext dBContext, IMapper mapper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _dBContext = dBContext;
            _mapper = mapper;
        }

        //public ServiceResponse<int> AddUpdate(BlockContentsModel model)
        //{
        //    SqlTransaction lSqlTrans = GetNewTransaction();
        //    List<string> imagePaths = new List<string>();
        //    Random rnd = new Random();
        //    BlockContentsModel blockContentsData = new BlockContentsModel();
        //    XDocument documentAttach = new XDocument(new XElement("BlockContentAttachment"));
        //    XDocument howToApplyAndQuickLinkLookup = new XDocument(new XElement("HowToApplyAndQuickLinkLookup"));
        //    XDocument FAQLookup = new XDocument(new XElement("FAQLookup"));
        //    DynamicParameters parameters = new DynamicParameters();
        //    ServiceResponse<int> response = new ServiceResponse<int>();
        //    var datetime = DateTime.UtcNow;
        //    try
        //    {
        //        model.UserId = _jWTAuthManager.User.Id;
        //        if (model.Id == 0 && !string.IsNullOrEmpty(model.SlugUrl))
        //            model.SlugUrl = SlugHelper.ToUrlSlug(model.SlugUrl);

        //        if (model.Id > 0)
        //        {
        //            blockContentsData = GetById((int)model.Id).Data;
        //            //Delele Attachment
        //            model.DBDeleteAttachmentLookupIds = string.Join(",", blockContentsData.InertnalDocuments
        //                           .Where(z => !model.InertnalDocuments.Where(a => a.Id.HasValue == true).Select(a => a.Id).Contains(z.Id)).Select(a => a.Id).ToList());

        //        }

        //        // Added Attachment
        //        if (model.Documents is not null && model.Documents.Count > 0)
        //        {
        //            foreach (var item in model.Documents)
        //            {
        //                XElement attach = new XElement("Attachment",
        //                    new XElement("Path", item)
        //                    );
        //                documentAttach.Root.Add(attach);
        //            }
        //            model.DBAttachmentLookup = documentAttach;
        //        }

        //        // Added HowToApplyAndQuickLinkLookup
        //        if (model is not null && model.HowToApplyAndQuickLinkLookup.Count > 0)
        //        {
        //            foreach (var item in model.HowToApplyAndQuickLinkLookup)
        //            {
        //                XElement HowToApplyAndQuick = new XElement("HowToApplyAndQuickLink",
        //                            new XElement("Id", item.Id),
        //                    new XElement("IsQuickLink", item.IsQuickLink),
        //                    new XElement("Title", item.Title),
        //                    new XElement("TitleHindi", item.TitleHindi),
        //                    new XElement("LinkUrl", item.LinkUrl),
        //                    new XElement("Description", item.Description),
        //                    new XElement("DescriptionHindi", item.DescriptionHindi),
        //                    new XElement("DescriptionJson", item.DescriptionJson),
        //                    new XElement("DescriptionHindiJson", item.DescriptionHindiJson),
        //                    new XElement("IconClass", item.IconClass),
        //                    new XElement("IsUpdate", item.IsUpdate)
        //                    );
        //                howToApplyAndQuickLinkLookup.Root.Add(HowToApplyAndQuick);
        //            }
        //            model.DBHowToApplyAndQuickLinkLookup = howToApplyAndQuickLinkLookup;
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
        //                    new XElement("IsUpdate", item.IsUpdate)
        //                    );

        //                FAQLookup.Root.Add(FAQLookupObj);
        //            }
        //        }

        //        // added notificationFile
        //        //if (notificationFile != null)
        //        //{
        //        //    if (System.IO.Path.GetExtension(notificationFile.FileName) == ".pdf")
        //        //    {
        //        //        FileUploadModel file = new FileUploadModel();
        //        //        file.file = notificationFile;
        //        //        file.filename = rnd.Next(0, 1000000).ToString() + "_" + datetime.Millisecond.ToString() + ".pdf";//System.IO.Path.GetExtension(file.file.FileName); //file.file.
        //        //        file.path = "blockcontent/notification";
        //        //        model.NotificationLink = _fileUploader.PostFile(file);
        //        //    }
        //        //    else
        //        //    {
        //        //        lSqlTrans.Rollback();
        //        //        return SetResultStatus<int>(0, MessageStatus.onlyPDFValid, false);
        //        //    }
        //        //}

        //        parameters.Add("@Id", model.Id);
        //        parameters.Add("@Title", model.Title);
        //        parameters.Add("@BlockTypeId", model.BlockTypeId);
        //        parameters.Add("@RecruitmentId", model.RecruitmentId);
        //        parameters.Add("@DepartmentId", model.DepartmentId);
        //        parameters.Add("@CategoryId", model.CategoryId);
        //        parameters.Add("@SlugUrl", model.SlugUrl);
        //        parameters.Add("@Url", model.Url);
        //        parameters.Add("@Description", model.Description);
        //        parameters.Add("@DescriptionJson", model.DescriptionJson);
        //        parameters.Add("@UserId", model.UserId);
        //        parameters.Add("@Date", model.Date);
        //        parameters.Add("@GroupId", model.GroupId);
        //        parameters.Add("@SubCategoryId", model.SubCategoryId);
        //        parameters.Add("@Keywords", model.Keywords);
        //        parameters.Add("@StateId", model.StateId);
        //        parameters.Add("@NotificationLink", model.NotificationLink);
        //        parameters.Add("@TitleHindi", model.TitleHindi);
        //        parameters.Add("@DescriptionHindi", model.DescriptionHindi);
        //        parameters.Add("@DescriptionHindiJson", model.DescriptionHindiJson);
        //        parameters.Add("@Summary", model.Summary);
        //        parameters.Add("@DBDeleteAttachmentLookupIds", model.DBDeleteAttachmentLookupIds);
        //        parameters.Add("@DBAttachmentLookup", documentAttach.ToString(), DbType.Xml, ParameterDirection.Input);
        //        parameters.Add("@DBHowToApplyAndQuickLinkLookup ", howToApplyAndQuickLinkLookup.ToString(), DbType.Xml, ParameterDirection.Input);
        //        parameters.Add("@LastDate", model.LastDate);
        //        parameters.Add("@ExtendedDate", model.ExtendedDate);
        //        parameters.Add("@FeePaymentLastDate", model.FeePaymentLastDate);
        //        parameters.Add("@CorrectionLastDate", model.CorrectionLastDate);
        //        parameters.Add("@UrlLabelId", model.UrlLabelId);
        //        parameters.Add("@ExamMode", model.ExamMode);
        //        parameters.Add("@Thumbnail", model.Thumbnail);
        //        parameters.Add("@SocialMediaUrl", model.SocialMediaUrl);
        //        parameters.Add("@ThumbnailCredit", model.ThumbnailCredit);
        //        parameters.Add("@IsCompleted", model.IsCompleted);
        //        parameters.Add("@ShouldReminder", model.ShouldReminder);
        //        parameters.Add("@ReminderDescription", model.ReminderDescription);
        //        parameters.Add("@UpcomingCalendarCode", model.UpcomingCalendarCode);
        //        parameters.Add("@FAQLookup", FAQLookup.ToString(), DbType.Xml, ParameterDirection.Input);
        //        parameters.Add("@KeywordsHindi", model.KeywordsHindi);
        //        parameters.Add("@SummaryHindi", model.SummaryHindi);


        //        var result = Execute(QueriesPaths.BlockContentsQueries.QAddUpdate, parameters, lSqlTrans);

        //        if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
        //        {
        //            lSqlTrans.Rollback();
        //            return SetResultStatus<int>(result.Data, MessageStatus.BlockContentExist, result.IsSuccess);
        //        }
        //        else if (!result.IsSuccess && result.StatusCode == 408)
        //        {
        //            lSqlTrans.Rollback();
        //            return SetResultStatus<int>(result.Data, MessageStatus.SlugUrlExist, result.IsSuccess);
        //        }
        //        else if (result.IsSuccess)
        //        {
        //            if (result.StatusCode == StatusCodes.Status200OK)
        //            {
        //                lSqlTrans.Commit();
        //                if (model.Id > 0)
        //                {
        //                    //delete NotificationLink
        //                    if (string.IsNullOrEmpty(model.NotificationLink))
        //                        if (!string.IsNullOrEmpty(blockContentsData.NotificationLink))
        //                            _fileUploader.DeleteFile(blockContentsData.NotificationLink);

        //                    if (!string.IsNullOrEmpty(model.DBDeleteAttachmentLookupIds))
        //                    {
        //                        int[] attchIds = model.DBDeleteAttachmentLookupIds.Split(',').Select(int.Parse).ToArray();
        //                        foreach (var deleteAttch in attchIds)
        //                        {
        //                            var filePath = blockContentsData.InertnalDocuments.Where(z => z.Id == Convert.ToInt32(deleteAttch)).FirstOrDefault();
        //                            if (!string.IsNullOrEmpty(filePath.Path))
        //                                _fileUploader.DeleteFile(filePath.Path);
        //                        }
        //                    }
        //                    return SetResultStatus<int>(model.Id, MessageStatus.Update, result.IsSuccess);
        //                }
        //                else
        //                {
        //                    return SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lSqlTrans.Rollback();
        //            return SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
        //        }
        //        lSqlTrans.Rollback();
        //        return SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(model.NotificationLink))
        //            _fileUploader.DeleteFile(model.NotificationLink);
        //        if (imagePaths is not null && imagePaths.Count() > 0)
        //        {
        //            foreach (var item in imagePaths)
        //            {
        //                _fileUploader.DeleteFile(item);
        //            }
        //        }
        //        Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "AddUpdate"));
        //        return SetResultStatus<int>(0, MessageStatus.Error, false);
        //    }
        //}


        public async Task<ServiceResponse<int>> AddUpdate(BlockContentsRequestDTO model)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            using (var transaction = await _dBContext.DbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    BlockContents_MDL blockContents_ = await _dBContext.BlockContents.Where(s => !s.IsDelete && ((s.Id == model.Id && model.Id > 0)
                                                || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id == 0)
                                                || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id != 0))).FirstOrDefaultAsync();

                    if (model.Id > 0 && blockContents_ is null)
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.NotExist, false);
                    }

                    else if (!string.IsNullOrEmpty(model.SlugUrl) && (blockContents_ is not null && blockContents_.SlugUrl == model.SlugUrl && model.Id == 0) || (blockContents_ is not null && blockContents_.SlugUrl == model.SlugUrl && blockContents_.Id != model.Id))
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.SlugUrlExist, false);
                    }

                    if (model.Id > 0 && blockContents_ is not null)
                    {
                        blockContents_ = _mapper.Map<BlockContentsRequestDTO, BlockContents_MDL>(model, blockContents_, opts => opts.BeforeMap((src, des) => { if (des.Status == (int)CommonEnum.ProgressStatus.published) { src.SlugUrl = des.SlugUrl; } }));
                        blockContents_.ModifiedBy = _jWTAuthManager.User.Id;
                        _dBContext.BlockContents.Update(blockContents_);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        // child tables
                        #region Document
                        //remove
                        List<BlockContentAttachmentLookup_MDL> bcAttachments = await _dBContext.BlockContentAttachmentLookup.Where(s => s.BlockContentId == model.Id).ToListAsync();
                        List<string> bcAttachPaths = model.Documents.Select(s => s).ToList();
                        List<BlockContentAttachmentLookup_MDL> removeBCAttachPaths = bcAttachments.Where(s => !bcAttachPaths.Contains(s.Path)).ToList();
                        if (removeBCAttachPaths.Count() > 0)
                        {
                            _dBContext.BlockContentAttachmentLookup.RemoveRange(removeBCAttachPaths);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add
                        List<BlockContentAttachmentLookup_MDL> bcAttachment_MDL = new List<BlockContentAttachmentLookup_MDL>();
                        model.Documents = model.Documents.Where(s => !bcAttachments.Select(s => s.Path).Contains(s)).ToList();
                        foreach (var item in model.Documents)
                        {
                            BlockContentAttachmentLookup_MDL bcAttachment_ = new BlockContentAttachmentLookup_MDL() { BlockContentId = model.Id, Path = item };
                            bcAttachment_MDL.Add(bcAttachment_);
                        }
                        if (bcAttachment_MDL is not null && bcAttachment_MDL.Count() > 0)
                        {
                            await _dBContext.BlockContentAttachmentLookup.AddRangeAsync(bcAttachment_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region HowToApplyAndQuickLink
                        //remove 
                        List<BlockContentsHowToApplyAndQuickLinkLookup_MDL> HWQKs = await _dBContext.BlockContentsHowToApplyAndQuickLinkLookup.Where(s => s.BlockContentId == model.Id).ToListAsync();
                        List<int> HWQKIds = model.HowToApplyAndQuickLinkLookup.Where(s => s.Id > 0).Select(s => s.Id).ToList();
                        List<BlockContentsHowToApplyAndQuickLinkLookup_MDL> removeHWQKs = HWQKs.Where(s => !HWQKIds.Contains(s.Id)).ToList();
                        if (removeHWQKs.Count() > 0)
                        {
                            _dBContext.BlockContentsHowToApplyAndQuickLinkLookup.RemoveRange(removeHWQKs);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add
                        //model.HowToApplyAndQuickLinkLookup = model.HowToApplyAndQuickLinkLookup.Where(s => s.Id >= 0 && !HWQKs.Select(s => s.Id).Contains(s.Id)).ToList();
                        List<BlockContentsHowToApplyAndQuickLinkLookup_MDL> HWQKs_MDL = new List<BlockContentsHowToApplyAndQuickLinkLookup_MDL>();
                        List<BlockContentsHowToApplyAndQuickLinkLookup_MDL> HWQKsUpdate_MDL = new List<BlockContentsHowToApplyAndQuickLinkLookup_MDL>();
                        foreach (var item in model.HowToApplyAndQuickLinkLookup.Where(s => s.Id >= 0))
                        {
                            item.BlockContentId = blockContents_.Id;
                            if (item.Id > 0)
                            {
                                BlockContentsHowToApplyAndQuickLinkLookup_MDL HWQKEntity = _mapper.Map<BlockContentsHowToApplyAndQuickLinkLookup_MDL>(item);
                                HWQKsUpdate_MDL.Add(HWQKEntity);
                            }
                            else
                            {
                                BlockContentsHowToApplyAndQuickLinkLookup_MDL HWQK = _mapper.Map<BlockContentsHowToApplyAndQuickLinkLookup_MDL>(item);
                                HWQKs_MDL.Add(HWQK);
                            }

                        }
                        if (HWQKsUpdate_MDL is not null && HWQKsUpdate_MDL.Count() > 0)
                        {
                            _dBContext.BlockContentsHowToApplyAndQuickLinkLookup.UpdateRange(HWQKsUpdate_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (HWQKs_MDL is not null && HWQKs_MDL.Count() > 0)
                        {
                            await _dBContext.BlockContentsHowToApplyAndQuickLinkLookup.AddRangeAsync(HWQKs_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        #region FAQ
                        List<FAQ_MDL> BCFaqs = await _dBContext.FAQ.Where(s => s.ModuleId == model.Id && s.BlockTypeId == model.BlockTypeId).ToListAsync();
                        // remove tags 
                        List<int> faqIds = model.FAQLookup.Where(s => s.Id > 0).Select(s => s.Id).ToList();
                        List<FAQ_MDL> removeFaqs = BCFaqs.Where(s => !faqIds.Contains(s.Id)).ToList();
                        if (removeFaqs.Count() > 0)
                        {
                            _dBContext.FAQ.RemoveRange(removeFaqs);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        //model.FAQLookup = model.FAQLookup.Where(s => s.Id >= 0 && !BCFaqs.Select(s => s.Id).Contains(s.Id)).ToList();
                        List<FAQ_MDL> faqUpdate_MDL = new List<FAQ_MDL>();
                        List<FAQ_MDL> faq_MDL = new List<FAQ_MDL>();
                        foreach (var item in model.FAQLookup.Where(s => s.Id >= 0))
                        {
                            item.ModuleId = blockContents_.Id;
                            item.BlockTypeId = blockContents_.BlockTypeId;
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
                        List<BlockContentsTags_MDL> BCTags = await _dBContext.blockContentsTags.Where(s => s.BlockContentId == model.Id).ToListAsync();
                        // remove tags 
                        List<int> tagsIds = model.BlockContentTags.Select(s => s).ToList();
                        List<BlockContentsTags_MDL> removetags = BCTags.Where(s => !tagsIds.Contains(s.TagsId)).ToList();
                        if (removetags.Count > 0)
                        {
                            _dBContext.blockContentsTags.RemoveRange(removetags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        List<BlockContentsTags_MDL> BCTags_MDL = new List<BlockContentsTags_MDL>();
                        model.BlockContentTags = model.BlockContentTags.Where(s => !BCTags.Select(s => s.TagsId).Contains(s)).ToList();
                        foreach (var item in model.BlockContentTags)
                        {
                            BlockContentsTags_MDL BCTags_ = new BlockContentsTags_MDL() { BlockContentId = blockContents_.Id, TagsId = item };
                            BCTags_MDL.Add(BCTags_);
                        }
                        if (BCTags_MDL is not null && BCTags_MDL.Count() > 0)
                        {
                            await _dBContext.blockContentsTags.AddRangeAsync(BCTags_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        await transaction.CommitAsync();
                        return SetResultStatus<int>(blockContents_.Id, MessageStatus.Update, true);
                    }
                    else
                    {
                        BlockContents_MDL entity = _mapper.Map<BlockContents_MDL>(model);
                        entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Status = (int)CommonEnum.ProgressStatus.unapproved;
                        await _dBContext.BlockContents.AddAsync(entity);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        // child tables
                        #region Document
                        List<BlockContentAttachmentLookup_MDL> bcAttachment_MDL = new List<BlockContentAttachmentLookup_MDL>();
                        foreach (var item in model.Documents)
                        {
                            BlockContentAttachmentLookup_MDL bcAttachment_ = new BlockContentAttachmentLookup_MDL() { BlockContentId = entity.Id, Path = item };
                            bcAttachment_MDL.Add(bcAttachment_);
                        }
                        if (bcAttachment_MDL is not null && bcAttachment_MDL.Count() > 0)
                        {
                            await _dBContext.BlockContentAttachmentLookup.AddRangeAsync(bcAttachment_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region HowToApplyAndQuickLink
                        List<BlockContentsHowToApplyAndQuickLinkLookup_MDL> HWQKs_MDL = new List<BlockContentsHowToApplyAndQuickLinkLookup_MDL>();
                        foreach (var item in model.HowToApplyAndQuickLinkLookup)
                        {
                            item.BlockContentId = entity.Id;
                            BlockContentsHowToApplyAndQuickLinkLookup_MDL HWQK = _mapper.Map<BlockContentsHowToApplyAndQuickLinkLookup_MDL>(item);
                            HWQKs_MDL.Add(HWQK);
                        }

                        if (HWQKs_MDL is not null && HWQKs_MDL.Count() > 0)
                        {
                            await _dBContext.BlockContentsHowToApplyAndQuickLinkLookup.AddRangeAsync(HWQKs_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        #region FAQ
                        List<FAQ_MDL> faq_MDL = new List<FAQ_MDL>();
                        foreach (var item in model.FAQLookup)
                        {
                            item.ModuleId = entity.Id;
                            item.BlockTypeId = entity.BlockTypeId;
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
                        if (model.BlockContentTags is not null && model.BlockContentTags.Count() > 0)
                        {
                            List<BlockContentsTags_MDL> BCTags_MDL = new List<BlockContentsTags_MDL>();
                            foreach (var item in model.BlockContentTags)
                            {
                                BlockContentsTags_MDL BCTags_ = new BlockContentsTags_MDL() { BlockContentId = entity.Id, TagsId = item };
                                BCTags_MDL.Add(BCTags_);
                            }
                            await _dBContext.blockContentsTags.AddRangeAsync(BCTags_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        await transaction.CommitAsync();
                        await _dBContext.DbContext.DisposeAsync();
                        return SetResultStatus<int>(entity.Id, MessageStatus.Save, true);
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _dBContext.DbContext.DisposeAsync();
                    Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "AddUpdate"));
                    return SetResultStatus<int>(0, MessageStatus.Error, false);
                }
            }
        }

        public ServiceResponse<BlockContentResponseDTO> GetById(int Id)
        {
            try
            {
                BlockContentResponseDTO model = new BlockContentResponseDTO();
                var result = QueryMultiple(@"SP_GetDetailOfBlockContentById @Id", new { Id = Id });
                if (result.Data is not null)
                {
                    model = SPResultHandler.GetObject<BlockContentResponseDTO>(result.Data[0]);
                    if (result.Data[1] is not null && SPResultHandler.GetCount(result.Data[1]) > 0)
                        model.Documents = SPResultHandler.GetList<BlockContentAttachmentRequestDTO>(result.Data[1]).Select(d => d.Path).ToList();
                    if (result.Data[2] is not null && SPResultHandler.GetCount(result.Data[2]) > 0)
                        model.HowToApplyAndQuickLinkLookup = SPResultHandler.GetList<HowToApplyAndQuickLinkResponseDTO>(result.Data[2]);
                    if (result.Data[3] is not null && SPResultHandler.GetCount(result.Data[3]) > 0)
                        model.FAQLookup = SPResultHandler.GetList<BlockContentFAQResponseDTO>(result.Data[3]);
                    if (result.Data[4] is not null && SPResultHandler.GetCount(result.Data[4]) > 0)
                        model.BlockContentTags = SPResultHandler.GetList<BlockContentsTagsResponseDTO>(result.Data[4]).Select(d => d.TagsId).ToList();
                    return SetResultStatus<BlockContentResponseDTO>(model, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<BlockContentResponseDTO>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "GetById"));
                return SetResultStatus<BlockContentResponseDTO>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<bool> Delete(int Id)
        {
            try
            {
                var deleteResult = ExecuteReturnData(@"Sp_BlockContentsDeleteUpdateStatus @Type='Delete',@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
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
                Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        //public ServiceResponse<BlockContentsModel> GetById(int Id)
        //{
        //    try
        //    {
        //        BlockContentsModel model = new BlockContentsModel();
        //        var result = QueryMultiple(@"SP_GetDetailOfBlockContentById @Id", new { Id = Id });
        //        if (result.Data is not null)
        //        {
        //            model = SPResultHandler.GetObject<BlockContentsModel>(result.Data[0]); ///JsonConvert.DeserializeObject<BlockContentsModel>(JsonConvert.SerializeObject(result.Data[0]));
        //            model.Documents = SPResultHandler.GetList<BlockContentAttachmentModel>(result.Data[1]).Select(d => d.Path).ToList(); //JsonConvert.DeserializeObject<List<BlockContentAttachmentModel>>(JsonConvert.SerializeObject(result.Data[1]));// result.Data[0] as List<SchemeDocumentLookupModel>;
        //            model.HowToApplyAndQuickLinkLookup = SPResultHandler.GetList<BlockContentHowToApplyAndQuickLinkLookup>(result.Data[2]);//JsonConvert.DeserializeObject<List<BlockContentHowToApplyAndQuickLinkLookup>>(JsonConvert.SerializeObject(result.Data[1]));// result.Data[0] as List<SchemeDocumentLookupModel>;
        //            model.FAQLookup = SPResultHandler.GetList<BlockContentFAQLookup>(result.Data[3]);
        //            return SetResultStatus<BlockContentsModel>(model, MessageStatus.Success, true);
        //        }
        //        else
        //        {
        //            return SetResultStatus<BlockContentsModel>(null, MessageStatus.NoRecord, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "GetById"));
        //        return SetResultStatus<BlockContentsModel>(null, MessageStatus.Error, false, ex.Message);
        //    }
        //}

        public ServiceResponse<List<BlockContentsViewModel>> GetList()
        {
            throw new NotImplementedException();
        }
        public ServiceResponse<List<BlockContentsViewModel>> GetPagination(BlockContentsFilterModel filterModel)
        {
            try
            {
                filterModel.UserId = _jWTAuthManager.User.Id;
                var result = QueryList<BlockContentsViewModel>(@"Sp_BlockContentsPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@SlugUrl,@BlockTypeId,@RecruitmentId,@DepartmentId,@CategoryId,@Status,@GroupId,@SubCategoryId,@UserId", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<BlockContentsViewModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<BlockContentsViewModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "GetPagination"));
                return SetResultStatus<List<BlockContentsViewModel>>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<List<BlockContentsTitleCheckModel>> CheckBlockContentTitle(string SearchText)
        {
            ServiceResponse<List<BlockContentsTitleCheckModel>> serviceResponse = new ServiceResponse<List<BlockContentsTitleCheckModel>>();
            List<BlockContentsTitleCheckModel> result = new List<BlockContentsTitleCheckModel>();
            try
            {
                serviceResponse.Data = QueryList<BlockContentsTitleCheckModel>(@"Sp_CheckBlockContentsTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "CheckBlockContentTitle"));
                return null;
            }
        }

        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            try
            {
                var result = ExecuteReturnData(@"Sp_BlockContentsDeleteUpdateStatus @Type='Status',@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + "", null);
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                        return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
                    else if (result.StatusCode == StatusCodes.Status404NotFound)
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
                Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<bool> BlockContentProgressStatus(int Id, int ProgressStatus)
        {
            try
            {
                if (Id > 0 && ProgressStatus > 0)
                {
                    var getBCData = QueryFast<BlockContentsViewModel>(@"select Title,* from Vw_BlockContents where Id=@Id", new { Id = Id });
                    if (getBCData.Data != null)
                    {
                        //string Deeplink = string.Empty;
                        //ResponseDeepLinkModel res = new ResponseDeepLinkModel();
                        //if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                        //{
                        //    DeeplinkModel link = new DeeplinkModel()
                        //    {
                        //        isRecruitment = false,
                        //        moduleName = getBCData.Data.ModuleSlug,
                        //        type = "post",
                        //        slugUrl = getBCData.Data.SlugUrl ?? "",
                        //        id = (int)getBCData.Data.Id,
                        //        thumbnail = !string.IsNullOrEmpty(getBCData.Data.Logo) ? getBCData.Data.Logo : string.Empty,
                        //        title = getBCData.Data.Title
                        //    };
                        //    Deeplink = JsonConvert.SerializeObject(link);
                        //    _fairBaseHelper.PostPushNotification(link, link.title, getBCData.Data.CategoryName);
                        //    var getDeeplink = Deeplink.GenerateDeeplink(link.title, getBCData.Data.Summary, link.thumbnail);
                        //    var deeplink = _fairBaseHelper.Post(getDeeplink);
                        //    if (!string.IsNullOrEmpty(deeplink))
                        //    {
                        //        res = JsonConvert.DeserializeObject<ResponseDeepLinkModel>(deeplink);
                        //    }
                        //}
                        var result = ExecuteReturnData(@"Sp_BlockContentsDeleteUpdateStatus @Type='ProgressStatus',@StatusCode=" + ProgressStatus + ",@Id=" + Id + ",@UserId=" + _jWTAuthManager.User.Id + ",@SortLinks='" + null + "'", null);
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
                Log.Error(ex, CommonFunction.Errorstring("BlockContentsService.cs", "BlockContentProgressStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }
    }
}
