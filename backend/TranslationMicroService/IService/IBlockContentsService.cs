using ModelService.Model;
using static CommonService.Other.UtilityManager;

namespace TranslationMicroService.IService
{
    public interface IBlockContentsService
    {
        //ServiceResponse<int> AddUpdate(BlockContentsModel model);
        Task<ServiceResponse<int>> AddUpdate(BlockContentsRequestDTO model);
        ServiceResponse<List<BlockContentsViewModel>> GetList();
        //ServiceResponse<BlockContentsModel> GetById(int Id);
        ServiceResponse<BlockContentResponseDTO> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<BlockContentsViewModel>> GetPagination(BlockContentsFilterModel filterModel);
        ServiceResponse<bool> BlockContentProgressStatus(int Id, int ProgressStatus);
        ServiceResponse<List<BlockContentsTitleCheckModel>> CheckBlockContentTitle(string SearchText);
    }
}
