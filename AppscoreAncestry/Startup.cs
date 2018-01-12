using AppscoreAncestry.Entities;
using AppscoreAncestry.Infrastructure;
using AppscoreAncestry.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AppscoreAncestry
{
    public class Startup
    {
        private IHostingEnvironment env;
        private IConfigurationRoot config;

        public Startup(IHostingEnvironment env)
        {
            this.env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(config);

            services.AddSingleton<IDataStore<Data>>(new FileDataStore<Data>(GetDataStoreFileName()));
            services.AddSingleton<IPersonSearchService, PersonSearchService>();

            services.AddMvc();
        }

        private string GetDataStoreFileName()
        {
            return Path.Combine(env.ContentRootPath, "App_Data", config["DataStore:FileName"]);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Search", action = "SearchBasic" });
            });
        }
    }
}
