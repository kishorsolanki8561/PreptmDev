using ModelService.Model.Front;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IBookmarkService
    {
        //ServiceResponse<BookmarkResponseModel> AddRemoveBookmark(BookmarkModel model);
        ServiceResponse<int?> AddRemoveBookmark(BookmarkModel model);
        ServiceResponse<List<BookmarkPostListModel>> GetBookmarkPostListByUser(BookmarkSearchFilterModel filterModel);
    }
}
