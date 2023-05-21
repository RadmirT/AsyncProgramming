using _002_AsyncAndParallel.Models;
using Microsoft.AspNetCore.Mvc;
namespace _002_AsyncAndParallel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            ServiceManager serviceManager = new ServiceManager();
            AsyncManager asyncManager = new AsyncManager();

            serviceManager.AddMessage("Начало работы");

            #region 1.0 Async

            await asyncManager.GetNewsAsync(serviceManager);
            await asyncManager.GetMoviePosterAsync(serviceManager);

            #endregion

            #region 2.0 Async&Parallel

            //var task1 = asyncManager.GetNewsAsync(serviceManager);
            //var task2 = asyncManager.GetMoviePosterAsync(serviceManager);
            //await Task.WhenAll(task1, task2);

            #endregion

            serviceManager.AddMessage("Конец работы");

            return View(serviceManager);
        }
    }
}