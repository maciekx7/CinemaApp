using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaServer.DBConnector;
using CinemaServer.Logic;
using CinemaServer.Model;
using CinemaServer.Model.cinemadb;
using CinemaServer.Rest.Logic.APILogic;
using CinemaServer.Rest.Model;
using CinemaServer.Rest.Model.APIModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using CinemaServer.DBConnector.DBInfo;

namespace CinemaServer.Server
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

            services.AddControllersWithViews();

            services.AddDbContext<cinemadbContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Client, IdentityRole>()
                .AddEntityFrameworkStores<cinemadbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "javaapp",
                    ValidIssuer = DBInfoData.GetInstance().UserName,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7S79jvOkEdwoRqHx"))
                };
            });

            services.AddAuthorization(options =>
                options.AddPolicy("ValidAccessToken", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CinemaServer.Server", Version = "v1" });
            });

            /*services.AddDbContext<cinemadbContext>(options =>
                options.UseMySQL(
                Configuration.GetConnectionString("DefaultConnection")));
            */

            services.AddTransient<ICinemaQueriesHandler, CinemaQueriesHandler>();
            services.AddSingleton<ICinemaRepository, CinemaRepository>();
            services.AddTransient<IValidationHandler, ValidationHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "CinemaServer.Server v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            
            app.UseAuthorization();

            //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
