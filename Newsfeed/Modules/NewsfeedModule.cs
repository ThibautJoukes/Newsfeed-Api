using Autofac;
using Newsfeed.Application.Interfaces;
using Newsfeed.Application.Services;
using Newsfeed.Persistance.Database;
using Newsfeed.Persistance.Interfaces;

namespace Newsfeed.Api.Modules
{
    public class NewsfeedModule: Module
    {    
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterRepositories(builder);
            RegisterServices(builder);
        }

        public void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<NewsfeedArticlesRepository>().As<INewsfeedArticlesRepository>();
            builder.RegisterType<NewsfeedSourceRepository>().As<INewsfeedSourceRepository>();
        }

        public void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<NewsfeedArticleService>().As<INewsfeedArticleService>();
            builder.RegisterType<NewsfeedSourceService>().As<INewsfeedSourceService>();
            builder.RegisterType<NewsfeedApiService>().As<INewsfeedApiService>();
        }
    }
}
