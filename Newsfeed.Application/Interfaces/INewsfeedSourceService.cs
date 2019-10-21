using Newsfeed.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Newsfeed.Application.Interfaces
{
    public interface INewsfeedSourceService
    {
        Task<NewsfeedArticleSource> GetSourceByIdAsync(int id);
        Task<IEnumerable<NewsfeedArticleSource>> GetAllSourcesAsync();
        Task<NewsfeedArticleSource> GetSourceByNameAsync(string name);
    }
}
