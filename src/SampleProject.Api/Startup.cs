using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SampleProject.Infrastructure.Authentication.Handle;
using SampleProject.Infrastructure.Authorization.Handle;
using SampleProject.Infrastructure.Authorization.Requirements;
using System;
using System.Text;

namespace SampleProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<Random>();

            services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Basic", (policy) => {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new BasicAuthorizationRequirement());
                });

                options.DefaultPolicy = options.GetPolicy("Basic");
            });

            services.AddSingleton<IAuthorizationHandler, BasicAuthorizationHandler>();

            services.AddAuthentication(options => {
                options.DefaultScheme = "SampleProjectAuthentication";
            })
            .AddScheme<AuthenticationSchemeOptions, SampleProjectAuthenticationHandler>("SampleProjectAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
