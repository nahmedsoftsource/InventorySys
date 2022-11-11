using Autofac;
using Application.SharedKernal;

namespace Application.Autofact
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetAssembly(typeof(AppService))).AsSelf();
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //.AsClosedTypesOf(typeof(IRepository<>));
            //  builder.RegisterModule<ServiceModule>();
            // builder.RegisterModule<HandlerModule>();
            builder.RegisterModule<RepositoryModule>();
        }
    }
}
