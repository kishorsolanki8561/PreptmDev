using CommonService.JWT;
using CommonService.Other;
using DBInfrastructure;
using System.Reflection;
using TranslationMicroService.IService;
using TranslationMicroService.Service;
using ModelService;
using TranslationMicroService.Service.Paper;
using TranslationMicroService.Service.Note;
using TranslationMicroService.Service.Syllabus;

namespace TranslationMicroService
{
    public static class UnityResolver
    {
        public static IServiceCollection RegisterServices(
 this IServiceCollection services)
        {
            //_= services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddModelService();
            //services.AddTransient<HttpClient>();
            #region singleton
            //services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<JWTAuthManager, JWTAuthManager>();
            services.AddScoped<IRecruitmentService, RecruitmentService>();
            services.AddScoped<FileUploader, FileUploader>();
            services.AddScoped<IBlockContentsService, BlockContentsService>();
            services.AddScoped<FairBaseHelper, FairBaseHelper>();
            services.AddScoped<ISchemeService, SchemeService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IDBPreptmContext, DBPreptmContext>();
            services.AddScoped<IPaperService, PaperService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ISyllabusService, SyllabusService>();
            #endregion


            return services;
        }
    }
}
