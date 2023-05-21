namespace _002_AsyncAndParallel.Models
{
    public class AsyncManager
    {
        public async Task GetNewsAsync(ServiceManager serviceManager)
        {
            serviceManager.AddMessage("Запрос на получение новостей");
            NewsClient newsClient = new NewsClient();
            serviceManager.News = await newsClient.GetNewsAsync();
            serviceManager.AddMessage("Новости получены");
        }

        public async Task GetMoviePosterAsync(ServiceManager serviceManager)
        {
            serviceManager.AddMessage("Запрос на получения киноафиши");
            MovieClient movieClient = new MovieClient();
            serviceManager.MoviePoster = await movieClient.GetMoviePosterAsync();
            serviceManager.AddMessage("Киноафиша получена");
        }
    }
}