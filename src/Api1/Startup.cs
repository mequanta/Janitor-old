﻿using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Api1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.UseOAuthBearerAuthentication(options =>
            {
                options.Authority = "https://localhost:44333";
                options.Audience = "https://localhost:44333/resources";

//                options.Authority = "https://janitor.chinacloudsites.cn";
//                options.Audience = "https://janitor.chinacloudsites.cn/resources";
                
                options.AutomaticAuthentication = true;
            });

            app.UseMiddleware<RequiredScopesMiddleware>(new List<string> { "api1" });
            app.UseMvc();
        }
    }
}