using System;
using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace application
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

            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            // Carregada uma única vez, Signing e Token
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            // Embutir o appsettings, em TokenConfigurations
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations")
            ).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication( authOptions => 
            {
                authOptions.DefaultAuthenticateScheme       = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme          = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => 
            {
                var paramsValidation    = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey           = signingConfigurations.Key; 
                paramsValidation.ValidAudience              = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer                = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey   = true;
                paramsValidation.ValidateLifetime           = true;
                paramsValidation.ClockSkew                  = TimeSpan.Zero;
            }) ;

            services.AddAuthorization( auth => 
            {
                auth.AddPolicy("Bearer", 
                    new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build()
                );
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Version = "v1", 
                    Title = "CSharpComDDD", 
                    Description = "Arquitetura DDD",
                    TermsOfService = new Uri("http://www.microsoft.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Alexandre Gomes",
                        Email = "alexandre@....com",
                        Url = new Uri("http://www.microsoft.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Alexandre Gomes",
                        Url = new Uri("http://www.microsoft.com/")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(
                c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "application v1");
                    c.RoutePrefix = string.Empty ;
                }
            );
            


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
