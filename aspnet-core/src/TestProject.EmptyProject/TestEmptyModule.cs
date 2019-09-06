using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace TestProject.EmptyProject
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule), typeof(AbpAutofacModule))]
    public class TestEmptyModule:AbpModule
    {


        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
            var configuration = context.Services.GetConfiguration();
            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "TestProject";
                    options.ApiSecret = "123456";
                    //This part suppose to be required. Bug of template code?
                    options.InboundJwtClaimTypeMap["sub"] = AbpClaimTypes.UserId;
                    options.InboundJwtClaimTypeMap["role"] = AbpClaimTypes.Role;
                    options.InboundJwtClaimTypeMap["email"] = AbpClaimTypes.Email;
                    options.InboundJwtClaimTypeMap["email_verified"] = AbpClaimTypes.EmailVerified;
                    options.InboundJwtClaimTypeMap["phone_number"] = AbpClaimTypes.PhoneNumber;
                    options.InboundJwtClaimTypeMap["phone_number_verified"] =
                        AbpClaimTypes.PhoneNumberVerified;
                    options.InboundJwtClaimTypeMap["name"] = AbpClaimTypes.UserName;


                    var httpClientHandler = new HttpClientHandler();
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    options.IntrospectionDiscoveryHandler = httpClientHandler;
                    options.IntrospectionBackChannelHandler = httpClientHandler;
                });
            context.Services.AddHttpClient("test")
                .ConfigureHttpMessageHandlerBuilder(builder =>
                {
                    builder.PrimaryHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
                    };
                });
            

        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();


            app.UseVirtualFiles();
            app.UseAuthentication();

            app.UseAuditing();
            app.UseMvcWithDefaultRouteAndArea();
        }
    }
}
