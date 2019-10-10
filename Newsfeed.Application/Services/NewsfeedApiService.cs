using Newsfeed.Application.Entities;
using Newsfeed.Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Newsfeed.Application.Services
{
    public class NewsfeedApiService : INewsfeedApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        public NewsfeedApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// This method was only made to run once, to fill up the database with data, should not be used again.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NewsfeedAPIArticleDto>> GetArticlesFromNewsApiAsync()
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = new Uri("https://newsapi.org");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "a4c8252f9d4e4363ad90a43090be172b");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync("v2/top-headlines?country=nl");

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var readAsString = await result.Content.ReadAsStringAsync();

            var outcome = JsonConvert.DeserializeObject<NewsfeedAPIDataDto>(readAsString);

            client.DefaultRequestHeaders.ConnectionClose = true;
            return outcome.Articles;
        }
    }
}
