using System;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection){
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            // Conexão com POSTGRES
            if(Environment.GetEnvironmentVariable("DATABASE").ToLower()=="Postgres".ToLower())
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            };
            // CONFIGURACAO MANUALMENTE
            /*
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseNpgsql("Server=localhost;Port=5437;Database=DBdotnetComDDD;UID=postgres;Pwd=postgres")
            );
            */
        }
    }
}
