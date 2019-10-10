using Newsfeed.Persistance.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Application.Interfaces
{
    public interface INewsfeedArticleService
    {
        //TODO: delete
        Task<IEnumerable<NewsfeedArticle>> AddDataArticlesIntoDB();
        Task<NewsfeedArticle> GetArticleByIdAsync(int id);
        Task<IEnumerable<NewsfeedArticle>> GetAllArticlesAsync();
        Task<string> PostArticleAsync(NewsfeedArticle article);
        Task<string> PostArticlesAsync(IEnumerable<NewsfeedArticle> articles);
        Task<string> DeleteArticleAsync(NewsfeedArticle article);
        Task<string> DeleteArticleByIdAsync(int id);
        Task<string> DeleteArticlesAsync(IEnumerable<NewsfeedArticle> articles);
    }
}
