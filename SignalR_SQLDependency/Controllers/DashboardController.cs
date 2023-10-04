using Microsoft.AspNetCore.Mvc;

namespace SignalR_SQLTableDependency.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
