using Contracts;
using Newtonsoft.Json;

namespace _002_AsyncAndParallel.Models
{
    public class NewsClient
    {
        private readonly string url = "http://localhost:5279/api/news";

        public async Task<News> GetNewsAsync()
        {
            HttpClient httpClient = new HttpClient();
            var news = await httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<News>(news);
        }
    }
}