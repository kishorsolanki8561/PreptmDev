using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;
using System.Xml.Linq;

namespace FrontMicroService.Service
{
    public class AdditionalPagesService : UtilityManager, IAdditionalPagesService
    {
        private readonly JWTAuthManager _jWTAuthManager;
        public AdditionalPagesService(JWTAuthManager  jWTAuthManager)
        {
            _jWTAuthManager = jWTAuthManager;
        }
        public ServiceResponse<string> GetAdditionalPagesByPageType(int PageType)
        {
            try
            {
                var result = QueryFast<AdditionalPagesViewModel>(@"select Content,ContentHindi from  AdditionalPages where PageType=@PageType", new { PageType = PageType });
                if (result.Data != null)
                {
                    var data = _jWTAuthManager.LanguageCode == "hi" ? result.Data.ContentHindi : result.Data.Content;
                    return SetResultStatus<string>(data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<string>(string.Empty, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AdditionalPagesService.cs", "GetAdditionalPagesByPageType"));
                return SetResultStatus<string>(string.Empty, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
