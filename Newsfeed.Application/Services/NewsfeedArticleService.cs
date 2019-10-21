using AutoMapper;
using Newsfeed.Application.Interfaces;
using Newsfeed.Persistance.Entities;
using Newsfeed.Persistance.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsfeed.Application.Services
{
    public class NewsfeedArticleService : INewsfeedArticleService
    {
        private INewsfeedArticlesRepository _newsfeedArticleRepos;
        private INewsfeedSourceService _SourceService;
        private IMapper _mapper;
        private INewsfeedApiService _newsfeedAPI;

        public NewsfeedArticleService(INewsfeedArticlesRepository newsfeedRepos,
            INewsfeedSourceService sourceService, IMapper mapper, INewsfeedApiService newsfeedApi)
        {
            _newsfeedArticleRepos = newsfeedRepos;
            _SourceService = sourceService;
            _mapper = mapper;
            _newsfeedAPI = newsfeedApi;
        }

        //! Only run once, populate's database
        public async Task<IEnumerable<NewsfeedArticle>> AddDataArticlesIntoDBAsync()
        {
            var articlesFromApi = await _newsfeedAPI.GetArticlesFromNewsApiAsync();
            // make id NULL cuz we autocreate our own id as intiger and not as string which is done by the NewsApi
            articlesFromApi.Select(s => { s.Source.Id = null; return s; }).ToList();
            IEnumerable<NewsfeedArticle> listArticles = _mapper.Map<IEnumerable<NewsfeedArticle>>(articlesFromApi);

            // Article validation
            //! because we need to validate on the id of all articles in DB, Bulk makes this difficult
            foreach (var article in listArticles)
            {
                await this.PostArticleAsync(article);
            }

            return listArticles;
        }

        public async Task<NewsfeedArticle> GetArticleByIdAsync(int id)
        {
            return await _newsfeedArticleRepos.GetArticleByIdAsync(id);
        }

        public async Task<IEnumerable<NewsfeedArticle>> GetArticleByFilteringAsync(NewsfeedArticleFiltering article)
        {
            return await _newsfeedArticleRepos.StartFilterSearchAsync(article);
        }

        public async Task<IEnumerable<NewsfeedArticle>> GetAllArticlesAsync()
        {
            return await _newsfeedArticleRepos.GetAllArticlesAsync();
        }

        public async Task<string> PostArticleAsync(NewsfeedArticle article)
        {
            // validates the source inside the article
            article = await ValidateSourceAsync(article);

            return await _newsfeedArticleRepos.PostArticleAsync(article);
        }

        public async Task<string> PostArticlesAsync(IEnumerable<NewsfeedArticle> articles)
        {
            string message = "";
            foreach (var article in articles)
            {
               message = await this.PostArticleAsync(article);
            }
            return message;
        }

        public async Task<string> DeleteArticleAsync(NewsfeedArticle article)
        {
            return await _newsfeedArticleRepos.DeleteArticleAsync(article);
        }

        public async Task<string> DeleteArticlesAsync(IEnumerable<NewsfeedArticle> articles)
        {
            return await _newsfeedArticleRepos.DeleteArticlesAsync(articles);
        }

        public async Task<string> DeleteArticleByIdAsync(int id)
        {
            return await _newsfeedArticleRepos.DeleteArticleByIdAsync(id);
        }

        /// <summary>
        /// Checks if the source already exists in the database. Source of article will be set to Null if the source already exits.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        private async Task<NewsfeedArticle> ValidateSourceAsync(NewsfeedArticle article)
        {
            NewsfeedArticleSource existingSource = await CheckIfSourceExistAsync(article.Source);

            if (existingSource != null)
            {
                // Set the foreign key from article to the existing source
                article.CurrentSourceId = existingSource.Id ?? 0; // ignore the ?? 0 - has no purpose except for disbling an error
                // Delete the source so it won't be added to the database
                article.Source = null;
            }
            return article;
        }

        private async Task<NewsfeedArticleSource> CheckIfSourceExistAsync(NewsfeedArticleSource articleSource)
        {
            // check if source id exists
            var sources = await _SourceService.GetAllSourcesAsync();
            // check on name
            var source = sources.Where(x => x.Name == articleSource.Name).FirstOrDefault();

            return source ?? null;
        }
    }
}
