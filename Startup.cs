using IdentidadeAmandaStore.Configuration;
using IdentidadeAmandaStore.Domain.Contexto;
using IdentidadeAmandaStore.Extensions;
using IdentidadeAmandaStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore
{
    public class Startup
    {
        private const string PermissoesEspecificasDeOrigem = "_permissoesEspecificasDeOrigem";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
          
            services.AddIdentityConfiguration(Configuration);
            services.AddTransient<IdentityErrorDescriber, IdentityMensagens>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = true;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Password.RequiredLength = 6;
              
            });
            
                services.AddScoped<IRegistrarUsuario, RegistrarUsuario>();
            services.AddTransient<IdentidadeAmandaStoreContext>();
            SwaggerConfiguration.Configure(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(PermissoesEspecificasDeOrigem);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identidade");
            });

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Identidade Amanda Store");
                });
            });
        }
    }
}
