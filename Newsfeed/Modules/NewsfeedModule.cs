using Autofac;
using MediatR;
using Newsfeed.Application.Interfaces;
using Newsfeed.Persistance.Database;
using System.Reflection;

namespace Newsfeed.Api.Modules
{
    // Module is refence to system.reflection and autofac... that's why we specify which one we're going to use
    public class NewsfeedModule: Autofac.Module
    {    
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterRepositories(builder);
            //RegisterMediatR(builder);
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<NewsfeedArticlesRepository>().As<INewsfeedArticlesRepository>();
            builder.RegisterType<NewsfeedSourceRepository>().As<INewsfeedSourceRepository>();
        }

        private void RegisterMediatR(ContainerBuilder builder)
        {
            // dispatcher itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(IRequestHandler<,>).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
        }
    }
}
