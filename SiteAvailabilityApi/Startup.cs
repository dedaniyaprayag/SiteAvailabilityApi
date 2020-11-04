using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SiteAvailabilityApi.Config;
using SiteAvailabilityApi.Infrastructure;
using SiteAvailabilityApi.Services;

namespace SiteAvailabilityApi
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
            services.AddCors();
            services.AddControllers();
            services.AddOptions();
            services.AddSingleton<IPostgreSqlConfiguration>(Configuration.GetSection("PostgreSql").Get<PostgreSqlConfiguration>());
            services.AddSingleton<IRabbitMqConfiguration>(Configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>());
            services.AddTransient<ISiteAvailablityService, SiteAvailabilityService>();
            services.AddTransient<IDbProvider, PostgresSqlProvider>();
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
