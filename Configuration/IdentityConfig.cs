using IdentidadeAmandaStore.Domain.Contexto;
using IdentidadeAmandaStore.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI;
using IdentidadeAmandaStore.Core.JWT;

namespace IdentidadeAmandaStore.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<IdentidadeAmandaStoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AmandaStoreDB")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagens>()
                .AddEntityFrameworkStores<IdentidadeAmandaStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            services.AddJwtConfiguration(configuration);

            return services;
        }
    }
}
