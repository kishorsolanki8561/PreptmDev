using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ModelService.CommonModel;
using ModelService.Model.Front;
using Serilog;

namespace FrontMicroService.Service
{
    public class UserFeedbackService : UtilityManager, IUserFeedbackService
    {
        private readonly HelperService _helperService;
        private readonly JWTAuthManager _jWTAuthManager;
        public UserFeedbackService(HelperService helperService, JWTAuthManager jWTAuthManager)
        {
            _helperService = helperService;
            _jWTAuthManager = jWTAuthManager;
        }
        public ServiceResponse<int> AddUpdate(UserFeedbackModel model)
        {
            try
            {
                var user = _jWTAuthManager.FrontUser;
                model.UserId = user is null ? 0: user.Id;
                model.Status = 1;
                var result = AddUpdate(@"Sp_UserFeedbackAddUpdate @Id,@UserId,@Status,@Type,@Message", _helperService.AddDynamicParameters(model));
                if (result.IsSuccess)
                {
                    return SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess);
                }
                else
                {
                    return SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("UserFeedbackService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

    }

}
