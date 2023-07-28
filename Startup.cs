using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using InfoCity.API.DbContexts;
using InfoCity.API.OperationFilters;
using InfoCity.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace InfoCity.API
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
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;

                options.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)
                    );
                options.Filters.Add(
                       new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable)
                   );
                options.Filters.Add(
                      new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError)
                  );
                options.Filters.Add(
                     new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized)
                 );
            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters();

            services.Configure<MvcOptions>(options =>
            {
                var jsonOutputFormatter = options.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>().FirstOrDefault();
                if (jsonOutputFormatter != null)
                {
                    //Quita la opcion text/json para trabajar con JSON  
                    if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                    }
                }
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("InfoCityAPI", new OpenApiInfo
                {
                    Title = "InfoCity.API",
                    Version = "v1",
                    Description = "Por medio de esta API puede acceder a las ciudades de Colombia y sus puntos de interes.",
                    Contact = new OpenApiContact()
                    {
                        Email = "wilmerab20@gmail.com",
                        Name = "Wilmer Buitrago",
                        Url = new Uri("https://www.linkedin.com/in/wilmer-alexis-buitrago-ruiz-a44a53207/")
                    }
                });

                c.SwaggerDoc("DowloadPictures.API", new OpenApiInfo
                {
                    Title = "DowloadPictures.API (Pictures)",
                    Version = "v1",
                    Description = "Por medio de esta API puede acceder a las imagenes en el repositorio asignado.",
                    Contact = new OpenApiContact()
                    {
                        Email = "wilmerab20@gmail.com",
                        Name = "Wilmer Buitrago",
                        Url = new Uri("https://www.linkedin.com/in/wilmer-alexis-buitrago-ruiz-a44a53207/")
                    }
                });

                c.OperationFilter<GetCityOperationFilter>();

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                c.IncludeXmlComments(xmlCommentsFullPath);

                //Realiza la especificaciones de la autenticacion
                c.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Se necesita de un token para el ingreso de esta API"
                });

                //Realiza los requerimiento de autenticacion en las especificacione
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "CityInfoApiBearerAuth"
                            }
                        }, new List<string>()
                    }
                });
            });
            #if DEBUG
                services.AddTransient<IMailService,LocalMailService>();
#else
                services.AddTransient<IMailService,CloudMailServices>();
#endif
            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Authentication:SecretForKey"]))
                };
            }
            );

            //Agregar politica de datos
            services.AddAuthorization(option =>
            {
                option.AddPolicy("SoloBogota", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("city", "1");
                });
            });

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });
            services.AddSingleton<CitiesDataStore>();
            services.AddSingleton<FileExtensionContentTypeProvider>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<CityInfoContext>(dbContextOptions => dbContextOptions.UseSqlite(Configuration["ConnectionStrings:CityInfoDbConnectionString"]));
            services.AddScoped<ICityInfoRepository,CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseStaticFiles();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //Donde puede conseguir las especificaciones de la API
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/InfoCityAPI/swagger.json", "Ciudades de Colombia");
                    c.RoutePrefix = string.Empty;

                    c.SwaggerEndpoint("/swagger/DowloadPictures.API/swagger.json", "DowloadPictures.API (Pictures)");
                    c.RoutePrefix = string.Empty;

                    c.DefaultModelExpandDepth(2);
                    c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    c.EnableDeepLinking();
                    c.DisplayOperationId();

                    c.InjectStylesheet("/assets/custom-ui-css");
                });
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
