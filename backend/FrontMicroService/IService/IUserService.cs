using ModelService.Model.Front;
using static CommonService.Other.UtilityManager;

namespace FrontMicroService.IService
{
    public interface IUserService
    {
        ServiceResponse<FrontUserViewModel> AddUpdate(FrontUserModel model, IFormFile profile = null);
        ServiceResponse<FrontUserViewModel> GetById(bool isPrivate = false,int Id = 0, string email = "", string UId = "");
        ServiceResponse<bool> UserDelete(int Id);
        ServiceResponse<FrontUserViewModel> GetUserLogin(FrontUserLoginModel model);
        ServiceResponse<bool> UpdateFCMToken(string fcmToken);
    }
}
