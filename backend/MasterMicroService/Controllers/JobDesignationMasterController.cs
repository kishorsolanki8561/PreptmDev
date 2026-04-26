using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class JobDesignationMasterController : ControllerBase
    {
        private readonly IJobDesignationMasterService _jobDesignationMasterService;
        public JobDesignationMasterController(IJobDesignationMasterService jobDesignationMasterService)
        {
            _jobDesignationMasterService = jobDesignationMasterService;
        }
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        [HttpPost]
        public object AddUpDate(JobDesignationMasterModel model)
        {
            ServiceResponse<int> obj = new ServiceResponse<int>();
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<JobDesignationMasterModel>.IsValid(ref IsSuccess, model);
            try
            {
                if (!IsSuccess) return Data;
                return _jobDesignationMasterService.AddUpdate(model);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpGet]
        public ServiceResponse<JobDesignationMasterModel> GetById(int Id)
        {
            ServiceResponse<JobDesignationMasterModel> obj = new ServiceResponse<JobDesignationMasterModel>();
            try
            {
                return _jobDesignationMasterService.GetById(Id);
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
                return _jobDesignationMasterService.Delete(Id);
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
                return _jobDesignationMasterService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }
        [HttpPost]
        public ServiceResponse<List<JobDesignationMasterViewModel>> GetPagination(JobDesignationMasterFilterModel filterModel)
        {
            ServiceResponse<List<JobDesignationMasterViewModel>> obj = new ServiceResponse<List<JobDesignationMasterViewModel>>();
            try
            {
                return _jobDesignationMasterService.GetPagination(filterModel);

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
