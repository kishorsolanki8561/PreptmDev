using CommonService.Other;
using MasterMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using Newtonsoft.Json;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryMasterService;
        public SubCategoryController(ISubCategoryService subCategoryMasterService)
        {
            _subCategoryMasterService = subCategoryMasterService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpdate(SubCategoryModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            SubCategoryModel subcategoryMasterModel = new SubCategoryModel();
            var Data = UtilityManager.PostValidator<SubCategoryModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _subCategoryMasterService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<SubCategoryModel> GetById(int Id)
        {
            ServiceResponse<SubCategoryModel> obj = new ServiceResponse<SubCategoryModel>();
            try
            {
                return _subCategoryMasterService.GetById(Id);
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
                return _subCategoryMasterService.Delete(Id);
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
                return _subCategoryMasterService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpPost]
        public ServiceResponse<List<SubCategoryViewModel>> GetList(SubCategoryFilterModel filterModel)
        {
            ServiceResponse<List<SubCategoryViewModel>> obj = new ServiceResponse<List<SubCategoryViewModel>>();
            try
            {
                return _subCategoryMasterService.GetPagination(filterModel);

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
