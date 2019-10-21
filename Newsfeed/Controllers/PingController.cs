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
        private INewsfeedArticleService _articleService;

        public PingController(ILogger logger, INewsfeedArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            //var articles = await _articleService.AddDataArticlesIntoDBAsync();
            return new OkObjectResult("Api staat online!");
        }
    }
}