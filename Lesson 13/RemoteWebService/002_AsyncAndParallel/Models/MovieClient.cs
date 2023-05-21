using Contracts;
using Newtonsoft.Json;

namespace _002_AsyncAndParallel.Models
{
    public class MovieClient
    {
        private readonly string url = "http://localhost:5279/api/movie";

        public async Task<MoviePoster> GetMoviePosterAsync()
        {
            HttpClient httpClient = new HttpClient();
            var moviePoster = await httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<MoviePoster>(moviePoster);
        }
    }
}