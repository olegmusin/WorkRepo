using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShiftsCalendarASP.Data;
using ShiftsCalendarASP.Models;
using ShiftsCalendarASP.Services;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using ShiftsCalendar.Models;
using ShiftsCalendar.ViewModels;
using ShiftsCalendar.Models.Repository;

namespace ShiftsCalendarASP
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
                builder.AddUserSecrets();
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
            services.AddDbContext<ShiftsCalendarContext>();
            services.AddTransient<DbSeedData>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            //One per request cycle
            services.AddScoped<ShiftsRepository>();
            services.AddScoped<ShiftsViewModel>();
            services.AddScoped<ProjectsRepository>();
            services.AddScoped<ProjectsViewModel>();
            services.AddScoped<WorkersRepository>();
            services.AddScoped<WorkersViewModel>();
           
            // Add framework services.

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
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
                app.UseBrowserLink();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                loggerFactory.AddDebug(LogLevel.Information);
            }

            

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("Api", "api/{controller}/{action}/{id?}");
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}
