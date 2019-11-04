using AutoMapper;
using MediatR;
using Newsfeed.Application.Interfaces;
using Newsfeed.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Newsfeed.Application.Articles.Commands.CreateArticle
{

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
    {
        private readonly INewsfeedArticlesRepository _articles;
        private readonly INewsfeedSourceRepository _sourseRepos;
        private readonly IMapper _IMapper;

        public CreateArticleCommandHandler(INewsfeedArticlesRepository articles, INewsfeedSourceRepository sourseRepos, IMapper IMapper)
        {
            _articles = articles;
            _sourseRepos = sourseRepos;
            _IMapper = IMapper;
        }

        async Task<Unit> IRequestHandler<CreateArticleCommand, Unit>.Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = new NewsfeedArticle
            {
                Id = default,
                CurrentSourceId = request.CurrentSourceId,
                Author = request.Author,
                Title = request.Title,
                Source = new NewsfeedArticleSource()
                {
                    Id = request.Source.Id,
                    Name = request.Source.Name
                },
                Url = request.Url,
                UrlToImage = request.UrlToImage,
                Description = request.Description,
                Content = request.Content,
                PublishedAt = request.PublishedAt
            };

            // validates the source inside the article
            var article = await GetArticleWithCorrectedSource(entity);

            await _articles.PostArticleAsync(article);

            return Unit.Value;
        }

        /// <summary>
        /// Checks if the source already exists in the database. Source of article will be set to Null if the source already exits.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public async Task<NewsfeedArticle> GetArticleWithCorrectedSource(NewsfeedArticle article)
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
            var source = await _sourseRepos.GetSourceByNameAsync(articleSource.Name);

            return source ?? null;
        }
    }
}
