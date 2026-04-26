using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class QualificationMasterModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class QualificationMasterViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleHindi { get; set; }
        public string? ModifiedByName { get; set; }
        public string? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }

    }
    public class QualificationMasterFilterModel : IndexModel
    {
        public string? Title { get; set; }
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
}
