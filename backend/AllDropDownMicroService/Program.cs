using AllDropDownMicroService.Service;
using CommonService.JWT;
using CommonService.Middlewares;
using CommonService.Other;
using CommonService.Other.AppConfig;
using Microsoft.OpenApi.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var appName = "Common DropDown Seervice ";

CofigurationBuilder.AddRules(builder.Services);
AddResponsecompression.AddResponsecompressionBuilder(builder);

CofigurationBuilder.Swagger(builder.Services, appName);

CofigurationBuilder.CorsPolicy(builder.Services, appName);

AppConfigFactory.InitializeConfiguration(builder.Configuration);

CofigurationBuilder.Seriallog(builder);

builder.Services.AddSingleton<JWTAuthManager, JWTAuthManager>();

builder.Services.AddScoped<IAllDropDownServcie, AllDropDownService>();

var app = builder.Build();


CofigurationBuilder.UseSwagger(app, appName);

CofigurationBuilder.UseStaticFiles(app);

CofigurationBuilder.UseSerialog(app);

CofigurationBuilder.UseCorsPolicy(app, appName);

CofigurationBuilder.UseMiddleware(app);
app.UseMiddleware<EncryptionMiddleware>();

app.MapControllers();

app.Run();
