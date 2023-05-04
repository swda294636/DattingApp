using API.Data;
using API.Interface;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection Services, IConfiguration  config)
        {
            Services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            //跨源
            Services.AddCors();
            
            Services.AddScoped<ITokenService,TokenService>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return Services;
        }
    }
}