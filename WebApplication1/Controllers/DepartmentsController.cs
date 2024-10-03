using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;
        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var SchoolContext = _context.Departments.Include(x => x.Administrator);
            return View(await SchoolContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null) 
            { 
                return NotFound();
            }

            string query = "SELECT * FROM Departments WHERE DepartmentID = {0}";
            var department = await _context.Departments.FromSqlRaw(query, id)
                .Include(d => d.Administrator)
                .AsNoTracking().FirstOrDefaultAsync();

            if (department == null) 
            {
                return NotFound();
            }

            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Budget,StartDate,TotalMoneyLaundered,TotalBodyCount,InstructorID,RowVersion")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depaato = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentID == id);

            if (depaato == null) 
            {
                return NotFound();
            }
            return View(depaato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depaato = await _context.Departments.FindAsync(id);
            
            _context.Departments.Remove(depaato);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var depaato = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (depaato == null)
            {
                return NotFound();
            }
            return View();
        }
        //𝓯𝓻𝓮𝓪𝓴𝔂 edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Budget,StartDate,TotalMoneyLaundered,TotalBodyCount,InstructorID,RowVersion")] Department depaato)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(depaato);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(depaato);
        }
    }
}
