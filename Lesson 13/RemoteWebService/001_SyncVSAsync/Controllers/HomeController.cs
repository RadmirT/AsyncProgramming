using Contracts;
using Microsoft.AspNetCore.Mvc;
using SyncVSAsync.Models;
using System.Diagnostics;

namespace SyncVSAsync.Controllers
{
    public class HomeController(PersonClient personClient) : Controller
    {
        public ActionResult Index()
        {
            return View();
        }   

        [Route("Home/LoadSync")]
        public ActionResult LoadSync()
        {
            Stopwatch timer = Stopwatch.StartNew();
            List<Person> people = personClient.GetPeople();
            ViewBag.Timer = timer;
            return View(people);
        }

        [Route("Home/LoadAsync")]
        public async Task<ActionResult> LoadAsync()
        {
            Stopwatch timer = Stopwatch.StartNew();
            List<Person> people = await personClient.GetPeopleAsync();
            ViewBag.Timer = timer;
            return View("Load", people);
        }
    }
}
