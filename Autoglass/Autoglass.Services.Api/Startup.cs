using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autoglass.Infra.Data.Context;
using Autoglass.Application.AutoMapper;
using Autoglass.Infra.CrossCutting.IoC;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text;
using static Dapper.SqlMapper;
using Autoglass.Services.Api.Settings;
using Microsoft.AspNetCore.Http;

namespace Autoglass.Services.Api
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();


            // Evitar o erro na controller de referencia circular 
            // Instalar os pacotes : NewtonSoftJson/ Microsoft.AspNetCore.Mvc
            services.AddControllers().AddJsonOptions(x =>
              x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddCors();

            services.AddControllers();

            // Setting DBContexts
            services.AddDbContext<AutoglassContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /* inversão de controle - ioc
             aqui não tem o .services.algumametodo, proque foi gerado um classe passando o "services",
             desta forma eles são injetados na classe "InjetorDependencias". */

            InjetorDependencias.RegistrarDependencia(services);

            // mapper
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMappingProfile());
            //});
            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);

            // outra maneira de configurar o serviço automapper.
            services.AddAutoMapper(typeof(AutoMappingProfile));

            // pegando as configuraçoes do arquivo Json appsettings.json
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();


            var key = Encoding.UTF8.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

          //  services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Autoglass.Services.Api",
                        Version = "v1",
                        Description = " APi",
                        Contact = new OpenApiContact
                        {
                            Name = "speed",
                            Email = "novospeed@hotmail.com.br",
                            Url = new Uri("https://www.msn.com/pt-br/noticias/brasil/cid-reclama-que-bolsonaro-o-teria-arrastado-para-a-lama-diz-colunista/ar-AA1giezJ?ocid=msedgntp&cvid=b659d596e5c84569a3c19bbdd960113b&ei=12")
                        }
                    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        new string[] { }
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
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Autoglass.Services.Api v1");
                });
            }

            // Não é no caso deste projeto, mas é usado para permitir usar arquivos estaticos definido em uma pasta. Tipo imagens.
            // Ex: Arquivo Json/ccs/Imagens.
            //app.UseStaticFiles(); 


            app.UseHttpsRedirection();
           // app.UseStatusCodePages();

            app.UseRouting();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

