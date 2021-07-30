using System;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(_environment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION"  ,"Server=localhost;Port=5437;Database=DBdotnetDDD_tests;UID=postgres;Pwd=postgres");
                Environment.SetEnvironmentVariable("DATABASE"       ,"Postgres");
                Environment.SetEnvironmentVariable("MIGRATION"      ,"APLICAR");
                Environment.SetEnvironmentVariable("Audience"       ,"AudienceExample");
                Environment.SetEnvironmentVariable("Issuer"         ,"IssuerExample");
                Environment.SetEnvironmentVariable("Seconds"        ,"28800");
            };

            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            var config = new AutoMapper.MapperConfiguration( cfg => 
            {
                cfg.AddProfile(new DTOToModelProfile());
                cfg.AddProfile(new EntityToDTOProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Carregada uma única vez, Signing e Token
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            // Embutir o appsettings, em TokenConfigurations
            /*
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations")
            ).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            */
            services.AddAuthentication( authOptions => 
            {
                authOptions.DefaultAuthenticateScheme       = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme          = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => 
            {
                var paramsValidation    = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey           = signingConfigurations.Key; 
                paramsValidation.ValidAudience              = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer                = Environment.GetEnvironmentVariable("Issuer");
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

                // Implementar o Botao no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token JWT",
                    Name        = "Authorization",
                    In          = ParameterLocation.Header,
                    Type        = SecuritySchemeType.ApiKey
                });

                // Implementar a Regra do Botao Authorization
                c.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{
                                Id      = "Bearer",
                                Type    = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()

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

            // Configuração do MIGRATIONS
            if(Environment.GetEnvironmentVariable("MIGRATION").ToLower()=="APLICAR".ToLower())
            {
                using(var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                                            .CreateScope())
                {
                    using( var context = service.ServiceProvider.GetService<MyContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            };


        }
    }
}
