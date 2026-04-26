using ModelService.Model.MastersModel;
using static CommonService.Other.UtilityManager;

namespace MasterMicroService.IService
{
    public interface IQualificationMasterService
    {
        ServiceResponse<int> AddUpdate(QualificationMasterModel model);
        ServiceResponse<List<QualificationMasterViewModel>> GetList();
        ServiceResponse<QualificationMasterModel> GetById(int Id);
        ServiceResponse<bool> Delete(int Id);
        ServiceResponse<bool> UpdateStatus(int Id);
        ServiceResponse<List<QualificationMasterViewModel>> GetPagination(QualificationMasterFilterModel filterModel);

    }
}
