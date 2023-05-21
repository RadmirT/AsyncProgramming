using Contracts;
using Microsoft.AspNetCore.Mvc;
using RemoteWebService.Models;

namespace RemoteWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        // GET api/people
        [HttpGet]
        public async Task<List<Person>> Get(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            PersonManager personManager = new PersonManager();
            var people = personManager.GetPeople();
            return people;
        }
    }
}
