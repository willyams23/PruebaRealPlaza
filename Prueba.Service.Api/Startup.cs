using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prueba.Application.MapperProfiles;
using Prueba.Infrastructure.Crosscutting.Encrypt;
using Prueba.Infrastructure.Data.Context;
using Prueba.Infrastructure.Data.Repositories;
using Prueba.Service.Api.Models.ResponseBase;
using Prueba.Service.Api.Models.Settings;
using Prueba.Service.SeedWork;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Service.Api
{
    public class Startup
    {
        const string corsPolicy = "AllowOrigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var instrumentationKey = Configuration.GetSection("ApplicationInsights")["InstrumentationKey"];
            services.AddApplicationInsightsTelemetry(instrumentationKey);

            var securityHeadersEnabled = Boolean.Parse(Configuration["SecurityHeadersEnabled"].ToString());

            if (securityHeadersEnabled)
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(60);
                });
            }

            var corsAllowedClients = Configuration["CorsAllowedClients"].ToString().Split(',', ';');


            var corsOrigins = Configuration.GetSection("AllowedHosts").Value.Split("|||");

            services.AddCors(o => o.AddPolicy(corsPolicy, builder =>
            {
                builder.WithOrigins(corsOrigins)
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            var jwtInternalSecret = Configuration.GetSection("jwt")["InternalSecretKey"];
            var jwtIssuer = Configuration.GetSection("jwt")["Issuer"];
            var jwtAudience = Configuration.GetSection("jwt")["Audience"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtInternalSecret)
                        ),
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                    };
                });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ); ; ;

            services.AddMvc()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        return new BadRequestObjectResult(new ApiBadRequestResponse(actionContext.ModelState));
                    };
                });

            ConfigureInjections(services);

            //Register dapper in scope    
            services.AddScoped<IDapper, Dapperr>();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddMvcCore()
                .AddApiExplorer();

            var httpCompressionEnabled = Boolean.Parse(Configuration["HttpCompressionEnabled"].ToString());

            if (httpCompressionEnabled)
            {
                services.AddResponseCompression(options =>
                {
                    options.Providers.Add<BrotliCompressionProvider>();
                    services.Configure<BrotliCompressionProviderOptions>(options =>
                    {
                        options.Level = CompressionLevel.Fastest;
                    });

                    options.Providers.Add<GzipCompressionProvider>();
                    services.Configure<GzipCompressionProviderOptions>(options =>
                    {
                        options.Level = CompressionLevel.Fastest;
                    });

                    options.MimeTypes =
                        ResponseCompressionDefaults.MimeTypes.Concat(
                            new[] { "text/json" });
                    options.EnableForHttps = true;
                });
            }


            //services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba.Service.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba.Service.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureInjections(IServiceCollection services)
        {
            #region Inject settings

            services.Configure<EncryptionSettings>(Configuration.GetSection("Encryption"));
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));


            #endregion

            #region Inject Encrypt / Jwt

            //services.AddTransient<IJwtUtility, JwtUtility>();
            services.AddTransient<Infrastructure.Crosscutting.Encrypt.Encrypt, Infrastructure.Crosscutting.Encrypt.Encrypt>();

            #endregion

            #region Inject dbcontext

            services.AddAutoMapper(typeof(AutoMapperProfile));

            var sqlConnectionString = Configuration.GetConnectionString("AppDbContext");
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnectionString));

            #endregion

            #region inject Appservices y repositories

            InjectionsSettings.ConfigureServiceCrossCutting(services);
            #endregion

        }
    }
}
