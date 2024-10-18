using HPIAM.Application.Interfaces;
using HPIAM.Application.Services;
using HPIAM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HPIAM.Core.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}
