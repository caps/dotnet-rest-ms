using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using App.Metrics.Configuration;
using App.Metrics.Data;
using App.Metrics.DependencyInjection;
using App.Metrics.Formatters.Json;
using App.Metrics.Infrastructure;
using Hystrix.Dotnet.AspNetCore;

namespace Caps.DotnetMicroservice
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMetrics();

            app.UseMvc();
            
            app.UseHystrixMetricsEndpoint("hystrix.stream");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services
                .AddLogging()
                .AddRouting(options => { options.LowercaseUrls = true; });

            services.AddMvc(options => options.AddMetricsResourceFilter());

            services
                .AddMetrics(options =>
                {
                    options.WithGlobalTags((globalTags, envInfo) =>
                    {
                        globalTags.Add("host", envInfo.HostName);
                        globalTags.Add("machine_name", envInfo.MachineName);
                        globalTags.Add("app_name", envInfo.EntryAssemblyName);
                        globalTags.Add("app_version", envInfo.EntryAssemblyVersion);
                    });
                })
                .AddPrometheusProtobufSerialization()
                .AddHealthChecks()
                .AddMetricsMiddleware(Configuration.GetSection("Metrics"));
                
            services.AddHystrix();
        }
    }        
}
