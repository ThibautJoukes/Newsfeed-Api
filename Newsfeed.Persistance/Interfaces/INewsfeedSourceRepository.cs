using Newsfeed.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Newsfeed.Persistance.Interfaces
{
    public interface INewsfeedSourceRepository
    {
        Task<NewsfeedArticleSource> GetSourceByIdAsync(int id);
        Task<IEnumerable<NewsfeedArticleSource>> GetAllSourcesAsync();
        Task<NewsfeedArticleSource> GetSourceByNameAsync(string name);
        Task<NewsfeedArticleSource> GetSourceBySourceIdAsync(string sourceId);
    }
}
