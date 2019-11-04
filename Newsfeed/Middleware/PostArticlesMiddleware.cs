using Microsoft.AspNetCore.Http;
using Newsfeed.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Newsfeed.Api.Middleware
{
    public class PostArticlesMiddleware
    {
        private readonly RequestDelegate _next;

        private MemoryStream _stream;
        private StreamWriter _writer;

        public PostArticlesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Enable seeking
            context.Request.EnableBuffering();

            // Read the stream as text
            var bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();

            // check if id is put as a string
            var articleBody = JsonConvert.DeserializeObject<IEnumerable<NewsfeedAPIArticleDto>>(bodyAsText);

            // set all source id's to Null
            articleBody.Select(c => { c.Source.Id = null; return c; }).ToList();

            // change context body to new body
            _stream = new MemoryStream();
            _writer = new StreamWriter(_stream);

            await _writer.WriteAsync(JsonConvert.SerializeObject(articleBody));
            _writer.Flush();
            _stream.Position = 0;

            context.Request.Body = _stream;


            // Set the position of the stream to 0 to enable rereading
            context.Request.Body.Position = 0;

            if (bodyAsText.Length == 0 || bodyAsText == null)
            {
                await context.Response.WriteAsync("No body found. Make sure you don't forget a body.");
            }
            else
            {
                await _next(context);

                // dispose after context has been send to the controller.
                if (_stream != null) _stream.Dispose();
                if (_writer != null) _writer.Dispose();
            }
        }

    }
}
