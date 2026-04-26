using ModelService.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelService.Model.MastersModel
{
    public class UserMasterViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int UserTypeCode { get; set; }
        public string? UserTypeName { get; set; }
        [JsonIgnore]
        public bool IsAutoLoggedOut { get; set; }
        //public bool IsActive { get; set; }  
        //public bool IsDelete { get; set; }  
        //public int CreatedBy { get; set; }
        //public int ModifiedBy { get; set; } 
        //public DateTime CreatedDate { get; set; }   
        //public DateTime ModifiedDate { get; set; }
        public string? Token { get; set; }
        public List<DynamicMenuModel> MenuList { get; set; } = new List<DynamicMenuModel>();
        public List<PageComponentActionByLoginModel> PageComponents { get; set; } = new List<PageComponentActionByLoginModel>();
        [JsonIgnore]
        public int TotalRows { get; set; }
        [JsonIgnore]
        public bool IsFront { get; set; }

    }

    public class UserMasterPaginationModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int UserTypeCode { get; set; }
        public string? UserTypeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public int TotalRows { get; set; }

    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int UserTypeCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [JsonIgnore]
        public bool IsAutoLoggedOut { get; set; }
    }

    public class UserMasterModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int UserTypeCode { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public int ModifiedBy { get; set; }
    }

    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Userloginlog
    {
        public int Logid { get; set; }
        public int? Userid { get; set; }
        public DateTime? Logintime { get; set; }
        public string Ip { get; set; } = null!;
        public bool Isactive { get; set; }
        public bool? Isdelete { get; set; }
        public int? Createby { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? Modifieddate { get; set; }
    }
    public class UserFilterModel : IndexModel
    {
        public int IsActive { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
    public class PageComponentActionByLoginModel
    {
        public string PageComponentName { get; set; }
        public int Action { get; set; }
    }
}
