using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using MasterMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using Newtonsoft.Json;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class AssetsMasterController : ControllerBase
    {
        private readonly IAssetsMasterService _assetsMasterService;
        public AssetsMasterController(IAssetsMasterService  assetsMasterService)
        {
            _assetsMasterService = assetsMasterService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpdate(AssetsMasterModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<AssetsMasterModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _assetsMasterService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<AssetsMasterModel> GetById(int Id)
        {
            ServiceResponse<AssetsMasterModel> obj = new ServiceResponse<AssetsMasterModel>();
            try
            {
                return _assetsMasterService.GetById(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<AssetsMasterViewModel>> GetList(AssetsMasterFilterModel filterModel)
        {
            ServiceResponse<List<AssetsMasterViewModel>> obj = new ServiceResponse<List<AssetsMasterViewModel>>();
            try
            {
                return _assetsMasterService.GetPagination(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpGet]
        public ServiceResponse<bool> Delete(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _assetsMasterService.Delete(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<bool> UpdateStatus(int Id)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _assetsMasterService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
    }
}
