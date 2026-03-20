using Microsoft.AspNetCore.Mvc;

namespace PruebaEmi.Controllers
{
    public class EmployeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
