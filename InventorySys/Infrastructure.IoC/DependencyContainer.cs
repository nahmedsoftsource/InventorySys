using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(new DBConnectionFactory(db => db.ConnectionString = connectionString));
        }
    }
}