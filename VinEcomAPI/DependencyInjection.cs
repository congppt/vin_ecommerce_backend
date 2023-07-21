using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VinEcomInterface.IService;
using VinEcomService.Service;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using VinEcomAPI.CustomRouteUrl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace VinEcomAPI
{
    public static class DependencyInjection
    {
        public static void InjectWebAPIService(this IServiceCollection services, IConfiguration config)
        {
            // Add services to the container.
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = config["Jwt:Audience"],
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "VinEcomAPIJWTKey"))
                };
            });
            services.AddSingleton(config);
            services.AddHttpContextAccessor();
            services.AddSingleton<IClaimService, ClaimService>();
            //
            services.AddCors();
        }
    }
}
