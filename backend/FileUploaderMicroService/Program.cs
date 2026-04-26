using CommonService.JWT;
using CommonService.Other;
using CommonService.Other.AppConfig;
using FileUploaderMicroService.Service;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "Content"
});
var appName = "FileUpload Seervice ";

CofigurationBuilder.AddRules(builder.Services);
AddResponsecompression.AddResponsecompressionBuilder(builder);

CofigurationBuilder.Swagger(builder.Services, appName);

CofigurationBuilder.CorsPolicy(builder.Services, appName);

builder.Services.AddScoped<IFileUploaderService, FileUploaderService>();

builder.Services.AddScoped<FileUploader, FileUploader>();

builder.Services.AddSingleton<JWTAuthManager, JWTAuthManager>();

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

AppConfigFactory.InitializeConfiguration(builder.Configuration);

CofigurationBuilder.Seriallog(builder);

var app = builder.Build();

CofigurationBuilder.UseSwagger(app, appName);

CofigurationBuilder.UseStaticFiles(app, "");
//CofigurationBuilder.UseStaticFiles(app, "Content");

CofigurationBuilder.UseSerialog(app);

CofigurationBuilder.UseCorsPolicy(app, appName);

CofigurationBuilder.UseMiddleware(app);

app.MapControllers();

app.Run();
