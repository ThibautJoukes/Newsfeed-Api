using AutoMapper;
using Newsfeed.Application.Entities;
using Newsfeed.Application.EntityDtos;
using Newsfeed.Persistance.Entities;

namespace Newsfeed.Api.Automapper
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            //TODO: only need to populate the database, not needed after
            CreateMap<NewsfeedArticle, NewsfeedAPIArticleDto>().ReverseMap();

            CreateMap<NewsfeedAPIArticleSourceDto, NewsfeedArticleSource>()
                .ForMember(dest => dest.IdSource, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
                

            NewsfeedArticleMapping();
            NewsfeedSourceMapping();
        }

        public void NewsfeedArticleMapping()
        {
            CreateMap<NewsfeedArticle, NewsfeedArticleDto>().ReverseMap();
        }
        public void NewsfeedSourceMapping()
        {
            CreateMap<NewsfeedArticleSource, NewsfeedArticleSourceDto>().ReverseMap();
        }
    }
}
