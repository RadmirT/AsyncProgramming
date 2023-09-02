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
        public List<Person> Get()
        {
            Thread.Sleep(3000);

            PersonManager personManager = new PersonManager();
            var people = personManager.GetPeople();
            return people;
        }
    }
}
