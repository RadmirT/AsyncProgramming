using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RemoteWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        public async Task<News> Get(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

            News news = new News();
            news.Topic = "Кошка застряла на дереве.";
            news.TimeOfIncident = DateTime.Now;
            return news;
        }
    }
}
