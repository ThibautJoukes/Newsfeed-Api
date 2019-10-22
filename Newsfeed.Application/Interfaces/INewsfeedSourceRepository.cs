using Newsfeed.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Application.Interfaces
{
    public interface INewsfeedSourceRepository
    {
        Task<NewsfeedArticleSource> GetSourceByIdAsync(int id);
        Task<IEnumerable<NewsfeedArticleSource>> GetAllSourcesAsync();
        Task<NewsfeedArticleSource> GetSourceByNameAsync(string name);
    }
}
