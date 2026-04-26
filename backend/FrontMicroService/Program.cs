using CommonService.Middlewares;
using CommonService.Other;
using CommonService.Other.AppConfig;
using FrontMicroService;

var builder = WebApplication.CreateBuilder(args);

var appName = "Front Seervice ";

CofigurationBuilder.AddRules(builder.Services);
AddResponsecompression.AddResponsecompressionBuilder(builder);

// Add services to the container.

CofigurationBuilder.AddRules(builder.Services);

CofigurationBuilder.Swagger(builder.Services, appName);

CofigurationBuilder.CorsPolicy(builder.Services, appName);

UnityResolver.RegisterServices(builder.Services);

AppConfigFactory.InitializeConfiguration(builder.Configuration);

CofigurationBuilder.Seriallog(builder);


var app = builder.Build();

CofigurationBuilder.UseSwagger(app, appName);

CofigurationBuilder.UseStaticFiles(app);

CofigurationBuilder.UseSerialog(app);

CofigurationBuilder.UseCorsPolicy(app, appName);

CofigurationBuilder.UseMiddleware(app);

//app.UseMiddleware<EncryptionMiddleware>();


app.MapControllers();

app.Run();
