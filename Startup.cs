using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APICursosGratuitos
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    // Conteúdos e informações para o Swagger:
                    Title = "APICursosGratuitos", 
                    Version = "v1",
                    Description = "API desenvolvida para o site de Cursos Gratuitos!",
                    TermsOfService = new Uri("https://meusite.com"),
                    Contact = new OpenApiContact  //informações de contato
                    {
                        Name = "Paloma Souza",
                        Url = new Uri("https://site.com")

                    },
                    License = new OpenApiLicense
                    {
                        Name = "Edusync - Desafio API",
                        Url = new Uri("https://site.com")

                    }

                });

                // Adicionar configurações extras da documentação, para ler os XMLs
                //Combinar informações, gerando o Assembly
                var xmlArquivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlArquivo));


            });
            // Interfaces e Repositorios usados na configuração do connectionstring declarados como listas, dentro da função AddTransient()

                services.AddTransient<IAulasRepository, AulasRepository>();
                services.AddTransient<IAlunosRepository, AlunosRepository>();
                services.AddTransient<IProfessoresRepository, ProfessoresRepository>();
                services.AddTransient<IAreasRepository, AreasRepository>();
                services.AddTransient<IAlunoCursoRepository, AlunoCursoRepository>();
                services.AddTransient<ICursosRepository, CursosRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APICursosGratuitos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")
                    ),
                RequestPath = "/StaticFiles"    //Diretório/Pasta onde está contida a pasta Images com as imagens dentro. 
                                               // Utilizá-se esse caminho na url
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
