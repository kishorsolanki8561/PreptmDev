using CommonService.JWT;
using CommonService.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService.Model;
using Newtonsoft.Json;
using Serilog;
using TranslationMicroService.IService;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BlockContentsController : ControllerBase
    {
        private readonly IBlockContentsService _blockContentsService;
        public BlockContentsController(IBlockContentsService blockContentsService)
        {
            _blockContentsService = blockContentsService;
        }


        [HttpPost]
        public async Task<ActionResult> AddUpdate(BlockContentsRequestDTO model)
        {
            bool IsSuccess = false;
            var Data = UtilityManager.PostValidator<BlockContentsRequestDTO>.IsValid(ref IsSuccess, model);
            if (!IsSuccess) return Conflict(Data);
            return Ok(await _blockContentsService.AddUpdate(model));
        }


        [HttpGet]
        public ServiceResponse<BlockContentResponseDTO> GetById(int Id)
        {
            ServiceResponse<BlockContentResponseDTO> obj = new ServiceResponse<BlockContentResponseDTO>();
            try
            {
                return _blockContentsService.GetById(Id);
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
                return _blockContentsService.Delete(Id);
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
                return _blockContentsService.UpdateStatus(Id);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpPost]
        public ServiceResponse<List<BlockContentsViewModel>> GetList(BlockContentsFilterModel filterModel)
        {
            ServiceResponse<List<BlockContentsViewModel>> obj = new ServiceResponse<List<BlockContentsViewModel>>();
            try
            {
                return _blockContentsService.GetPagination(filterModel);

            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpGet]
        public ServiceResponse<bool> BlockContentProgressStatus(int Id, int ProgressStatus)
        {
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                return _blockContentsService.BlockContentProgressStatus(Id, ProgressStatus);
            }
            catch (Exception ex)
            {
                obj.Exception = ex.Message;
                obj.StatusCode = StatusCodes.Status500InternalServerError;
                return obj;
            }
        }

        [HttpGet]
        public ServiceResponse<List<BlockContentsTitleCheckModel>> CheckBlockContentTitle(string SearchText)
        {
            ServiceResponse<List<BlockContentsTitleCheckModel>> obj = new ServiceResponse<List<BlockContentsTitleCheckModel>>();
            try
            {
                return _blockContentsService.CheckBlockContentTitle(SearchText);
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
