using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using DBInfrastructure;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using ModelService.Model.Translation.Note;
using ModelService.Model.Translation.Syllabus;
using Serilog;

namespace TranslationMicroService.Service.Syllabus
{
    public class SyllabusService :UtilityManager, ISyllabusService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IDBPreptmContext _dBContext;
        private readonly IMapper _mapper;
        public SyllabusService(HelperService helperService, JWTAuthManager jWTAuthManager, IDBPreptmContext dBContext, IMapper mapper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> AddUpdate(SyllabusRequestDTO model)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            using (var transaction = await _dBContext.DbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    Syllabus_MDL _syllabus = await _dBContext.Syllabus.Include(p => p.SyllabusTags).Include(p => p.SyllabusTags).Where(s => !s.IsDelete && ((s.Id == model.Id && model.Id > 0)
                                               || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id == 0)
                                               || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id != 0))).FirstOrDefaultAsync();

                    if (model.Id > 0 && _syllabus is null)
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.NotExist, false);
                    }

                    else if (!string.IsNullOrEmpty(model.SlugUrl) && (_syllabus is not null && _syllabus.SlugUrl == model.SlugUrl && model.Id == 0) || (_syllabus is not null && _syllabus.SlugUrl == model.SlugUrl && _syllabus.Id != model.Id))
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.SlugUrlExist, false);
                    }
                    if (model.Id > 0 && _syllabus is not null)
                    {
                        _syllabus = _mapper.Map<SyllabusRequestDTO, Syllabus_MDL>(model, _syllabus, opts => opts.BeforeMap((src, des) => { if (des.Status == (int)CommonEnum.ProgressStatus.published) { src.SlugUrl = des.SlugUrl; } }));
                        _dBContext.Syllabus.Update(_syllabus);
                        _syllabus.ModifiedBy = _jWTAuthManager.User.Id;

                        #region Tags
                        // remove tags 
                        List<int> tagsIds = model.SyllabusTags.Select(s => s).ToList();
                        List<SyllabusTags_MDL> removetags = _syllabus.SyllabusTags.Where(s => !tagsIds.Contains(s.TagId)).ToList();
                        if (removetags.Count > 0)
                        {
                            _dBContext.SyllabusTags.RemoveRange(removetags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        model.SyllabusTags = model.SyllabusTags.Where(s => !_syllabus.SyllabusTags.Select(s => s.TagId).Contains(s)).ToList();

                        List<SyllabusTags_MDL> syllabusTags_ = model.SyllabusTags.Select(s => new SyllabusTags_MDL()
                        {
                            SyllabusId = _syllabus.Id,
                            TagId = s,
                        }).ToList();

                        if (syllabusTags_ is not null && syllabusTags_.Count() > 0)
                        {
                            await _dBContext.SyllabusTags.AddRangeAsync(syllabusTags_);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Subject
                        List<int> subjecctIds = model.Syllabus_Subjects.Select(s => s.Id).ToList();
                        List<Syllabus_Subject_MDL> removeSubject = _syllabus.Syllabus_Subjects.Where(s => !subjecctIds.Contains(s.Id)).ToList();
                        if (removeSubject.Count() > 0)
                        {
                            _dBContext.SyllabusSubject.RemoveRange(removeSubject);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (model.Syllabus_Subjects.Any(s => s.Id > 0))
                        {
                            foreach (var item in _syllabus.Syllabus_Subjects)
                            {
                                var subject = model.Syllabus_Subjects.Where(s => s.Id == item.Id).FirstOrDefault();
                                if (_helperService.GetDifferences<SyllabusSubjectRequestDTO>(_mapper.Map<SyllabusSubjectRequestDTO>(item), subject))
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

                        if (model.Syllabus_Subjects.Any(s => s.Id == 0))
                        {
                            List<Syllabus_Subject_MDL> _addSubject = _mapper.Map<List<Syllabus_Subject_MDL>>(model.Syllabus_Subjects.Where(s => s.Id == 0).ToList());
                            foreach (var item in _addSubject)
                            {
                                item.SyllabusId = _syllabus.Id;
                                item.CreatedBy = _jWTAuthManager.User.Id;
                                item.CreatedDate = new DateTime().Date;
                            }
                            await _dBContext.SyllabusSubject.AddRangeAsync(_addSubject);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        await transaction.CommitAsync();
                        return SetResultStatus<int>(_syllabus.Id, MessageStatus.Update, true);
                    }
                    else
                    {
                        Syllabus_MDL entity = _mapper.Map<Syllabus_MDL>(model);
                        entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Status = (int)CommonEnum.ProgressStatus.unapproved;
                        await _dBContext.Syllabus.AddAsync(entity);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        #region Tags
                        if (model.SyllabusTags is not null && model.SyllabusTags.Count() > 0)
                        {
                            List<SyllabusTags_MDL> _Tags = new List<SyllabusTags_MDL>();
                            foreach (var item in model.SyllabusTags)
                            {
                                SyllabusTags_MDL syllabusTags_ = new SyllabusTags_MDL() { SyllabusId = entity.Id, TagId = item };
                                _Tags.Add(syllabusTags_);
                            }
                            await _dBContext.SyllabusTags.AddRangeAsync(_Tags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion
                        #region Subject
                        if (model.Syllabus_Subjects is not null && model.Syllabus_Subjects.Count() > 0)
                        {
                            List<Syllabus_Subject_MDL> _Subject = new List<Syllabus_Subject_MDL>();
                            foreach (var item in model.Syllabus_Subjects)
                            {
                                item.SyllabusId = entity.Id;
                                Syllabus_Subject_MDL faq = _mapper.Map<Syllabus_Subject_MDL>(item);
                                _Subject.Add(faq);
                            }
                            await _dBContext.SyllabusSubject.AddRangeAsync(_Subject);
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
                    Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "AddUpdate"));
                    return SetResultStatus<int>(0, MessageStatus.Error, false);
                }
            }
        }

        public ServiceResponse<List<SyllabusTitleCheckDTO>> CheckArticleTitle(string SearchText)
        {
            ServiceResponse<List<SyllabusTitleCheckDTO>> serviceResponse = new ServiceResponse<List<SyllabusTitleCheckDTO>>();
            List<SyllabusTitleCheckDTO> result = new List<SyllabusTitleCheckDTO>();
            try
            {
                serviceResponse.Data = QueryList<SyllabusTitleCheckDTO>(@"Sp_CheckSyllabusTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "CheckArticleTitle"));
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
                Syllabus_MDL _syllabus = await _dBContext.Syllabus.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_syllabus == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                _syllabus.IsDelete = true;
                _syllabus.IsActive = true;
                _syllabus.ModifiedDate = DateTime.Now;
                _syllabus.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Syllabus.Update(_syllabus);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.Delete, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }

        public async Task<ServiceResponse<SyllabusResponseDTO>> GetById(int Id)
        {
            ServiceResponse<SyllabusResponseDTO> _response = new();
            SyllabusResponseDTO _noteResult = new();

            try
            {
                if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
                {
                    _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                }
                Syllabus_MDL _syllabus = await _dBContext.Syllabus.Include(p => p.SyllabusTags).Include(p => p.Syllabus_Subjects).Where(s => s.Id == Id && s.IsActive && !s.IsDelete).FirstOrDefaultAsync();
                if (_syllabus is null)
                {
                    return SetResultStatus<SyllabusResponseDTO>(null, MessageStatus.NoRecord, true);

                }
                _noteResult = _mapper.Map<SyllabusResponseDTO>(_syllabus);
                if (_syllabus.SyllabusTags.Count > 0)
                {
                    _noteResult.SyllabusTags = _syllabus.SyllabusTags.Select(s => s.TagId).ToList();
                }
                if (_syllabus.Syllabus_Subjects.Count > 0)
                {
                    _noteResult.Syllabus_Subjects = _mapper.Map<List<SyllabusSubjecResponseDTO>>(_syllabus.Syllabus_Subjects);
                }
                return SetResultStatus<SyllabusResponseDTO>(_noteResult, MessageStatus.Success, true);

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "GetById"));
                return SetResultStatus<SyllabusResponseDTO>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<List<SyllabusViewListDTO>> GetPagination(SyllabusFilterDTO filterModel)
        {
            try
            {
                var result = QueryList<SyllabusViewListDTO>(@"Sp_SyllabusPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@FromDate,@ToDate,@Title,@TitleHindi", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<SyllabusViewListDTO>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<SyllabusViewListDTO>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "GetPagination"));
                return SetResultStatus<List<SyllabusViewListDTO>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
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
                Syllabus_MDL _syllabus = await _dBContext.Syllabus.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_syllabus == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                if (ProgressStatus == (int)CommonEnum.ProgressStatus.published || ProgressStatus == (int)CommonEnum.ProgressStatus.unapproved || ProgressStatus == (int)CommonEnum.ProgressStatus.approved)
                {
                    _syllabus.Status = ProgressStatus;
                    if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                    {
                        _syllabus.PublisherDate = DateTime.Now;
                        _syllabus.PublisherId = _jWTAuthManager.User.Id;
                    }
                    _dBContext.Syllabus.Update(_syllabus);
                    await _dBContext.SaveChangesAsync(CancellationToken.None);
                    await _dBContext.DbContext.DisposeAsync();
                    return SetResultStatus<bool>(true, MessageStatus.StatusProgressUpdate, true);
                }
                return SetResultStatus<bool>(false, MessageStatus.StatusProgressNotValidCodeUpdate, false);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "ProgressStatus"));
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
                Syllabus_MDL _syllabus = await _dBContext.Syllabus.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_syllabus == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                _syllabus.IsActive = !_syllabus.IsActive;
                _syllabus.ModifiedDate = DateTime.Now;
                _syllabus.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Syllabus.Update(_syllabus);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("SyllabusService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
