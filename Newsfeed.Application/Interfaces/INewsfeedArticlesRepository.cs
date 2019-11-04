using Newsfeed.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Application.Interfaces
{
    public interface INewsfeedArticlesRepository
    {
        Task<NewsfeedArticle> GetArticleByIdAsync(int id);
        Task<IEnumerable<NewsfeedArticle>> StartFilterSearchAsync(NewsfeedArticleFiltering filterArticle);
        Task<IEnumerable<NewsfeedArticle>> GetAllArticlesAsync();
        Task PostArticleAsync(NewsfeedArticle article);
        Task DeleteArticleAsync(NewsfeedArticle article);
        Task DeleteArticleByIdAsync(int id);
        Task DeleteArticlesAsync(IEnumerable<NewsfeedArticle> articles);
    }
}
