using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Domain.Contexto
{
    public class IdentidadeAmandaStoreContext: IdentityDbContext
    {
        public IdentidadeAmandaStoreContext(DbContextOptions<IdentidadeAmandaStoreContext> options)
            :base(options)
        {

        }

        public class IdentidadeAmandaStoreFactory : IDesignTimeDbContextFactory<IdentidadeAmandaStoreContext>
        {
            public IdentidadeAmandaStoreContext CreateDbContext(string[] args)
            {

                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);
                var configuration = builder.Build();
                var optionsBuilder = new DbContextOptionsBuilder<IdentidadeAmandaStoreContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AmandaStoreDB"));

                return new IdentidadeAmandaStoreContext(optionsBuilder.Options);
            }
        }
    }
}
