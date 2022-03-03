using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySql.EntityFrameworkCore.Extensions;
using WebApi.DAL;

namespace WebApi
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
            services.AddCors(policy => policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));
            services.AddControllers();
            services.AddScoped<DbContext, BlazorDbContext>(); 
            var cc = Environment.GetEnvironmentVariable("Test");
            var cnn=Encoding.Default.GetString(Convert.FromBase64String(Configuration["ConnectionSetting:MySql"]));
            //var cnn=Encoding.Default.GetString(Convert.FromBase64String(cc));
            services.AddDbContext<BlazorDbContext>(options => options.UseMySQL(cnn));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BlazorDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Doc");
            });
            context.Database.EnsureCreated();
        }
    }
}