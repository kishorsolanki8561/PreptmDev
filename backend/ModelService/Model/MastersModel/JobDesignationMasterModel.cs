using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class JobDesignationMasterModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class JobDesignationMasterViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameHindi { get; set; }

        public string? ModifiedByName { get; set; }
        public string? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }
    }
    public class JobDesignationMasterFilterModel : IndexModel
    {
        public string? Name { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public JobDesignationMasterFilterModel()
        {
            IsActive = -1;
        }
    }
}
