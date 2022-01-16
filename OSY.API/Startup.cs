using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OSY.API.Infrastucture;
using OSY.Service.ApartmentServiceLayer;
using OSY.Service.BillServiceLayer;
using OSY.Service.HousingServiceLayer;
using OSY.Service.Job;
using OSY.Service.ResidentServiceLayer;
using System;
using System.Text;

namespace OSY.API
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
            //Setup Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc();

            //Hangfire kuruldu
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OSY.API", Version = "v1" });
            });

            //Mapper in konfigusrayonuna kendi mapperimin konfigurasyonunu veriyorum. Kendi mapperimi kullanacagimi bildiriyorum.
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(profile: new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper(); // mapper olusturuyorum
            services.AddSingleton(mapper); //projeme injekt ediyorum

            //Dependency Injection
            services.AddTransient<IResidentService, ResidentService>();
            services.AddTransient<IHousingService, HousingService>();
            services.AddTransient<IApartmentService, ApartmentService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IEmailOperations, EmailOperations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OSY.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            /*backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire Job!"));
            recurringJobManager.AddOrUpdate("EmailOperation",
                () => serviceProvider.GetService<IEmailOperations>().sendWelcomeEmail(),
                "* * * * * "); //hergün sendEmail methodunu çalýþtýrýyoruz*/
        }
    }
}
