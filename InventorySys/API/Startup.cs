using Autofac;
using Application.Autofact;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.IoC;
using GenericHelpers.Services;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")), ServiceLifetime.Transient); //,  ServiceLifetime.Transient

            RegisterServices(services, Configuration.GetConnectionString("DbConnection"));

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddFile("Logs/mylog-{Date}.txt");
            var configuration = Configuration.GetSection("Logging"); //log levels set in your configuration
            loggerFactory.AddFile(configuration);

           // var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
           // app.UseRequestLocalization(locOptions.Value);


            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            



            app.UseHttpsRedirection();

            app.UseRouting();

          //  app.UseAuthorization();

            HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static void RegisterServices(IServiceCollection services, string connectionString)
        {
            DependencyContainer.RegisterServices(services, connectionString);
        }


    }
}
