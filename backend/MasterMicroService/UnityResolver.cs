using CommonService.JWT;
using CommonService.Other;
using MasterMicroService.IService;
using MasterMicroService.Service;
using MasterService.IService;
using MasterService.Service;

namespace MasterService
{
    public static class UnityResolver
    {
        public static IServiceCollection RegisterServices(
          this IServiceCollection services)
        {
            services.AddTransient<HttpClient>();
            #region singleton
            //services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<JWTAuthManager, JWTAuthManager>();
            services.AddScoped<FileUploader, FileUploader>();
            //services.AddScoped<HelperService, HelperService>();
            #endregion

            #region scoped
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMenuMasterService, MenuMasterService>();
            services.AddScoped<IPageMasterService, PageMasterService>();
            services.AddScoped<IDepartmentMasterService, DepartmentMasterService>();
            services.AddScoped<IJobDesignationMasterService, JobDesignationMasterService>();
            services.AddScoped<IQualificationMasterService, QualificationMasterService>();
            services.AddScoped<ICategoryMasterService, CategoryMasterService>();
            services.AddScoped<IAssetsMasterService, AssetsMasterService>();
            services.AddScoped<IBlockTypeService, BlockTypeService>();
            services.AddScoped<IGroupMasterService, GroupMasterService>();
            services.AddScoped<IUserTypeMasterService, UserTypeMasterService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IAdditionalPagesService, AdditionalPagesService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<ILookupTypeService, LookupTypeService>();
            services.AddScoped<IBannerService, BannerService>();
            #endregion

            return services;
        }
    }

}
