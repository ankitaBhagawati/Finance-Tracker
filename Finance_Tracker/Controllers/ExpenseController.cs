using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
