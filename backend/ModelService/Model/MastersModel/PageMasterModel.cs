using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class PageMasterNModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public int MenuId { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public int ModifiedBy { get; set; }
        public List<PageUrlMapping> PageUrls { get; set; } = new List<PageUrlMapping>();
    }
    public class PageUrlMapping
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        [JsonIgnore]
        public int PageId { get; set; }
        public int PermissionType { get; set; }
    }
    public class PageMasterViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string? ModifiedByName { get; set; }
        //public List<PageUrlMapping> PageUrls { get; set; } = new List<PageUrlMapping>();
        [JsonIgnore]

        public int TotalRows { get; set; }
    }

    public class PageMasterFilterModel : IndexModel
    {
        public int IsActive { get; set; } = -1;
        public string? FromDate { get; set; } = string.Empty;
        public string? ToDate { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public int MenuId { get; set; } = 0;
    }

    public class PagePermissionViewModel
    {
        public int Id { get; set; }
        public string? MenuName { get; set; }
        public string? PageName { get; set; }
        public string PageComponentName { get; set; }
        public int Action { get; set; }
        public bool IsAllowed { get; set; }

    }
    public class PagePermissionModel
    {
        public int Id { get; set; }
        public int UserTypeCode { get; set; }
    }
    public class DynamicPageMasterModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public string PageUrl { get; set; }
        public string? Icon { get; set; }

    }



    public class PageMasterModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string PageUrl { get; set; }
        public int MenuId { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public int ModifiedBy { get; set; }
        public List<PageComponentModel> PageComponents { get; set; } = new List<PageComponentModel>();

    }


    public class PageComponentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public int PageId { get; set; }
        //public List<PageComponentActionModel> PageComponentsAction { get; set; } = new List<PageComponentActionModel>();
        public List<int> PageComponentsAction { get; set; } = new List<int>();

    }

    public class PageComponentActionModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int PageId { get; set; }
        [JsonIgnore]
        public int ComponentId { get; set; }
        public int Action { get; set; }

    }
}
