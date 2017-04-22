using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using Vizwiz.API.Services;
using Microsoft.Extensions.Configuration;
using Vizwiz.API.Entities;

namespace Vizwiz.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            services.AddTransient<IMailService, LocalMailService>();

            //var connectionString = Startup.Configuration["connectionStrings:VizwizDbConnectionString"]; 
            //services.AddDbContext<VizwizContext>(o => o.UseInMemoryDatabase());
            //var connectionString = Startup.Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<VizwizContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IVizwizRepository, VizwizRepository>()   ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            VizwizContext vizwizContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler();
            }

            //vizwizContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Tag, Models.TagWithoutMessagesDto>();
                cfg.CreateMap<Entities.Tag, Models.TagDto>();
                cfg.CreateMap<Entities.Message, Models.MessageDto>();
                cfg.CreateMap<Models.MessageForCreationDto, Entities.Message>();
                cfg.CreateMap<Models.MessageForUpdateDto, Entities.Message>();
                cfg.CreateMap<Entities.Message, Models.MessageForUpdateDto>();
            });

            //app.UseCors(builder =>
            //    builder.WithOrigins("http://localhost:4200"));
            //app.UseCors(b => b.WithMethods("POST", "GET"));

            app.UseMvc();

            //app.Run((context) =>
            //{
            //    throw new Exception("Example Exception");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
