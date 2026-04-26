using CommonService.JWT;
using CommonService.Other;
using FrontMicroService.IService;
using FrontMicroService.Service;

namespace FrontMicroService
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
            services.AddSingleton<JWTAuthManager, JWTAuthManager>();
            services.AddScoped<IRecruitmentService, RecruitmentService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<FileUploader, FileUploader>();
            services.AddScoped<IBookmarkService, BookmarkService>();
            services.AddScoped<IBlockContentService, BlockContentService>();
            services.AddScoped<IAdditionalPagesService, AdditionalPagesService>();
            services.AddScoped<IUserFeedbackService, UserFeedbackService>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<IArticleService, ArticleService>();

            #endregion


            return services;
        }
    }
}
