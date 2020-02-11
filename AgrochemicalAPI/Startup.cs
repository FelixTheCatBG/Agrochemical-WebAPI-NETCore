using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgrochemicalAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwaggerOptions = AgrochemicalAPI.Options.SwaggerOptions;
using AgrochemicalAPI.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using AgrochemicalAPI.Models;
using AgrochemicalAPI.Services;

namespace AgrochemicalAPI
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
            // Enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            // Token Model
            services.AddScoped<TokenModel>();

            services.AddDbContext<AgrochemicalDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AgrochemicalDbContext")));

            //SQLLite connection string
            //services.AddDbContext<AgrochemicalDbContext>(options => options.UseSqlite("Data Source=agrochemical.db")); 

            services.AddScoped<IDecisionBuilder, DecisionBuilder>();
           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Bearer token configuration
            // Specifiying we are going to use Identity Framework
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<AgrochemicalDbContext>().AddDefaultTokenProviders();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // Authentication Middleware
            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = appSettings.Site,
                    ValidAudience = appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero

                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireLoggedIn", policy => policy.RequireRole("Admin", "Customer", "Moderator").RequireAuthenticatedUser());

                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin").RequireAuthenticatedUser());
            });

            services.AddSwaggerGen(x =>
            {
            x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "AgrochemicalAPI", Version = "v1"});
                //Implementing JWT into swagger
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer",new string[0] }
                };
                x.AddSecurityDefinition(name: "Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using bearer scheme",
                    Name="Authorization",
                    In = "header",
                    Type="apiKey"
                });
                x.AddSecurityRequirement(security);
            });

            //// configure DI for application services
            //services.AddScoped<IUserService, UserService>();
        }

        // Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AgrochemicalDbContext context, UserManager<ApplicationUser> _userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("EnableCORS");

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            //Add authentication to the middleware
            app.UseAuthentication();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });

            app.UseMvc();

            //Seed database with dummy data 
            Initializer.Initialize(context, _userManager).Wait();
        }

    }
}
