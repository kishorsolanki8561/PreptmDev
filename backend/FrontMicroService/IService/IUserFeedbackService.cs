using ModelService.Model.Front;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IUserFeedbackService
    {
        ServiceResponse<int> AddUpdate(UserFeedbackModel model);
    }
}
