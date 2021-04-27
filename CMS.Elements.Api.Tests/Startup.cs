using CM.Elements.Api.BLL.BusinessServices;
using CMS.Elelment.Api.DAL.Repositories;
using CMS.Elements.Api.Contracts.Interfaces.BusinessLogicLayers;
using CMS.Elements.Api.Contracts.Interfaces.Repositories;
using CMS.Elements.Api.Dal.RA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Elements.Api.Tests
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var serviceCollection = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            //Instance injection
            serviceCollection.AddTransient<IContactRepository, ContactRepository>();
            serviceCollection.AddTransient<IContactService, ContactService>();
            serviceCollection.AddDbContext<ContactContext>(c =>
                   c.UseSqlServer(Configuration.GetConnectionString("ContactDbConnection")));

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
