using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;

namespace api.Extensions
{
    public static class ServiceExtensions
    {
        //configure SqlServer Connectionstring and DbContext
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString,
             b => b.MigrationsAssembly(typeof(RepositoryContext).Assembly.FullName)));
        }

        #region configure MySql Connectionstring and DbContext
        ////configure MySql Connectionstring and DbContext
        //public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        //{
        //    var connectionString = config["mysqlconnection:connectionString"];
        //    services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString,
        //        MySqlServerVersion.LatestSupportedServerVersion));
        //}
        #endregion


        //configure RepositoryWrapper
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }


        //configure CORS
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        //configure LoggerService
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        //ConfigureIIS
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }


    }



}
