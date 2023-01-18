using _3TierArchitecture.language;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Progect.BL.Interface;
using Progect.BL.Mapper;
using Progect.BL.Repository;
using Progect.DAL.Database;
using Progect.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace _3TierArchitecture
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
            services.AddControllersWithViews()

                //*********************     Multi languages  *****************************
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) // Enable tranlate in view

                .AddDataAnnotationsLocalization(options => //Enable tranlate with validation
                {
                     options.DataAnnotationLocalizerProvider = (type, factory) => 
                     factory.Create(typeof(SharedResource));
                })
                //Multi languages


                .AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver(); // Newtonsoft => CamolCase To BascalCase AjaxCall
                 });


            //connectionstring
            services.AddDbContextPool<ProjectContext>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("ProjectConnection")));// Connection String



            services.AddScoped<IDepartmentRep, DepartmentRep>();
            services.AddScoped<IEmployeeRep, EmployeeRep>();  //Dependancy Injaction => Ctor


            services.AddScoped<ICountryRep, CountryRep>();  //Dependancy Injaction => Ctor
            services.AddScoped<ICityRep, CityRep>();  //Dependancy Injaction => Ctor
            services.AddScoped<IDistrictRep, DistrictRep>();  //Dependancy Injaction => Ctor


            services.AddAutoMapper(x => x.AddProfile(new DomainProfile())); // Mapper


            // ***************************   Is He Login Or No  *******************************
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login"); // UnAuthenticated
                    options.AccessDeniedPath = new PathString("/Account/Login"); // UnAuthorized
                });




            // ********************************       Password Config. ***********************

            services.AddIdentity<IdentityUserExtend, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ProjectContext>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUserExtend>>(TokenOptions.DefaultProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //*********************     Multi languages  *****************************

            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"), // default language in Run Time
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });

            //Multi languages//




            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
