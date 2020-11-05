using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            services.AddApplicationInsightsTelemetry();
            services.AddControllers();
            services.AddOptions();
            services.AddSingleton<IPostgreSqlConfiguration>(Configuration.GetSection("PostgreSql").Get<PostgreSqlConfiguration>());
            services.AddSingleton<IRabbitMqConfiguration>(Configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>());
            services.AddTransient<ISiteAvailablityService, SiteAvailabilityService>();
            services.AddTransient<IDbProvider, PostgresSqlProvider>();
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Site Availabilty Api" });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Site Availabilty Api"); });
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
