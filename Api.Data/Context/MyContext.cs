using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // COMANDO : Migrations
            //     dotnet ef migrations add First_Migration
            // COMANDO : Update no Banco
            //     dotnet ef database update
            base.OnModelCreating(modelBuilder);


            // Entidade sendo carregada pela Estrutura MAP
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
            // 1 - tableEntity
            // 2 - Context
            // 3 - MAP
            // 4 - Migrations

        }

    }
}
