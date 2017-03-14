using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using ShiftsSchedule.Models.ViewModels;
using ShiftsSchedule.Models;
using ShiftsSchedule.Data;
using ShiftsSchedule.Services;
using ShiftsSchedule.Models.Repository;

namespace ShiftsSchedule
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            //Singletons
            services.AddSingleton(Configuration);
            services.AddDbContext<ShiftsScheduleContext>();
            services.AddTransient<WorkersRepository>();
            services.AddTransient<ShiftsRepository>();
            services.AddTransient<ProjectsRepository>();
            services.AddTransient<DbSeedData>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AppKeys"));
            
            //One vm per request cycle

            services.AddScoped<ShiftsViewModel>();
            services.AddScoped<ProjectsViewModel>();
            services.AddScoped<WorkersViewModel>();

            // Add framework services.

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ShiftsScheduleContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<ApplicationRoleManager>();
            services.AddLogging();
            services.AddMvc().AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbSeedData seeder)
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
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                loggerFactory.AddDebug(LogLevel.Information);
            }

            app.UseIdentity();



            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //    routes.MapRoute("Api", "api/{controller}/{action}/{id?}");
            //    routes.MapRoute("ShiftsRoute", "projects/{projectsId}/shifts/{id?}");
            //});

            seeder.EnsureSeedData().Wait();
        }
    }
}
