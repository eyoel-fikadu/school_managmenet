using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SCMS.DataAccess;
using SMS.SERVICE.Service_Layer.IService.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.Admission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.ClassActivity;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.CommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.ConfigurationManagment;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.EventManagement;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.SecurityService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.BusinessLayer.StudentService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IAdmission;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivity;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IClassActivityInternal;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.ICommonService;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IConfigurationManagment;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IEventManagement;
using SMS.SERVICE.ServiceLayer.BusinessLogic.IBusinessLayer.IStudentService;
using Swashbuckle.AspNetCore.Swagger;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SMS.SERVICE.ServiceLayer.Security.SecurityService;
using SMS.SERVICE.ServiceLayer.Security.ISecurityService;
using SMS.API.WEB.Controllers.TeacherPortal.Action_Filters;
using SMS.SERVICE.ServiceLayer.Exception_Handler;
using SMS.API.WEB.Controllers.StudentPortal.Action_Filters;
using SMS.API.WEB.Controllers.SchoolEmployeePortal.Action_Filters;

namespace SMS.API
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
            services.Add(new ServiceDescriptor(typeof(IUserService), new UserService()));
            services.Add(new ServiceDescriptor(typeof(ISchoolService), new SchoolService()));
            services.Add(new ServiceDescriptor(typeof(IEnrollmentService), new EnrollmentService()));
            services.Add(new ServiceDescriptor(typeof(ILookupService), new LookupService()));
            services.Add(new ServiceDescriptor(typeof(ICuriculumService), new CuriculumService()));
            services.Add(new ServiceDescriptor(typeof(IMainSystemService), new MainSystemService()));
            services.Add(new ServiceDescriptor(typeof(ITimeTableService), new TimeTableService()));
            services.Add(new ServiceDescriptor(typeof(IAttendanceService), new AttendanceService()));
            services.Add(new ServiceDescriptor(typeof(IAssesmentService), new AssesmentService()));
            services.Add(new ServiceDescriptor(typeof(ICalendarService), new CalendarService()));
            services.Add(new ServiceDescriptor(typeof(IItAdminSecurityService), new ItAdminSecurityService()));
            services.Add(new ServiceDescriptor(typeof(ITeacherSecurityService), new TeacherSecurityService()));
            services.Add(new ServiceDescriptor(typeof(IStudentSecurityService), new StudentSecurityService()));
            services.AddSingleton(typeof(JwtSettings));
            
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            //Security
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtBearerOption =>
            {
                jwtBearerOption.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TDWEB_Secret_Token_Key_For_Testing_Purpose")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                jwtBearerOption.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //Action filters
            services.AddScoped<TeacherAccessAPIFilters>();
            services.AddScoped<StudentAccessAPIFilters>();
            services.AddScoped<ItAdminAccessAPIFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Exception Handling
            app.ConfigureExceptionHandler();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
