using AutoMapper;
using CommonService.JWT;
using CommonService.Other;
using DBInfrastructure;
using Microsoft.EntityFrameworkCore;
using ModelService.MDL.Translation;
using ModelService.Model.Translation.Note;
using ModelService.Model.Translation.Paper;
using Serilog;

namespace TranslationMicroService.Service.Note
{
    public class NoteService : UtilityManager, INoteService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        private readonly IDBPreptmContext _dBContext;
        private readonly IMapper _mapper;
        public NoteService(HelperService helperService, JWTAuthManager jWTAuthManager, IDBPreptmContext dBContext, IMapper mapper)
        {
            _jWTAuthManager = jWTAuthManager;
            _helperService = helperService;
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> AddUpdate(NoteRequestDTO model)
        {
            if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
            {
                _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            using (var transaction = await _dBContext.DbContext.Database.BeginTransactionAsync(CancellationToken.None))
            {
                try
                {
                    Note_MDL _notes = await _dBContext.Notes.Include(p => p.Note_Tags).Include(p => p.Note_Subjects).Where(s => !s.IsDelete && ((s.Id == model.Id && model.Id > 0)
                                               || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id == 0)
                                               || (!string.IsNullOrEmpty(model.SlugUrl) && s.SlugUrl == model.SlugUrl && model.Id != 0))).FirstOrDefaultAsync();

                    if (model.Id > 0 && _notes is null)
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.NotExist, false);
                    }

                    else if (!string.IsNullOrEmpty(model.SlugUrl) && (_notes is not null && _notes.SlugUrl == model.SlugUrl && model.Id == 0) || (_notes is not null && _notes.SlugUrl == model.SlugUrl && _notes.Id != model.Id))
                    {
                        return SetResultStatus<int>(model.Id, MessageStatus.SlugUrlExist, false);
                    }
                    if (model.Id > 0 && _notes is not null)
                    {
                        _notes = _mapper.Map<NoteRequestDTO, Note_MDL>(model, _notes, opts => opts.BeforeMap((src, des) => { if (des.Status == (int)CommonEnum.ProgressStatus.published) { src.SlugUrl = des.SlugUrl; } }));
                        _dBContext.Notes.Update(_notes);
                        _notes.ModifiedBy = _jWTAuthManager.User.Id;

                        #region Tags
                        // remove tags 
                        List<int> tagsIds = model.Note_Tags.Select(s => s).ToList();
                        List<NoteTags_MDL> removetags = _notes.Note_Tags.Where(s => !tagsIds.Contains(s.TagId)).ToList();
                        if (removetags.Count > 0)
                        {
                            _dBContext.NoteTags.RemoveRange(removetags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        //add tags
                        model.Note_Tags = model.Note_Tags.Where(s => !_notes.Note_Tags.Select(s => s.TagId).Contains(s)).ToList();

                        List<NoteTags_MDL> noteTags_ = model.Note_Tags.Select(s => new NoteTags_MDL()
                        {
                            NoteId = _notes.Id,
                            TagId = s,
                        }).ToList();

                        if (noteTags_ is not null && noteTags_.Count() > 0)
                        {
                            await _dBContext.NoteTags.AddRangeAsync(noteTags_);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion

                        #region Subject
                        List<int> subjecctIds = model.Note_Subjects.Select(s => s.Id).ToList();
                        List<Note_Subject_MDL> removeSubject = _notes.Note_Subjects.Where(s => !subjecctIds.Contains(s.Id)).ToList();
                        if (removeSubject.Count() > 0)
                        {
                            _dBContext.NoteSubject.RemoveRange(removeSubject);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        if (model.Note_Subjects.Any(s => s.Id > 0))
                        {
                            foreach (var item in _notes.Note_Subjects)
                            {
                                var subject = model.Note_Subjects.Where(s => s.Id == item.Id).FirstOrDefault();
                                if (_helperService.GetDifferences<NoteSubjectRequestDTO>(_mapper.Map<NoteSubjectRequestDTO>(item), subject))
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

                        if (model.Note_Subjects.Any(s => s.Id == 0))
                        {
                            List<Note_Subject_MDL> _addSubject = _mapper.Map<List<Note_Subject_MDL>>(model.Note_Subjects.Where(s => s.Id == 0).ToList());
                            foreach (var item in _addSubject)
                            {
                                item.NoteId = _notes.Id;
                                item.CreatedBy = _jWTAuthManager.User.Id;
                                item.CreatedDate = new DateTime().Date;
                            }
                            await _dBContext.NoteSubject.AddRangeAsync(_addSubject);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }

                        #endregion

                        await transaction.CommitAsync();
                        return SetResultStatus<int>(_notes.Id, MessageStatus.Update, true);
                    }
                    else
                    {
                        Note_MDL entity = _mapper.Map<Note_MDL>(model);
                        entity.CreatedBy = _jWTAuthManager.User.Id;
                        entity.Status = (int)CommonEnum.ProgressStatus.unapproved;
                        await _dBContext.Notes.AddAsync(entity);
                        await _dBContext.SaveChangesAsync(CancellationToken.None);

                        #region Tags
                        if (model.Note_Tags is not null && model.Note_Tags.Count() > 0)
                        {
                            List<NoteTags_MDL> _Tags = new List<NoteTags_MDL>();
                            foreach (var item in model.Note_Tags)
                            {
                                NoteTags_MDL noteTags_ = new NoteTags_MDL() { NoteId = entity.Id, TagId = item };
                                _Tags.Add(noteTags_);
                            }
                            await _dBContext.NoteTags.AddRangeAsync(_Tags);
                            await _dBContext.SaveChangesAsync(CancellationToken.None);
                        }
                        #endregion
                        #region Subject
                        if (model.Note_Subjects is not null && model.Note_Subjects.Count() > 0)
                        {
                            List<Note_Subject_MDL> _Subject = new List<Note_Subject_MDL>();
                            foreach (var item in model.Note_Subjects)
                            {
                                item.NoteId = entity.Id;
                                Note_Subject_MDL faq = _mapper.Map<Note_Subject_MDL>(item);
                                _Subject.Add(faq);
                            }
                            await _dBContext.NoteSubject.AddRangeAsync(_Subject);
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
                    Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "AddUpdate"));
                    return SetResultStatus<int>(0, MessageStatus.Error, false);
                }
            }
        }

        public ServiceResponse<List<NoteTitleCheckDTO>> CheckArticleTitle(string SearchText)
        {
            ServiceResponse<List<NoteTitleCheckDTO>> serviceResponse = new ServiceResponse<List<NoteTitleCheckDTO>>();
            List<NoteTitleCheckDTO> result = new List<NoteTitleCheckDTO>();
            try
            {
                serviceResponse.Data = QueryList<NoteTitleCheckDTO>(@"Sp_CheckNoteTitle @SearchText", new { SearchText = SearchText }).Data.ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "CheckArticleTitle"));
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
                Note_MDL _note = await _dBContext.Notes.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_note == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                _note.IsDelete = true;
                _note.IsActive = true;
                _note.ModifiedDate = DateTime.Now;
                _note.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Notes.Update(_note);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.Delete, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "Delete"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }

        public async Task<ServiceResponse<NoteResponseDTO>> GetById(int Id)
        {
            ServiceResponse<NoteResponseDTO> _response = new();
            NoteResponseDTO _noteResult = new();

            try
            {
                if (_dBContext.DbContext.ChangeTracker.QueryTrackingBehavior != QueryTrackingBehavior.NoTracking)
                {
                    _dBContext.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                }
                Note_MDL _note = await _dBContext.Notes.Include(p => p.Note_Tags).Include(p => p.Note_Subjects).Where(s => s.Id == Id && s.IsActive && !s.IsDelete).FirstOrDefaultAsync();
                if (_note is null)
                {
                    return SetResultStatus<NoteResponseDTO>(null, MessageStatus.NoRecord, true);

                }
                _noteResult = _mapper.Map<NoteResponseDTO>(_note);
                if (_note.Note_Tags.Count > 0)
                {
                    _noteResult.Note_Tags = _note.Note_Tags.Select(s => s.TagId).ToList();
                }
                if (_note.Note_Subjects.Count > 0)
                {
                    _noteResult.Note_Subjects = _mapper.Map<List<NoteSubjecResponseDTO>>(_note.Note_Subjects);
                }
                return SetResultStatus<NoteResponseDTO>(_noteResult, MessageStatus.Success, true);

            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "GetById"));
                return SetResultStatus<NoteResponseDTO>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
            }
        }

        public ServiceResponse<List<NoteViewListDTO>> GetPagination(NoteFilterDTO filterModel)
        {
            try
            {
                var result = QueryList<NoteViewListDTO>(@"Sp_NotePagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@FromDate,@ToDate,@Title,@TitleHindi", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<NoteViewListDTO>>(result.Data, MessageStatus.Success, result.IsSuccess, "", "", result.Data[0].TotalRows, StatusCodes.Status200OK, null);
                }
                else
                {
                    return SetResultStatus<List<NoteViewListDTO>>(null, MessageStatus.NoRecord, result.IsSuccess, "", "", 0, StatusCodes.Status404NotFound, null);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "GetPagination"));
                return SetResultStatus<List<NoteViewListDTO>>(null, MessageStatus.Error, false, ex.Message, ex.InnerException.Message, 0, StatusCodes.Status500InternalServerError, null);
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
                Note_MDL _note = await _dBContext.Notes.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_note == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                if (ProgressStatus == (int)CommonEnum.ProgressStatus.published || ProgressStatus == (int)CommonEnum.ProgressStatus.unapproved || ProgressStatus == (int)CommonEnum.ProgressStatus.approved)
                {
                    _note.Status = ProgressStatus;
                    if (ProgressStatus == (int)CommonEnum.ProgressStatus.published)
                    {
                        _note.PublisherDate = DateTime.Now;
                        _note.PublisherId = _jWTAuthManager.User.Id;
                    }
                    _dBContext.Notes.Update(_note);
                    await _dBContext.SaveChangesAsync(CancellationToken.None);
                    await _dBContext.DbContext.DisposeAsync();
                    return SetResultStatus<bool>(true, MessageStatus.StatusProgressUpdate, true);
                }
                return SetResultStatus<bool>(false, MessageStatus.StatusProgressNotValidCodeUpdate, false);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "ProgressStatus"));
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
                Note_MDL _note = await _dBContext.Notes.Where(s => !s.IsDelete && s.Id == Id).FirstOrDefaultAsync();
                if (_note == null)
                {
                    return SetResultStatus<bool>(false, MessageStatus.NotExist, false);
                }
                _note.IsActive = !_note.IsActive;
                _note.ModifiedDate = DateTime.Now;
                _note.ModifiedBy = _jWTAuthManager.User.Id;
                _dBContext.Notes.Update(_note);
                await _dBContext.SaveChangesAsync(CancellationToken.None);
                await _dBContext.DbContext.DisposeAsync();
                return SetResultStatus<bool>(true, MessageStatus.StatusUpdate, true);
            }
            catch (Exception ex)
            {
                await _dBContext.DbContext.DisposeAsync();
                Log.Error(ex, CommonFunction.Errorstring("NoteService.cs", "UpdateStatus"));
                return SetResultStatus<bool>(false, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
