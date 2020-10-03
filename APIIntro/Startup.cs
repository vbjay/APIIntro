
using APIIntro.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APIIntro
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("LocalHostOnly",
                                        policy => policy.RequireAssertion(context =>
                                        {
                                            if (context.Resource is AuthorizationFilterContext mvcContext)
                                            {
                                                var cn = mvcContext.HttpContext.Connection;
                                                bool isLocal = mvcContext.HttpContext.Request.IsLocal();
                                                //Log.Logger.Information($"Remote:{cn.RemoteIpAddress} Local:{cn.RemoteIpAddress} isLocal:{isLocal}");

                                                return isLocal;
                                            }
                                            else
                                            {
                                                return true;
                                            }
                                        }).Build());
            });

            services.AddControllers();

            services.AddSingleton<IConfiguration>(Configuration);

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
