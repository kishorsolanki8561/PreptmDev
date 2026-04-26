using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using DBInfrastructure;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using ModelService.Model;
using ModelService.Model.Translation;
using ModelService.Model.Translation.Article;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TranslationMicroService.IService;

namespace TranslationMicroService.Service
{
    public class ArticleService : UtilityManager, IArticleService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IDBPreptmContext _dBContext;
        private readonly IMapper _mapper;

        public ArticleService(HelperService helperService, JWTAuthManager jWTAuthManager, IDBPreptmContext dBContext, IMapper mapper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> AddUpdate(ArticleRequestDTO model)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            using (var transaction = await _dBContext.DbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    Article_MDL article_ = await _dBContext.Article.Where(s => !s.IsDelete && ((s.Id == model.Id && model.Id > 0)
                                                || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id == 0)
                                                || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id != 0))).FirstOrDefaultAsync();

                    if (model.Id > 0 && article_ is null)
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.NotExist, false);
                    }

                    else if (!string.IsNullOrEmpty(model.SlugUrl) && (article_ is not null && article_.SlugUrl == model.SlugUrl && model.Id == 0) || (article_ is not null && article_.SlugUrl == model.SlugUrl && article_.Id != model.Id))
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.SlugUrlExist, false);
                    }

                    if (model.Id > 0 && article_ is not null)
                    {
                        article_ = _mapper.Map<ArticleRequestDTO, Article_MDL>(model, article_, opts => opts.BeforeMap((src, des) => { if (des.Status == (int)CommonEnum.ProgressStatus.published) { src.SlugUrl = des.SlugUrl; } }));
                        _dBContext.Article.Update(article_);
                        article_.ModifiedBy = _jWTAuthManager.User.Id;
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        #region Tags
                        List<ArticleTags_MDL> articleTags = await _dBContext.ArticleTags.Where(s => s.ArticleId == model.Id).ToListAsync();
                        // remove tags 
                        List<int> tagsIds = model.ArticleTagsDTOs.Select(s => s).ToList();
                        List<ArticleTags_MDL> removetags = articleTags.Where(s => !tagsIds.Contains(s.TagsId)).ToList();
                        if (removetags.Count > 0)
                        {
                            _dBContext.ArticleTags.RemoveRange(removetags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        List<ArticleTags_MDL> articleTags_MDL = new List<ArticleTags_MDL>();
                        model.ArticleTagsDTOs = model.ArticleTagsDTOs.Where(s => !articleTags.Select(s => s.TagsId).Contains(s)).ToList();
                        foreach (var item in model.ArticleTagsDTOs)
                        {
                            ArticleTags_MDL articleTags_ = new ArticleTags_MDL() { ArticleId = article_.Id, TagsId = item };
                            articleTags_MDL.Add(articleTags_);
                        }
                        if (articleTags_MDL is not null && articleTags_MDL.Count() > 0)
                        {
                            await _dBContext.ArticleTags.AddRangeAsync(articleTags_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region FAQ
                        List<ArticleFaq_MDL> articleFaqs = await _dBContext.ArticleFaqs.Where(s => s.ArticleId == model.Id).ToListAsync();
                        // remove tags 
                        List<int> faqIds = model.ArticleFaqsDTOs.Select(s => s.Id).ToList();
                        List<ArticleFaq_MDL> removeFaqs = articleFaqs.Where(s => !faqIds.Contains(s.Id)).ToList();
                        if (removeFaqs.Count() > 0)
                        {
                            _dBContext.ArticleFaqs.RemoveRange(removeFaqs);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags

                        List<ArticleFaq_MDL> faqUpdate_MDL = new List<ArticleFaq_MDL>();
                        List<ArticleFaq_MDL> faq_MDL = new List<ArticleFaq_MDL>();
                        foreach (var item in model.ArticleFaqsDTOs)
                        {
                            item.ArticleId = article_.Id;
                            if (item.Id > 0)
                            {
                                ArticleFaq_MDL faqUpdateEntity = _mapper.Map<ArticleFaq_MDL>(item);
                                faqUpdate_MDL.Add(faqUpdateEntity);
                            }
                            else
                            {
                                ArticleFaq_MDL faq = _mapper.Map<ArticleFaq_MDL>(item);
                                faq_MDL.Add(faq);
                            }
                        }

                        if (faq_MDL is not null && faq_MDL.Count() > 0)
                        {
                            await _dBContext.ArticleFaqs.AddRangeAsync(faq_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        if (faqUpdate_MDL is not null && faqUpdate_MDL.Count() > 0)
                        {
                            _dBContext.ArticleFaqs.UpdateRange(faqUpdate_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion
                        await transaction.CommitAsync();
                        return SetResultStatus<int>(article_.Id, MessageStatus.Update, true);
                    }
                    else
                    {
                        Article_MDL entity = _mapper.Map<Article_MDL>(model);
                        entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Status = (int)CommonEnum.ProgressStatus.unapproved;
                        await _dBContext.Article.AddAsync(entity);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        #region Tags
                        if (model.ArticleTagsDTOs is not null && model.ArticleTagsDTOs.Count() > 0)
                        {
                            List<ArticleTags_MDL> articleTags_MDL = new List<ArticleTags_MDL>();
                            foreach (var item in model.ArticleTagsDTOs)
                            {
                                ArticleTags_MDL articleTags_ = new ArticleTags_MDL() { ArticleId = entity.Id, TagsId = item };
                                articleTags_MDL.Add(articleTags_);
                            }
                            await _dBContext.ArticleTags.AddRangeAsync(articleTags_MDL);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region FAQ
                        if (model.ArticleFaqsDTOs is not null && model.ArticleFaqsDTOs.Count() > 0)
                        {
                            List<ArticleFaq_MDL> faq_MDL = new List<ArticleFaq_MDL>();
                            foreach (var item in model.ArticleFaqsDTOs)
                            {
                                item.ArticleId = entity.Id;
                                ArticleFaq_MDL faq = _mapper.Map<ArticleFaq_MDL>(item);
                                faq_MDL.Add(faq);
                            }
                            await _dBContext.ArticleFaqs.AddRangeAsync(faq_MDL);
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
                    Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "AddUpdate"));
                    return SetResultStatus<int>(0, MessageStatus.Error, false);
                }
            }
        }

        public async Task<ServiceResponse<bool>> ProgressStatus(int Id, int ProgressStatus)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                Article_MDL article_ = await _dBContext.Article.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (article_ == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                if (ProgressStatus == (int)CommonEnum.ProgressStatus.published || ProgressStatus == (int)CommonEnum.ProgressStatus.unapproved || ProgressStatus == (int)CommonEnum.ProgressStatus.approved)
                {
                    article_.Status = ProgressStatus;
                    if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                    {
                        article_.PublisherDate = DateTime.Now;
                        article_.PublisherId = _jWTAuthManager.User.Id;
                    }
                    _dBContext.Article.Update(article_);
                    await _dBContext.SaveChangesAsync(CancellationToken.None);
                    await _dBContext.DbContext.DisposeAsync();
                    return SetResultStatus<bool>(true, MessageStatus.StatusProgressUpdate, true);
                }
                return SetResultStatus<bool>(false, MessageStatus.StatusProgressNotValidCodeUpdate, false);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "ProgressStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false);
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int Id)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                Article_MDL article_ = await _dBContext.Article.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (article_ == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                article_.IsDelete = true;
                article_.IsActive = true;
                article_.ModifiedDate = DateTime.Now;
                article_.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Article.Update(article_);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.Delete, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> UpdateStatus(int Id)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            try
            {
                Article_MDL article_ = await _dBContext.Article.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (article_ == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                article_.IsActive = !article_.IsActive;
                article_.ModifiedDate = DateTime.Now;
                article_.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Article.Update(article_);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<ArticleResponseDTO> GetById(int Id)
        {
            ArticleResponseDTO resultModel = new ArticleResponseDTO();
            try
            {
                var result = QueryMultiple(@"SP_GetDetailOfArticleById @Id", new { Id = Id });

                if (result.Data is not null && SPResultHandler.GetCount(result.Data[0]) > 0)
                {
                    resultModel = SPResultHandler.GetObject<ArticleResponseDTO>(result.Data[0]);
                    if (SPResultHandler.GetCount(result.Data[1]) > 0)
                        resultModel.ArticleTagsDTOs = SPResultHandler.GetList<ArticleTagsResponseDTO>(result.Data[1]).Select(s => s.TagsId).ToList();
                    if (SPResultHandler.GetCount(result.Data[2]) > 0)
                        resultModel.ArticleFaqsDTOs = SPResultHandler.GetList<ArticleFaqResponseDTO>(result.Data[2]).ToList();


                    return SetResultStatus<ArticleResponseDTO>(resultModel, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<ArticleResponseDTO>(null, MessageStatus.NoRecord, true);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "GetById"));
                return SetResultStatus<ArticleResponseDTO>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<ArticleTitleCheckModel>> CheckArticleTitle(string SearchText)
        {
            ServiceResponse<List<ArticleTitleCheckModel>> serviceResponse = new ServiceResponse<List<ArticleTitleCheckModel>>();
            List<ArticleTitleCheckModel> result = new List<ArticleTitleCheckModel>();
            try
            {
                serviceResponse.Data = QueryList<ArticleTitleCheckModel>(@"Sp_CheckArticleTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "CheckArticleTitle"));
                return null;
            }
        }

        public ServiceResponse<List<ArticleViewListModel>> GetPagination(ArticleFilterModel filterModel)
        {
            try
            {
                var result = QueryList<ArticleViewListModel>(@"Sp_ArticlePagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@TitleHindi", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<ArticleViewListModel>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<ArticleViewListModel>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("ArticleService.cs", "GetPagination"));
                return SetResultStatus<List<ArticleViewListModel>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }
    }
}
