using CommonService.JWT;
using CommonService.Middlewares;
using CommonService.Other.AppConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json;

namespace CommonService.Other
{
    public static class CofigurationBuilder
    {
        #region CorsPolicy
        public static void CorsPolicy(this IServiceCollection services, string appName, string path = "*")
        {
            services.AddCors(p => p.AddPolicy(appName, builder =>
            {
                builder.WithOrigins(path).AllowAnyMethod().AllowAnyHeader();
            }));
        }

        public static void UseCorsPolicy(this WebApplication app, string appName)
        {
            app.UseCors(appName);
        }
        #endregion

        #region Add Service Rules
        public static void AddRules(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddSingleton<HelperService, HelperService>();
            services.AddMemoryCache(o => o.SizeLimit = 512);
            services.AddResponseCaching();
        }
        #endregion

        #region Swagger
        public static void Swagger(this IServiceCollection services, string appName)
        {
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = appName + "Api",
                    Description = "ASP.NET Core 8.0 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        public static void UseSwagger(this WebApplication app, string appName)
        {
            app.UseSwagger(c => { c.RouteTemplate = "/api/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", appName + "Api 6.0");
                c.RoutePrefix = "swagger";

            });
        }

        #endregion

        #region Seriallog
        [Obsolete]
        public static void Seriallog(this WebApplicationBuilder builder)
        {
            //builder.Logging.AddSerilog();
            builder.Host.UseSerilog();
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .Enrich.WithProcessId()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithClientIp()
            .Enrich.WithCorrelationId()
            .Enrich.WithProcessName()
            .Enrich.FromLogContext()
            .WriteTo.File("logs/log.txt"
            , rollingInterval: RollingInterval.Day
            , outputTemplate: "{NewLine}--------------------Log Start --------------------" +
                     "{NewLine}--------------------IP--------------------" +
                     "{NewLine}<{ClientIp}>" +
                     "{NewLine}{Timestamp:yyyy-MM-dd HH:mm} {NewLine}" +
                     "{NewLine}<{MachineName}> {NewLine} {Message} Error Level: [{Level}] {NewLine}" +
                     "--------------------Message--------------------" +
                     "{NewLine}{Message}{NewLine}" +
                     "--------------------Exception--------------------" +
                     "{NewLine}{Exception}{NewLine}" +
                     "--------------------Log End--------------------{NewLine}")
            .WriteTo.MSSqlServer(
                connectionString: AppConfigFactory.Configs.connectionStrings.log
                , tableName: "Logs"
                , autoCreateSqlTable: true
                , columnOptions: new ColumnOptions
                {
                    AdditionalColumns = new List<SqlColumn> {
                    new("LogLevel", System.Data.SqlDbType.NVarChar),
                    new("EventId", System.Data.SqlDbType.BigInt),
                    new("NewLine", System.Data.SqlDbType.NVarChar),
                    new("SourceContext", System.Data.SqlDbType.NVarChar),
                    new("CorrelationId", System.Data.SqlDbType.NVarChar),
                    new("ProcessName", System.Data.SqlDbType.NVarChar),
                    new("ProcessId", System.Data.SqlDbType.BigInt),
                    new("ThreadId", System.Data.SqlDbType.BigInt),
                    new("MachineName", System.Data.SqlDbType.NVarChar),
                    new("ClientIp", System.Data.SqlDbType.NVarChar),
                    new("EnvironmentName", System.Data.SqlDbType.NVarChar)
                    },
                })
                .WriteTo.Console(
                    outputTemplate: "{NewLine}--------------------Log Start --------------------" +
                     "{NewLine}--------------------IP--------------------" +
                     "{NewLine}<{ClientIp}>" +
                     "{NewLine}{Timestamp:yyyy-MM-dd HH:mm} {NewLine}" +
                     "{NewLine}<{MachineName}> {NewLine} {Message} Error Level: [{Level}] {NewLine}" +
                     "--------------------Message--------------------" +
                     "{NewLine}{Message}{NewLine}" +
                     "--------------------Exception--------------------" +
                     "{NewLine}{Exception}{NewLine}" +
                     "--------------------Log End--------------------{NewLine}"
                    )

            .CreateLogger();

            builder.Services.AddHttpLogging(Logging => { });
        }

        public static void UseSerialog(this WebApplication app)
        {
            app.UseHttpLogging();
            app.UseSerilogRequestLogging();

        }
        #endregion

        #region Middlewares
        public static void UseMiddleware(this WebApplication app)
        {
            AddResponsecompression.AddResponsecompressionapp(app);
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseMiddleware<UrlNormalizationMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();
        }
        #endregion

        #region UseStaticFiles

        public static void UseStaticFiles(this WebApplication app, string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory())),
                });
                app.UseDirectoryBrowser();
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                          Path.Combine(Directory.GetCurrentDirectory(), @"" + path)),
                    RequestPath = new PathString("/" + path)
                });
                app.UseDirectoryBrowser(new DirectoryBrowserOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"" + path)),
                    RequestPath = new PathString("/" + path)
                });
            }

        }
        #endregion
    }
}
