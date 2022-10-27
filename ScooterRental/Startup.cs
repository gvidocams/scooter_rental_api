using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScooterRental.Core.CrossTables;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Core.Validations;
using ScooterRental.Data;
using ScooterRental.Services;

namespace ScooterRental
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScooterRental", Version = "v1" });
            });

            var connectionString = Configuration.GetConnectionString("Scooter-Rental");

            services.AddDbContext<ScooterRentalDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IScooterRentalDbContext, ScooterRentalDbContext>();
            services.AddScoped<IDbService, DbService>();

            services.AddScoped<IEntityService<RentalCompany>, EntityService<RentalCompany>>();
            services.AddScoped<IEntityService<RentalDetail>, EntityService<RentalDetail>>();
            services.AddScoped<IEntityService<Scooter>, EntityService<Scooter>>();

            services.AddScoped<IEntityService<CompanyScooter>, EntityService<CompanyScooter>>();
            services.AddScoped<IEntityService<CompanyRentalDetail>, EntityService<CompanyRentalDetail>>();

            services.AddScoped<ICompanyService, CompanyService>();
            
            services.AddScoped<CompanyValidator>();
            services.AddScoped<ScooterValidator>();

            services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScooterRental v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
