using CM.Elements.Api.BLL.BusinessServices;
using CMS.Elelment.Api.DAL.Repositories;
using CMS.Elements.Api.Web.Host.ExceptionHandler;
using CMS.Elements.Api.Contracts.Interfaces.BusinessLogicLayers;
using CMS.Elements.Api.Contracts.Interfaces.Repositories;
using CMS.Elements.Api.Dal.RA.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Newtonsoft.Json.Serialization;

namespace CMS.Elements.Api.Web.Host
{
    public class Startup
    {
        readonly IWebHostEnvironment WebHostEnvironment;
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            WebHostEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
           
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration); //IConfigurationRoot
            services.AddSingleton<IConfiguration>(Configuration);

            //Add Support for strongly typed Configuration and map to class
            services.AddOptions();
            services.AddDbContext<ContactContext>(c =>
                   c.UseSqlServer(Configuration.GetConnectionString("ContactDbConnection")));

            //Instance injection
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactService, ContactService>();

            services.AddControllers(options =>
            {
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // global error handler
            app.UseExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
