using Microsoft.OpenApi.Models;

namespace Unidas.MS.Maintenance.Services.Api.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Grupo Unidas - Ouro Verde",
                    Description = "API Swagger surface",
                    Contact = new OpenApiContact { Name = "Arquitetura Grupo Unidas", Email = "arquitetura.ti@unidas.com.br", Url = new Uri("http://www.unidas.com.br") },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://www.unidas.com.br") }
                });

                #region Estrutura de Segurança comentada para posterior atuação
                /*
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                */
                #endregion
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}

