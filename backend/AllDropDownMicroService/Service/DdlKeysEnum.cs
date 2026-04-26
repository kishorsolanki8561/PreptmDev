using Google.Apis.Util;

namespace AllDropDownMicroService.Service
{
    public class DdlKeysEnum
    {
        public enum DdlKeys
        {
            [StringValue("ddlUserType")]
            ddlUserType,
            [StringValue("ddlMenu")]
            ddlMenu,
            [StringValue("ddlState")]
            ddlState,
            [StringValue("ddlDepartment")]
            ddlDepartment,
            [StringValue("ddlPublisher")]
            ddlPublisher,
            [StringValue("ddlJobDesignation")]
            ddlJobDesignation,
            [StringValue("ddlQualification")]
            ddlQualification,
            [StringValue("ddlCategory")]
            ddlCategory,
            [StringValue("ddlSubCategory")]
            ddlSubCategory,
            [StringValue("ddlBlockType")]
            ddlBlockType,
            [StringValue("ddlRecruitment")]
            ddlRecruitment,
            [StringValue("ddlGroup")]
            ddlGroup,
            [StringValue("ddlBlockTypeByForRecruitment")]
            ddlBlockTypeByForRecruitment,
            [StringValue("ddlLookupType")]
            ddlLookupType,
        }
    }
}
