using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using Newtonsoft.Json;
using Serilog;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentMasterService _departmentMasterService;
        public DepartmentController(IDepartmentMasterService departmentMasterService)
        {
            _departmentMasterService = departmentMasterService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpDate(DepartmentMasterModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            try
            {
                var Data = UtilityManager.PostValidator<DepartmentMasterModel>.IsValid(ref IsSuccess, model);
                if (!IsSuccess) return Data;
                return _departmentMasterService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex, CommonFunction.Errorstring("DepartmentService.cs", "AddUpDate"));
                obj.Exception = ex.Message;
                obj.IsSuccess = false;
                obj.Message = MessageStatus.Error;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<DepartmentMasterModel> GetById(int Id)
        {
            ServiceResponse<DepartmentMasterModel> obj = new ServiceResponse<DepartmentMasterModel>();
            try
            {
                return _departmentMasterService.GetById(Id);
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
                return _departmentMasterService.Delete(Id);
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
                return _departmentMasterService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<DepartmentMasterViewModel>> GetPagination(DepartmentMasterFilterModel filterModel)
        {
            ServiceResponse<List<DepartmentMasterViewModel>> obj = new ServiceResponse<List<DepartmentMasterViewModel>>();
            try
            {
                return _departmentMasterService.GetPagination(filterModel);

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
