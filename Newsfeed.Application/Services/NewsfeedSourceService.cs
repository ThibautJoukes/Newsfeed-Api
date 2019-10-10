using Newsfeed.Application.Interfaces;
using Newsfeed.Persistance.Entities;
using Newsfeed.Persistance.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Application.Services
{
    public class NewsfeedSourceService : INewsfeedSourceService
    {
        private INewsfeedSourceRepository _newsfeedSourceRepos;
        public NewsfeedSourceService(INewsfeedSourceRepository newsfeedRepos)
        {
            _newsfeedSourceRepos = newsfeedRepos;
        }

        public async Task<NewsfeedArticleSource> GetSourceByIdAsync(int id)
        {
            return await _newsfeedSourceRepos.GetSourceByIdAsync(id);
        }
        public async Task<NewsfeedArticleSource> GetSourceBySourceIdAsync(string sourceId)
        {
            return await _newsfeedSourceRepos.GetSourceBySourceIdAsync(sourceId);
        }
        public async Task<NewsfeedArticleSource> GetSourceByNameAsync(string name)
        {
            return await _newsfeedSourceRepos.GetSourceByNameAsync(name);
        }

        public async Task<IEnumerable<NewsfeedArticleSource>> GetAllSourcesAsync()
        {
            return await _newsfeedSourceRepos.GetAllSourcesAsync();
        }

    }
}
