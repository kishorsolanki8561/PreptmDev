using CommonService.Other;
using CommonService.Other.AppConfig;
using Microsoft.Extensions.FileProviders;
using TranslationMicroService;
using DBInfrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var appName = "Translation Seervice ";
CofigurationBuilder.AddRules(builder.Services);
AddResponsecompression.AddResponsecompressionBuilder(builder);

CofigurationBuilder.Swagger(builder.Services, appName);

CofigurationBuilder.CorsPolicy(builder.Services, appName);

UnityResolver.RegisterServices(builder.Services);

builder.Services.AddDBPersistence();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


AppConfigFactory.InitializeConfiguration(builder.Configuration);

CofigurationBuilder.Seriallog(builder);

var app = builder.Build();

CofigurationBuilder.UseSwagger(app, appName);

CofigurationBuilder.UseStaticFiles(app);

CofigurationBuilder.UseSerialog(app);

CofigurationBuilder.UseCorsPolicy(app, appName);

CofigurationBuilder.UseMiddleware(app);

app.MapControllers();

app.Run();
