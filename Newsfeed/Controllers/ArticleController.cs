using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsfeed.Application.Articles.Commands.CreateArticle;
using Newsfeed.Application.Articles.Queries.GetArticleDetail;
using Newsfeed.Application.Articles.Queries.GetArticleList;
using Newsfeed.Application.Articles.Queries.GetFilteredArticle;
using Newsfeed.Application.Interfaces;
using Newsfeed.Domain.Entities;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private INewsfeedArticlesRepository _newsfeedArticleRepos;
        private IMapper _mapper;
        private IMediator _mediator;

        public ArticleController(INewsfeedArticlesRepository newsfeedArticleRepos, 
            IMapper mapper,
            IMediator mediator)
        {
            _newsfeedArticleRepos = newsfeedArticleRepos;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            return new OkObjectResult(await _mediator.Send(new GetArticlesListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllArticleById([FromRoute] int id)
        {
            return new OkObjectResult(await _mediator.Send(new GetArticleDetailQuery() { Id = id }));
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetArticlesByFiltering([FromBody] GetArticleFilterQuery filter)
        {
            return new OkObjectResult(await _mediator.Send(filter));
            //return new OkObjectResult(await _newsfeedArticleRepos.StartFilterSearchAsync(filter));
        }

        [HttpPost]
        [Route("publishArticle")]
        public async Task<IActionResult> PostArticle([FromBody] CreateArticleCommand article)
        {
            return new OkObjectResult(await _mediator.Send(article));
        }

        [HttpPost]
        [Route("publishArticles")]
        public async Task<IActionResult> PostArticles([FromBody] IEnumerable<CreateArticleCommand> articles)
        {
            object result = null;

            foreach (var article in articles)
            {
                result = await _mediator.Send(article);
            }
            return new OkObjectResult(result);
        }
    }
}