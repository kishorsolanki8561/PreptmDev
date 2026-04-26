using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using DBInfrastructure;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using ModelService.Model.Translation.Article;
using ModelService.Model.Translation.Paper;
using Serilog;
using System.Collections;
using System.Collections.Generic;

namespace TranslationMicroService.Service.Paper
{
    public class PaperService : UtilityManager, IPaperService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IDBPreptmContext _dBContext;
        private readonly IMapper _mapper;

        public PaperService(HelperService helperService, JWTAuthManager jWTAuthManager, IDBPreptmContext dBContext, IMapper mapper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> AddUpdate(PaperRequestDTO model)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            using (var transaction = await _dBContext.DbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    Paper_MDL _paper = await _dBContext.Papers.Include(p => p.PaperTags).Include(p => p.Paper_Subjects).Where(s => !s.IsDelete && ((s.Id == model.Id && model.Id > 0)
                                               || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id == 0)
                                               || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id != 0))).FirstOrDefaultAsync();

                    if (model.Id > 0 && _paper is null)
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.NotExist, false);
                    }

                    else if (!string.IsNullOrEmpty(model.SlugUrl) && (_paper is not null && _paper.SlugUrl == model.SlugUrl && model.Id == 0) || (_paper is not null && _paper.SlugUrl == model.SlugUrl && _paper.Id != model.Id))
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.SlugUrlExist, false);
                    }
                    if (model.Id > 0 && _paper is not null)
                    {
                        _paper = _mapper.Map<PaperRequestDTO, Paper_MDL>(model, _paper, opts => opts.BeforeMap((src, des) => { if (des.Status == (int)CommonEnum.ProgressStatus.published) { src.SlugUrl = des.SlugUrl; } }));
                        _dBContext.Papers.Update(_paper);
                        _paper.ModifiedBy = _jWTAuthManager.User.Id;
                        //await _dBContext.SaveChangesAsync(CancellationToken.None);

                        #region Tags
                        // remove tags 
                        List<int> tagsIds = model.PaperTags.Select(s => s).ToList();
                        List<PaperTags_MDL> removetags = _paper.PaperTags.Where(s => !tagsIds.Contains(s.TagId)).ToList();
                        if (removetags.Count > 0)
                        {
                            _dBContext.PaperTags.RemoveRange(removetags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        model.PaperTags = model.PaperTags.Where(s => !_paper.PaperTags.Select(s => s.TagId).Contains(s)).ToList();

                        List<PaperTags_MDL> paperTags_ = model.PaperTags.Select(s => new PaperTags_MDL()
                        {
                            PaperId = _paper.Id,
                            TagId = s,
                        }).ToList();

                        if (paperTags_ is not null && paperTags_.Count() > 0)
                        {
                            await _dBContext.PaperTags.AddRangeAsync(paperTags_);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Subject
                        List<int> subjecctIds = model.PapperSubjects.Select(s => s.Id).ToList();
                        List<PaperSubject_MDL> removeSubject = _paper.Paper_Subjects.Where(s => !subjecctIds.Contains(s.Id)).ToList();
                        if (removeSubject.Count() > 0)
                        {
                            _dBContext.PaperSubject.RemoveRange(removeSubject);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (model.PapperSubjects.Any(s => s.Id > 0))
                        {
                            foreach (var item in _paper.Paper_Subjects)
                            {
                                var subject = model.PapperSubjects.Where(s => s.Id == item.Id).FirstOrDefault();
                                if (_helperService.GetDifferences<PapperSubjectRequestDTO>(_mapper.Map<PapperSubjectRequestDTO>(item), subject))
                                {
                                    item.SubjectName = subject.SubjectName;
                                    item.Path = subject.Path;
                                    item.SubjectNameHindi = subject.SubjectNameHindi;
                                    item.YearId = subject.YearId;
                                    item.ModifiedDate = DateTime.Now;
                                    item.ModifiedBy = _jWTAuthManager.User.Id;
                                }
                            }
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (model.PapperSubjects.Any(s => s.Id == 0))
                        {
                            List<PaperSubject_MDL> _addSubject = _mapper.Map<List<PaperSubject_MDL>>(model.PapperSubjects.Where(s => s.Id == 0).ToList());
                            foreach (var item in _addSubject)
                            {
                                item.PaperId = _paper.Id;
                                item.CreatedBy = _jWTAuthManager.User.Id;
                                item.CreatedDate = new DateTime().Date;
                            }
                            await _dBContext.PaperSubject.AddRangeAsync(_addSubject);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        await transaction.CommitAsync();
                        return SetResultStatus<int>(_paper.Id, MessageStatus.Update, true);
                    }
                    else
                    {
                        Paper_MDL entity = _mapper.Map<Paper_MDL>(model);
                        entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Status = (int)CommonEnum.ProgressStatus.unapproved;
                        await _dBContext.Papers.AddAsync(entity);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        #region Tags
                        if (model.PaperTags is not null && model.PaperTags.Count() > 0)
                        {
                            List<PaperTags_MDL> _Tags = new List<PaperTags_MDL>();
                            foreach (var item in model.PaperTags)
                            {
                                PaperTags_MDL articleTags_ = new PaperTags_MDL() { PaperId = entity.Id, TagId = item };
                                _Tags.Add(articleTags_);
                            }
                            await _dBContext.PaperTags.AddRangeAsync(_Tags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion
                        #region Subject
                        if (model.PapperSubjects is not null && model.PapperSubjects.Count() > 0)
                        {
                            List<PaperSubject_MDL> _Subject = new List<PaperSubject_MDL>();
                            foreach (var item in model.PapperSubjects)
                            {
                                item.PaperId = entity.Id;
                                PaperSubject_MDL faq = _mapper.Map<PaperSubject_MDL>(item);
                                _Subject.Add(faq);
                            }
                            await _dBContext.PaperSubject.AddRangeAsync(_Subject);
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
                    Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "AddUpdate"));
                    return SetResultStatus<int>(0, MessageStatus.Error, false);
                }
            }
        }

        public ServiceResponse<List<PapperTitleCheckDTO>> CheckArticleTitle(string SearchText)
        {
            ServiceResponse<List<PapperTitleCheckDTO>> serviceResponse = new ServiceResponse<List<PapperTitleCheckDTO>>();
            List<PapperTitleCheckDTO> result = new List<PapperTitleCheckDTO>();
            try
            {
                serviceResponse.Data = QueryList<PapperTitleCheckDTO>(@"Sp_CheckPaperTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "CheckArticleTitle"));
                return null;
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
                Paper_MDL _paper = await _dBContext.Papers.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_paper == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                _paper.IsDelete = true;
                _paper.IsActive = true;
                _paper.ModifiedDate = DateTime.Now;
                _paper.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Papers.Update(_paper);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.Delete, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }

        public async Task<ServiceResponse<PaperResponseDTO>> GetById(int Id)
        {
            ServiceResponse<PaperResponseDTO> _response = new();
            PaperResponseDTO _paperResult = new();

            try
            {
                if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
                {
                    _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                }
                Paper_MDL _paper = await _dBContext.Papers.Include(p => p.PaperTags).Include(p => p.Paper_Subjects).Where(s => s.Id == Id && s.IsActive && !s.IsDelete).FirstOrDefaultAsync();
                if (_paper is null)
                {
                    return SetResultStatus<PaperResponseDTO>(null, MessageStatus.NoRecord, true);

                }
                _paperResult = _mapper.Map<PaperResponseDTO>(_paper);
                if (_paper.PaperTags.Count > 0)
                {
                    _paperResult.PaperTags = _paper.PaperTags.Select(s => s.TagId).ToList();
                }
                if (_paper.Paper_Subjects.Count > 0)
                {
                    _paperResult.PapperSubjects = _mapper.Map<List<PapperSubjectResponseDTO>>(_paper.Paper_Subjects);
                }
                return SetResultStatus<PaperResponseDTO>(_paperResult, MessageStatus.Success, true);

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "GetById"));
                return SetResultStatus<PaperResponseDTO>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<List<PapperViewListDTO>> GetPagination(PaperFilterDTO filterModel)
        {
            try
            {
                var result = QueryList<PapperViewListDTO>(@"Sp_PaperPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@FromDate,@ToDate,@Title,@TitleHindi", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<PapperViewListDTO>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<PapperViewListDTO>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "GetPagination"));
                return SetResultStatus<List<PapperViewListDTO>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
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
                    Paper_MDL _paper = await _dBContext.Papers.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                    if (_paper == null)
                    {
                        return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                    }
                    if (ProgressStatus == (int)CommonEnum.ProgressStatus.published || ProgressStatus == (int)CommonEnum.ProgressStatus.unapproved || ProgressStatus == (int)CommonEnum.ProgressStatus.approved)
                    {
                        _paper.Status = ProgressStatus;
                        if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                        {
                            _paper.PublisherDate = DateTime.Now;
                            _paper.PublisherId = _jWTAuthManager.User.Id;
                        }
                        _dBContext.Papers.Update(_paper);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);
                        await _dBContext.DbContext.DisposeAsync();
                        return SetResultStatus<bool>(true, MessageStatus.StatusProgressUpdate, true);
                    }
                    return SetResultStatus<bool>(false, MessageStatus.StatusProgressNotValidCodeUpdate, false);
                }
                catch (Exception ex)
                {
                    await _dBContext.DbContext.DisposeAsync();
                    Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "ProgressStatus"));
                    return SetResultStatus<bool>(false, MessageStatus.Error, false);
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
                Paper_MDL _paper = await _dBContext.Papers.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_paper == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                _paper.IsActive = !_paper.IsActive;
                _paper.ModifiedDate = DateTime.Now;
                _paper.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Papers.Update(_paper);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("PaperService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
