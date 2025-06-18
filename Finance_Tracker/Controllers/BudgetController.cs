using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Controllers
{
    public class BudgetController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
