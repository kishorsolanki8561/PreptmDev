using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class MenuMasterModel
    {
        public int Id { get; set; }
        public string? MenuName { get; set; }
        public string? DisplayName { get; set; }
        public bool HashChild { get; set; }
        public int? ParentId { get; set; }
        public int Position { get; set; }
        public string? IconClass { get; set; }
        [JsonIgnore]
        public long ModifiedBy { get; set; }
        [JsonIgnore]
        public long CreatedBy { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public List<int> UserTypeCodes { get; set; } = new List<int>();
    }

    public class MenuMasterMappingModel
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int UserTypeCode { get; set; }
    }
    public class MenuMasterViewModel
    {
        public int Id { get; set; }
        public string? MenuName { get; set; }
        public string? DisplayName { get; set; }
        public bool HashChild { get; set; }
        public int? ParentId { get; set; }
        public string? ParentMenuName { get; set; }
        public int Position { get; set; }
        public string? IconClass { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedByName { get; set; }

        [JsonIgnore]
        public int TotalRows { get; set; }

    }

    public class DynamicMenuModel
    {
        public int Id { get; set; }
        public string? MenuName { get; set; }
        public bool HashChild { get; set; }
        public int? ParentId { get; set; }
        public string? ParentMenuName { get; set; }
        public int Position { get; set; }
        public string? IconClass { get; set; }
        public List<DynamicPageMasterModel> Pages { get; set; } = new List<DynamicPageMasterModel>();
        public List<DynamicMenuModel> ChildParentMenuList { get; set; } = new List<DynamicMenuModel>();
    }
    public class MenuMasterFilterModel : IndexModel
    {
        public int IsActive { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? MenuName { get; set; }
        public int HashChild { get; set; }
        public int ParentMenuId { get; set; }

        public MenuMasterFilterModel()
        {
            IsActive = -1;
            HashChild = -1;
            ParentMenuId = -1;
        }
    }
}
