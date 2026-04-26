using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using ModelService.CommonModel;
using ModelService.Model;
using ModelService.Model.Front;
using Serilog;
using static CommonService.Other.CommonEnum;

namespace FrontMicroService.Service
{
    public class BookmarkService : UtilityManager, IBookmarkService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        public BookmarkService(HelperService helperService, JWTAuthManager jWTAuthManager)
        {
            _helperService = helperService;
            _jWTAuthManager = jWTAuthManager;
        }
        public ServiceResponse<int?> AddRemoveBookmark(BookmarkModel model)
        {
            ServiceResponse<int?> serviceResponse = new ServiceResponse<int?>();
            try
            {
                model.UserId = _jWTAuthManager.FrontUser.Id;
                var result = Query<BookmarkResponseModel>(@"Sp_BookmarkAddUpdate @Id,@PostId,@UserId,@ModuleEnum", _helperService.AddDynamicParameters(model));
                if (result.IsSuccess)
                {
                 return SetResultStatus<int?>(result.Data.BookmarkId, result.Data.Massage, result.IsSuccess, "", "", 0, StatusCodes.Status200OK);
                }

                return serviceResponse;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BookmarkService.cs", "AddRemoveBookmark"));
                return SetResultStatus<int?>(null, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<List<BookmarkPostListModel>> GetBookmarkPostListByUser(BookmarkSearchFilterModel filterModel)
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;
                filterModel.UserId = user is null ? null : user.Id;
                filterModel.LanguageCode = _jWTAuthManager.LanguageCode ?? "en";
                var result = QueryList<BookmarkPostListModel>(@"Sp_BookmarkPostListByUser @SearchText,@StateId,@PageSize,@Page,@UserId,@LanguageCode", _helperService.AddDynamicParameters(filterModel));
                if (result.Data != null && result.Data.Count() > 0)
                {
                    return SetResultStatus<List<BookmarkPostListModel>>(result.Data, MessageStatus.Success, true, "", "", result.Data[0].TotalRows);
                }
                else
                {
                    return SetResultStatus<List<BookmarkPostListModel>>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("BookmarkService.cs", "GetBookmarkPostListByUser"));
                return SetResultStatus<List<BookmarkPostListModel>>(null, MessageStatus.Error, false);
            }
        }
    }

}
