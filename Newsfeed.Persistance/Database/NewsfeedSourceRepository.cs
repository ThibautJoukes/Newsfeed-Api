using Microsoft.EntityFrameworkCore;
using Newsfeed.Application.Interfaces;
using Newsfeed.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsfeed.Persistance.Database
{
    public class NewsfeedSourceRepository : INewsfeedSourceRepository
    {
        private DbNewsfeedContext _db;
        public NewsfeedSourceRepository(DbNewsfeedContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<NewsfeedArticleSource>> GetAllSourcesAsync()
        {
            var sources = await (from a in _db.NewsfeedArticleSource
                                select a).ToListAsync();

            return sources;
        }

        public async Task<NewsfeedArticleSource> GetSourceByIdAsync(int id)
        {
            var source = await (from a in _db.NewsfeedArticleSource
                                where a.Id == id
                                select a).FirstOrDefaultAsync();

            return source;
        }

        public async Task<NewsfeedArticleSource> GetSourceByNameAsync(string name)
        {
            var source = await _db.NewsfeedArticleSource.Where(x => x.Name == name).FirstOrDefaultAsync();

            return source;
        }
    }
}
