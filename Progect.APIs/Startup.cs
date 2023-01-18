using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Progect.BL.Interface;
using Progect.BL.Mapper;
using Progect.BL.Repository;
using Progect.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Progect.APIs
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

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



            services.AddDbContextPool<ProjectContext>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("ProjectConnection")));// Connection String


            services.AddScoped<IEmployeeRep, EmployeeRep>();  //Dependancy Injaction => Ctor



            services.AddAutoMapper(x => x.AddProfile(new DomainProfile())); // Mapper


            services.AddSwaggerGen(); //Swagger


            services.AddCors(); //Cross Orgine Resource Sharing

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");        //Swagger
            });



            app.UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyMethod()                       //Cross Orgine Resource Sharing
            .AllowAnyHeader());




            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
