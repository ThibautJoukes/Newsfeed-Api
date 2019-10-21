using Newsfeed.Persistance.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Persistance.Interfaces
{
    public interface INewsfeedArticlesRepository
    {
        // TODO: Must delete after data is loaded into database
        //public Task<IEnumerable<NewsfeedArticle>> AddArticlesToDatabaseAsync();
        Task<NewsfeedArticle> GetArticleByIdAsync(int id);
        Task<IEnumerable<NewsfeedArticle>> StartFilterSearchAsync(NewsfeedArticleFiltering filterArticle);
        Task<IEnumerable<NewsfeedArticle>> GetAllArticlesAsync();
        Task<string> PostArticleAsync(NewsfeedArticle article);
        Task<string> DeleteArticleAsync(NewsfeedArticle article);
        Task<string> DeleteArticleByIdAsync(int id);
        Task<string> DeleteArticlesAsync(IEnumerable<NewsfeedArticle> articles);
    }
}
