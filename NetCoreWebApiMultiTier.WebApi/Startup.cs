using NetCoreWebApiMultiTier.WebApi.Data.DatabaseContext;
using NetCoreWebApiMultiTier.WebApi.Data.Repositories.Implementations;
using NetCoreWebApiMultiTier.WebApi.Data.Repositories.Interfaces;
using NetCoreWebApiMultiTier.WebApi.ExternalApiServices.Implementations;
using NetCoreWebApiMultiTier.WebApi.ExternalApiServices.Interfaces;
using NetCoreWebApiMultiTier.WebApi.Services.Implementations;
using NetCoreWebApiMultiTier.WebApi.Services.Interfaces;
using NetCoreWebApiMultiTier.WebApi.Validation.Implementations;
using NetCoreWebApiMultiTier.WebApi.Validation.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IO;
using System.Net.Http;

namespace NetCoreWebApiMultiTier.WebApi
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
            // Relax Cors policy to enable a development UI (such as an Angular SPA or MVC Web App) to use the Api.
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            services.AddControllers();

            // Register database context with dependency resolver.
            // EnableRetryOnFailure mitigates the Sql Server not being available owing to transient errors.
            // The Database database cold starts frequently. 
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Database")
                                    ,providerOptions => providerOptions.EnableRetryOnFailure()));

            // Setup Polly for Customer Web Api.
            services.AddHttpClient("CustomerApi").AddPolicyHandler(GetRetryPolicy());

            // Register request lifetime components with dependency resolver.
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRequestValidator, CustomerRequestValidator>();

            // HttpClient is intended to be instantiated once and re-used throughout the life of an application.
            services.AddSingleton<IHttpClientService, HttpClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowAll");

            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreWebApiMultiTier API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                // Handle HttpRequestExceptions, 408 and 5xx status codes
                .HandleTransientHttpError()
                // Retry 3 times, each time wait 1,2 and 4 seconds before retrying.
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
