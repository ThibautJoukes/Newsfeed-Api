using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private ILogger _logger;
        private INewsfeedArticleService _newsfeedArticleService;
        private IMapper _mapper;

        public ArticleController(ILogger logger, INewsfeedArticleService newsfeedArticlesService, IMapper mapper)
        {
            _logger = logger;
            _newsfeedArticleService = newsfeedArticlesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            return new OkObjectResult(await _newsfeedArticleService.GetAllArticlesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllArticleById([FromRoute] int id)
        {
            return new OkObjectResult(await _newsfeedArticleService.GetArticleByIdAsync(id));
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetArticlesByFiltering([FromBody] NewsfeedArticleFiltering filter)
        {
            return new OkObjectResult(await _newsfeedArticleService.GetArticleByFilteringAsync(filter));
        }

        [HttpPost]
        [Route("publishArticle")]
        public async Task<IActionResult> PostArticle([FromBody] NewsfeedArticle article)
        {
            return new OkObjectResult(await _newsfeedArticleService.PostArticleAsync(article));
        }

        [HttpPost]
        [Route("publishArticles")]
        public async Task<IActionResult> PostArticles([FromBody] IEnumerable<NewsfeedArticle> article)
        {
            return new OkObjectResult(await _newsfeedArticleService.PostArticlesAsync(article));
        }
    }
}