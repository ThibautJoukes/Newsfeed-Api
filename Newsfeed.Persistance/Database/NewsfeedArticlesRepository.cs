using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newsfeed.Persistance.Entities;
using Newsfeed.Persistance.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsfeed.Persistance.Database
{
    /// <summary>
    /// This class is used to make crud calls to the Newsfeed database
    /// </summary>
    public class NewsfeedArticlesRepository : INewsfeedArticlesRepository
    {
        private DbNewsfeedContext _db;
        private IMapper _mapper;
        public NewsfeedArticlesRepository(DbNewsfeedContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<NewsfeedArticle> GetArticleByIdAsync(int id)
        {
            var article = await _db.NewsfeedArticle.Include(x => x.Source)
                                                   .FirstOrDefaultAsync(x => x.Id == id);

            return article;
        }


        public async Task<IEnumerable<NewsfeedArticle>> StartFilterSearchAsync(NewsfeedArticleFiltering filterArticle)
        {
            IQueryable<NewsfeedArticle> articles = _db.NewsfeedArticle;

            if (!string.IsNullOrEmpty(filterArticle.Author))
                articles = articles.Where(art => art.Author.Contains(filterArticle.Author));

            if (!string.IsNullOrEmpty(filterArticle.Title))
                articles = articles.Where(art => art.Title.Contains(filterArticle.Title));

            if (!string.IsNullOrEmpty(filterArticle.Content))
                articles = articles.Where(art => art.Content.Contains(filterArticle.Content));

            return await articles.ToListAsync();
        }
       
        public async Task<IEnumerable<NewsfeedArticle>> GetAllArticlesAsync()
        {
            var articles = await _db.NewsfeedArticle.Include(x => x.Source).ToListAsync();

            return articles;
        }

        public async Task<string> PostArticleAsync(NewsfeedArticle article)
        {
            await this._db.NewsfeedArticle.AddAsync(article);

            await this._db.SaveChangesAsync();

            return "This article has been successfully added to the database.";
        }

        public async Task<string> DeleteArticleAsync(NewsfeedArticle article)
        {
            this._db.NewsfeedArticle.Remove(article);

            await this._db.SaveChangesAsync();

            return "Data has been successfully deleted from the database.";
        }

        public async Task<string> DeleteArticlesAsync(IEnumerable<NewsfeedArticle> articles)
        {
            this._db.NewsfeedArticle.RemoveRange(articles);

            await this._db.SaveChangesAsync();

            return "Data has been successfully deleted from the database.";
        }

        public async Task<string> DeleteArticleByIdAsync(int id)
        {
            NewsfeedArticle article = new NewsfeedArticle() { Id = id };
            this._db.Entry(article).State = EntityState.Deleted;

            await this._db.SaveChangesAsync();

            return "Data has been successfully deleted from the database.";
        }
    }
}
