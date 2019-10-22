using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsfeed.Application.Interfaces;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Newsfeed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        private ILogger _logger;
        private INewsfeedArticleService _articleService;
        private readonly IMediator _mediator;

        public PingController(ILogger logger, INewsfeedArticleService articleService, IMediator mediator)
        {
            _logger = logger;
            _articleService = articleService;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            //string result = await _mediator.Send(new Ping());
            //var articles = await _articleService.AddDataArticlesIntoDBAsync();
            return new OkObjectResult("Ping ping, server staat online!");
        }
    }

    //public class Ping : IRequest<string> {
    //}

    //public class PingHandler : IRequestHandler<Ping, string>
    //{
    //    public Task<string> Handle(Ping request, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult("Pong");
    //    }
    //}
}