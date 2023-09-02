using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace RemoteWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        public News Get()
        {
            Thread.Sleep(2000);

            News news = new News();
            news.Topic = "Кошка застряла на дереве.";
            news.TimeOfIncident = DateTime.Now;
            return news;
        }
    }
}
