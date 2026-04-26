using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IJobDesignationMasterService
    {
        ServiceResponse<int> AddUpdate(JobDesignationMasterModel model);
        ServiceResponse<List<JobDesignationMasterViewModel>> GetList();
        ServiceResponse<JobDesignationMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<JobDesignationMasterViewModel>> GetPagination(JobDesignationMasterFilterModel filterModel);

    }
}
