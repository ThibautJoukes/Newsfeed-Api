using AutoMapper;
using Newsfeed.Application.Articles.Queries.GetArticleDetail;
using Newsfeed.Application.Articles.Queries.GetArticleList;
using Newsfeed.Domain.Entities;
using Newsfeed.Domain.EntityDto;

namespace Newsfeed.Application.Automapper
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<NewsfeedAPIArticleSourceDto, NewsfeedArticleSource>()
                //.ForMember(dest => dest.IdSource, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            MapDtos();
        }

        public void MapDtos()
        {
            CreateMap<NewsfeedArticleDto, NewsfeedArticle>().ReverseMap();
            CreateMap<NewsfeedArticleSourceDto, NewsfeedArticleSource>().ReverseMap();
        }
    }
}
