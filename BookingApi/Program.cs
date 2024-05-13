using BLL.Services.Auth;
using BLL.Services.Reservations;
using BookingApi.Data;
using BookingApi.Repository;
using DAL.Repositories.Accommodations;
using DAL.Repositories.Reservations;
using DAL.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace BookingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor(); // Add HTTP context accessor
            builder.Services.AddSwaggerGen(options =>
            {
                // Configure Swagger
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>(); // Add security requirements operation filter
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Add JWT authentication
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            builder.Services.AddDbContext<DataContext>(options =>
            {
                // Configure the data context with SQL Server and migrations assembly
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), a => a.MigrationsAssembly("DAL"));
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Add AutoMapper
            builder.Services.AddScoped<IUserRepository, UserRepository>(); // Add user repository
            builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>(); // Add apartment repository
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>(); // Add reservations repository

            builder.Services.AddScoped<IReservationService, ReservationService>(); // Add reservation service
            builder.Services.AddScoped<IAuthService, AuthService>();  // Add authentication service

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
