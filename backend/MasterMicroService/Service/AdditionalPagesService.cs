using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using ModelService.Model.MastersModel;
using Serilog;

namespace MasterMicroService.Service
{
    public class AdditionalPagesService : UtilityManager, IAdditionalPagesService
    {
        private readonly HelperService _helperService;
        private readonly ILoggerManager _logger;
        public AdditionalPagesService(HelperService helperService, ILoggerManager logger) {
            _helperService = helperService;
            _logger = logger;
        }
        public ServiceResponse<int> AddUpdate(AdditionalPagesModel model)
        {
            try
            {
                ServiceResponse<int> response = new ServiceResponse<int>();
                var result = AddUpdate(QueriesPaths.AdditionalPages.QAddUpdate, _helperService.AddDynamicParameters(model), null);
                if (!result.IsSuccess && result.StatusCode == StatusCodes.Status409Conflict)
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.PageTypeExist, result.IsSuccess);
                }
                if (result.IsSuccess)
                {
                    if (result.StatusCode == StatusCodes.Status200OK)
                    {
                        if (model.Id > 0)
                            response = SetResultStatus<int>(model.Id, MessageStatus.Update, result.IsSuccess);

                        else
                            response = SetResultStatus<int>(result.Data, MessageStatus.Save, result.IsSuccess);
                    }
                    return response;
                }
                else
                {
                    response = SetResultStatus<int>(result.Data, MessageStatus.Error, result.IsSuccess);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AdditionalPagesService.cs", "AddUpdate"));
                return SetResultStatus<int>(0, MessageStatus.Error, false);
            }
        }

        public ServiceResponse<AdditionalPagesViewModel> GetById(int Id)
        {
            try
            {
                var result = QueryFast<AdditionalPagesViewModel>(QueriesPaths.AdditionalPages.QGetById, new { Id = Id });
                if (result.Data != null)
                {
                    return SetResultStatus<AdditionalPagesViewModel>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<AdditionalPagesViewModel>(null, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AdditionalPagesService.cs", "GetById"));
                return SetResultStatus<AdditionalPagesViewModel>(null, MessageStatus.Error, false, ex.Message);
            }
        }

        public ServiceResponse<List<AdditionalPagesListModel>> GetList()
        {
            try
            {
                var result = QueryList<AdditionalPagesListModel>(QueriesPaths.AdditionalPages.QGetList, null);
                if (result.Data != null)
                {
                    return SetResultStatus<List<AdditionalPagesListModel>>(result.Data, MessageStatus.Success, true);
                }
                else
                {
                    return SetResultStatus<List<AdditionalPagesListModel>>(result.Data, MessageStatus.NoRecord, true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("AdditionalPagesService.cs", "GetList"));
                return SetResultStatus<List<AdditionalPagesListModel>>(null, MessageStatus.Error, false, ex.Message);
            }
        }
    }
}
