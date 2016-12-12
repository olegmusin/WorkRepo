using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShiftsCalendar.Models;
using ShiftsCalendar.Models.Repository;
using ShiftsCalendar.ViewModels;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace ShiftsCalendar
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json");
            Configuration = builder.Build();
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddDbContext<ShiftsCalendarContext>();
            //One per request cycle
           
            services.AddScoped<ShiftsRepository>();
            services.AddScoped<ShiftsViewModel>();
            services.AddScoped<ProjectsRepository>();
            services.AddScoped<ProjectsViewModel>();
            services.AddScoped<WorkersRepository>();
            services.AddScoped<WorkersViewModel>();
            services.AddTransient<DbSeedData>();

            services.AddLogging();
            services.AddMvc()
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }); 

        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
                              DbSeedData seeder)
        {
            Mapper.Initialize(config =>
                    {
                        config.CreateMap<ShiftsViewModel, Shift>().ReverseMap();
                        config.CreateMap<ProjectsViewModel, Project>().ReverseMap();
                        config.CreateMap<WorkersViewModel, Worker>()
                        .ForMember(w => w.Shifts,
                        vm => vm.MapFrom
                        (
                            q => Mapper.Map<ICollection<WorkerShift>, ICollection<WorkerShift>>(q.Shifts)
                        ))
                        .ReverseMap();

                    }
                    );
            //Support for Angular2 client app routing paths. 
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Information);
            }


            app.UseMvc(config =>
            {
                config.MapRoute("Default", "{controller}/{action}/{id?}");
                config.MapRoute("Api", "api/{controller}/{action}/{id?}");
           //     config.MapRoute("Api1", "api/{controller}/{id}/shifts");
            });

            seeder.EnsureSeedData().Wait();
            
        }
    }
}
