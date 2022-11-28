namespace Sat.Recruitment.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Sat.Recruitment.Api.Dtos;
    using Sat.Recruitment.Api.Validators.Implementations;
    using Sat.Recruitment.Api.Validators.Interfaces;
    using Sat.Recruitment.Domain.Configs;
    using Sat.Recruitment.Repository.Helpers.Implementations;
    using Sat.Recruitment.Repository.Helpers.Interfaces;
    using Sat.Recruitment.Repository.Implementations;
    using Sat.Recruitment.Repository.Interfaces;
    using Sat.Recruitment.Services.Implementations;
    using Sat.Recruitment.Services.Interfaces;

    public class Startup
    {
        protected IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddScoped<IUserFileHelper, UserFileHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMoneyCalculator, NormalUserMoneyCalculator>();
            services.AddScoped<IMoneyCalculator, PremiumUserMoneyCalculator>();
            services.AddScoped<IMoneyCalculator, SuperUserMoneyCalculator>();
            services.AddSingleton<IValidator<UserValidator, UserDto>, UserValidator>();
            services.AddAutoMapper(typeof(MappingProfile));
            var fileConfig = Configuration
                    .GetSection("FilePathConfig")
                    .Get<FilePathConfig>();
            services.AddSingleton(fileConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
