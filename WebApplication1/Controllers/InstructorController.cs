using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
