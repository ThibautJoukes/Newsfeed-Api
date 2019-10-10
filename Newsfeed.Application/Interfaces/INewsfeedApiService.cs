using Newsfeed.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Application.Interfaces
{
    public interface INewsfeedApiService
    {
        Task<IEnumerable<NewsfeedAPIArticleDto>> GetArticlesFromNewsApiAsync();
    }
}
