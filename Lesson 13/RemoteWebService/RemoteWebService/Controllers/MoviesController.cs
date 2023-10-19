using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RemoteWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet]
        public MoviePoster Get()
        {
            Thread.Sleep(2000);

            MoviePoster moviePoster = new MoviePoster();

            List<Movie> movieList = new List<Movie>();
            movieList.Add(new Movie("Интерстеллар", 150));
            movieList.Add(new Movie("Терминатор 1", 500));
            movieList.Add(new Movie("Остров проклятых", 400));
            movieList.Add(new Movie("Зеленая миля", 999));
            movieList.Add(new Movie("Побег из Шоушенга", 555));

            moviePoster.GetMovies = movieList;

            return moviePoster;
        }
    }
}
