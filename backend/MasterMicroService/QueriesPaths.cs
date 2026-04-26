namespace MasterMicroService
{
    public static class QueriesPaths
    {
        public static class AssetsMasterQueries
        {
            public static string QAddUpdate = @"Sp_AssetsMasterAddUpdate @Id,@Title,@Path,@DirectoryName,@UserId";
            public static string QUpdatePath = @"UPDATE [assetsmaster] SET [path] =@path WHERE  Id = @Id";
            public static string QGetById = @"select * from [AssetsMaster] where isdelete=0 and Id=@Id";
            public static string QDelete = @"Sp_AssetsMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QPagination = @"Sp_AssetsMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@DirectoryName";
            public static string QUpdateStatus = @"Sp_AssetsMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
        public static class BlockTypeQueries
        {
            public static string QAddUpdate = @"Sp_BlockTypeAddUpdate @Id,@Name,@SlugUrl,@UserId,@NameHindi,@Description,@DescriptionHindi,@ForRecruitment";
            public static string QDelete = @"Sp_BlockTypeDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select Id,Name,SlugUrl,IsActive,IsDelete,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,NameHindi,ForRecruitment,ModifiedSideMapDate,Description,DescriptionHindi from [BlockType] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_BlockTypePagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name,@SlugUrl";
            public static string QUpdateStatus = @"Sp_BlockTypeDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
        public static class CategoryMasterQueries
        {
            public static string QAddUpdate = @"Sp_CategoryMasterAddUpdate @Id,@Name,@SlugUrl,@Icon,@UserId,@NameHindi";
            public static string QDirectoryPath = @"Category";
            public static string QUpdatePath = @"UPDATE [categorymaster] SET [icon] =@icon  WHERE  Id = @Id";
            public static string QDelete = @"Sp_CategoryMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from [categorymaster] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_CategoryMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name,@SlugUrl,@NameHindi";
            public static string QUpdateStatus = @"Sp_CategoryMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
        public static class DepartmentMasterQueries
        {
            public static string QAddUpdate = @"Sp_DepartmentMasterAddUpdate @Id,@Name,@Url,@ShortName,@Logo,@Address,@MapUrl,@Email,@PhoneNumber,@StateId,@SlugUrl,@UserId,@Description,@FaceBookLink,@TwitterLink,@NameHindi,@AddressHindi,@DescriptionHindi,@WikipediaEnglishUrl,@WikipediaHindiUrl,@DescriptionJson,@DescriptionHindiJson,@ShortDescription,@ShortDescriptionHindi";
            public static string QDirectoryPath = @"department";
            public static string QUpdatePath = @"UPDATE [departmentmaster] SET [logo] =@logo  WHERE  Id = @Id";
            public static string QDelete = @"Sp_DepartmentMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from Vw_DepartmentMaster where Id=@Id";
            public static string QPagination = @"Sp_DepartmentMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name,@Url,@StateId,@ShortName,@NameHindi";
            public static string QUpdateStatus = @"Sp_DepartmentMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
        public static class GroupMasterQueries
        {
            public static string QAddUpdate = @"Sp_GroupMasterAddUpdate @Id,@Name,@SlugUrl,@UserId,@NameHindi";
            public static string QDelete = @"Sp_GroupMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from [GroupMaster] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_GroupMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name,@SlugUrl";
            public static string QUpdateStatus = @"Sp_GroupMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";

        }
        public static class JobDesignationMasterQueries
        {
            public static string QAddUpdate = @"Sp_JobDesignationMasterAddUpdate  @Id,@Name,@UserId,@NameHindi";
            public static string QDelete = @"Sp_JobDesignationMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from Vw_JobDesignationMaster where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_JobDesignationMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name";
            public static string QUpdateStatus = @"Sp_JobDesignationMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";

        }
        public static class MenuMasterQueries
        {
            public static string QAddUpdate = @"Sp_MenuMasterAddUpdate  @Id,@MenuName,@DisplayName,@HashChild,@ParentId,@Position,@IconClass,@UserId";
            public static string QMappingAddUpdate = @"Sp_MenuMasterMappingAddUpdate @Id,@MenuId,@UserTypeCode";
            public static string QDelete = @"Sp_MenuMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from MenuMaster where IsDelete=0 AND Id=@Id";
            public static string QPagination = @"Sp_MenuMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@MenuName,@HashChild,@ParentMenuId";
            public static string QUpdateStatus = @"Sp_MenuMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
            public static string QMenuMasterMapping = @"select * from MenuMasterMapping where menuId=@menuId";
            public static string QMenuMasterMappingDelete = @"Delete From MenuMasterMapping where UserTypeCode=@tpyeCode And MenuId=@MenuId";
            public static string QDynamicMenu = @"select * from [Vw_MenuMaster] where UserTypeCode = @UserTypeCode ORDER BY Position ";
            public static string QDynamicPage = @"select * from [Vw_PageMaster] where MenuId= @menuid And UserTypeIdCode =@UserTypeIdCode";
            public static string QDynamicPageUrlMapping = @"select * from PageUrlMapping where PageId= @PageId";

        }
        public static class SubCategoryMasterQueries
        {
            public static string QAddUpdate = @"Sp_SubCategoryMasterAddUpdate @Id,@Name,@SlugUrl,@CategoryId,@Icon,@UserId,@NameHindi";
            public static string QDirectoryPath = @"SubCategory";
            public static string QUpdatePath = @"UPDATE [Subcategory] SET [icon] =@icon  WHERE  Id = @Id";
            public static string QDelete = @"Sp_SubCategoryMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from [subcategory] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_SubCategoryMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name,@SlugUrl,@CategoryId";
            public static string QUpdateStatus = @"Sp_SubCategoryMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
        public static class PageMasterQueries
        {
            public static string QAddUpdate = @"Sp_PageMasterAddUpdate  @Id,@Name,@Icon,@PageUrl,@MenuId,@UserId";
            public static string QPageUrlMappingAddUpdate = @"Sp_PageUrlMappingAddUpdate  @Id,@Url,@PageId,@PermissionType";
            public static string QPageMasterPermissionAddUpdate = @"Sp_PageMasterPermissionAddUpdate  @PageId,@MenuId";
            public static string QDelete = @"Sp_PageMasterDeleteUpdateStatus @Type,@Id=@Id,@UserId=@UserId";
            public static string QPageMappingDelete = @"Sp_PageMappingDelete @Type='Delete',@Id,@UserId";
            public static string QGetById = @"select * from PageMaster where  IsDeleted=0 AND  Id=@Id";
            public static string QPagination = @"Sp_PageMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Name,@MenuId";
            public static string QUpdateStatus = @"Sp_PageMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
            public static string QPageUrlMappingGetByPageId = @"select * from PageUrlMapping where PageId = @Id";
            public static string QPagePermissionList = @"select * from Vw_PageComponentPermission where userTypeCode=@UserTypeIdCode";
            public static string QPageMasterPermissionModifiedById = @"Sp_PageComponentPermissionModified @Id,@UserTypeCode";

        }
        public static class UserMasterQueries
        {
            public static string QAddUpdate = @"Sp_UserAddUpdate @Id,@Name,@Email,@Password,@UserTypeCode,@UserId";
            public static string QDelete = @"Sp_UserDeleteUpdateStatus @Type='Delete',@Id =@id,@UserId= @UserId";
            public static string QGetById = @"select * from Vw_User where Id=@Id";
            public static string QPagination = @"Sp_UserPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@Name,@Email,@FromDate,@ToDate";
            public static string QUpdateStatus = @"Sp_UserDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
            public static string QGetUserLogin = @"select * from Vw_User where Email =@Email and password=@password";
            public static string QagePermissionUrlsByUserType = @"Sp_PagePermissionUrlsByUserType @UserTypeIdCode";
            public static string QIsAutoLoggout = @"UPDATE [user] SET [IsAutoLoggedOut] =@IsAutoLoggedOut WHERE  UserTypeCode = @UserTypeCode AND UserTypeCode <> 1";
            public static string QIsAutoLoggoutByUserId = @"UPDATE [user] SET [IsAutoLoggedOut] =@IsAutoLoggedOut WHERE  Id = @Id";

        }

        public static class UserTypeMasterQueries
        {
            public static string QAddUpdate = @"Sp_UserTypeAddUpdate @Id,@TypeName,@UserId";
            public static string QDelete = @"Sp_UserTypeDeleteUpdateStatus @Type='Delete',@Id =@id,@UserId= @UserId";
            public static string QGetById = @"select * from UserType where Id=@Id";
            public static string QGetList = @"select * from UserType where Isdelete=0";
            public static string QUpdateStatus = @"Sp_UserTypeDeleteUpdateStatus @Type='Status',@Id =@id,@UserId=@UserId";
        }
        public static class AdditionalPages
        {
            public static string QAddUpdate = @"Sp_AdditionalPagesAddUpdate @Id,@PageType,@Content,@ContentHindi,@ContentJson,@ContentHindiJson";
            public static string QGetById = @"select * from [AdditionalPages] where Id=@Id";
            public static string QGetList = @"select * from [AdditionalPages] ";
        }
        public static class LookupTypeQueries
        {
            public static string QAddUpdate = @"Sp_LookupTypeAddUpdate @Id,@Title,@TitleHindi,@Description,@DescriptionHindi,@Slug,@UserId";
            public static string QDelete = @"Sp_LookupTypeDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from [LookupType] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_LookupTypePagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@TitleHindi";
            public static string QUpdateStatus = @"Sp_LookupTypeDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
        public static class LookupQueries
        {
            public static string QAddUpdate = @"Sp_LookupAddUpdate @Id,@LookupTypeId,@Title,@TitleHindi,@Description,@DescriptionHindi,@Slug,@UserId";
            public static string QDelete = @"Sp_LookupDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from [Lookup] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_LookupPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@TitleHindi,@LookupTypeId";
            public static string QUpdateStatus = @"Sp_LookupDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }

        public static class BannerQueries
        {
            public static string QAddUpdate = @"Sp_BannerMasterAddUpdate @Id,@Title,@TitleHindi,@URL,@AttachmentUrl,@IsAdvt,@DisplayOrder,@UserId";
            public static string QDelete = @"Sp_BannerMasterDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";
            public static string QGetById = @"select * from [BannerMaster] where isdelete=0 and Id=@Id";
            public static string QPagination = @"Sp_BannerMasterPagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@IsAdvt";
            public static string QUpdateStatus = @"Sp_BannerMasterDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
        }
    }
}
