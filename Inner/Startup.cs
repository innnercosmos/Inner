using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inner.Common.DB;
using Inner.Common.Helper;
using Inner.IServices;
using Inner.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Inner
{
    public class Startup
    {
        private string version = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string ApiName { get; set; } = "Inner";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(new Appsettings(Configuration));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();//ȥ��netcoreĬ�ϵ�   httpcontext ת��    ��jwt������
            #region   JWT
            //��ȡ�����ļ�
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
            var keyByteArray = Encoding.ASCII.GetBytes(audienceConfig["Secret"]);

            var signingKey = new SymmetricSecurityKey(keyByteArray);


            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            #region ���ڶ�����������֤����
            // ������֤����
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],//������
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],//������
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            //2.1����֤����core�Դ��ٷ�JWT��֤
            // ����Bearer��֤
            services.AddAuthentication("Bearer")
             // ���JwtBearer����
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // ������ڣ����<�Ƿ����>��ӵ�������ͷ��Ϣ��
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });


            #endregion


            #endregion

            #region Swagger

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.0",
                    Description = $"{ApiName} HTTP API " + version,
                    TermsOfService = new Uri("http://tempuri.org/terms"), //����Э��
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                ////����  Actions require a unique method/path combination for Swagger/OpenAPI 3.0. Use ConflictingActionsResolver as a workaround
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());


                c.OrderActionsBy(o => o.RelativePath);


                //�����������������������
                var xmlPath = Path.Combine(basePath, "Inner.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�
                var xmlModelPath = Path.Combine(basePath, "Inner.Model.xml");//�������Model���xml�ļ���
                c.IncludeXmlComments(xmlModelPath, true);
                var xmlCommonPath = Path.Combine(basePath, "Inner.Common.xml");//�������Model���xml�ļ���
                c.IncludeXmlComments(xmlCommonPath, true);
                #region Swagger�п���JWT����
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                #region Token�󶨵�ConfigureServices  oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme  
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
                #endregion
            });
            #endregion


            services.AddControllers(
                 //���ÿ����xml��ʽ
                 //op =>
                 //{
                 //    op.ReturnHttpNotAcceptable = true;
                 //    op.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                 //});
                 );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();//��Ȩ�������û�е�¼����¼����˭����ֵ��User
            app.UseAuthorization();//������Ȩ�����Ȩ��

            #region Swagger
            app.UseSwagger();
            var basePath = AppContext.BaseDirectory;
            var xmlPath = Path.Combine(basePath, "Inner.xml");//������Ǹո����õ�xml�ļ���
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"{ApiName} v1");
                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });
            #endregion


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
