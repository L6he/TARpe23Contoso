using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        public IActionResult Index()
        {
            return View();
        }
    }
}
