using Microsoft.AspNetCore.Mvc;
using Newsfeed.Application.Interfaces;
using Newsfeed.Persistance.Entities;
using Serilog;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newsfeed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        private ILogger _logger;
        private INewsfeedArticleService _newsfeedArticleService;

        public PingController(ILogger logger, INewsfeedArticleService newsfeedArticlesService)
        {
            _logger = logger;
            _newsfeedArticleService = newsfeedArticlesService;
        }

        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            //var resultArticles = await _newsfeedArticleService.AddDataArticlesIntoDB();

            return new OkObjectResult(await _newsfeedArticleService.GetAllArticlesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Ping([FromBody] NewsfeedArticle article)
        {
            return new OkObjectResult(await _newsfeedArticleService.PostArticleAsync(article));
        }
    }
}