using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore
{
    public class SwaggerConfiguration
    {
        public static void Configure(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Identidade REST API",
                    Description = "Esta API faz parte do serviço de identidade de AmandaStore",
                    Contact = new OpenApiContact() { Name = "Vagner Developer", Email = "sinxberserker@gmail.com" },
                    License = new OpenApiLicense() { Name = "Vagner", Url = new Uri("https://github.com/VagnerProjects") }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Identity"
                            }
                        },
                        new string[] {}
                    }
                });

            });
        }
    }
}
