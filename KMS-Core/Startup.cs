using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using KMS.CryptographyProviders;
using KMS.Factory;

namespace KMS_Core
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
            services.AddControllers();

            // Register CryptographyProviderFactory as a singleton
            services.AddSingleton<CryptographyProviderFactory>();

            // Register KeyStoreManager as a singleton
            services.AddSingleton<KeyStoreManager>();

            // Register providers as Transient or Scoped based on your needs
            services.AddTransient<AESProvider>();
            services.AddTransient<RSAProvider>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KMS API",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API for KMS",
                    Contact = new OpenApiContact
                    {
                        Name = "Hamza ELJAOUHARI",
                        Email = "hamza.eljaouhari.1995@gmail.com"
                    }
                });
            });

            // Add CORS policy to allow all origins, methods, and headers
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
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

            app.UseRouting();

            app.UseCors("AllowAll"); // Use the CORS policy

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KMS API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at apps root
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
