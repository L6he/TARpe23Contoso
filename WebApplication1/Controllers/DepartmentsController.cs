using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var depaato = await _context.Departments.Include(i => i.Administrator).AsNoTracking().FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (depaato == null)
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", depaato.InstructorID);
            return View(depaato);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, byte[] rowVersion)
        {
            ModelState.Remove("RowVersion");
            ModelState.Remove("Courses");
            if (id == null) { return NotFound(); }
            
            var sonoChiNoSadame = await _context.Departments
                               .Include(i => i.Administrator)
              .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (sonoChiNoSadame == null) 
            {
                Department departmentIsDeleted = new Department();
                await TryUpdateModelAsync(departmentIsDeleted);
                ModelState.AddModelError(string.Empty, "unable to shb your shb. Department gone lol");
                ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", departmentIsDeleted.InstructorID);
                return View(departmentIsDeleted);
            }

            _context.Entry(sonoChiNoSadame).Property("RowVersion").OriginalValue = rowVersion;

            var tryUpdate = await TryUpdateModelAsync<Department>(sonoChiNoSadame, "",
                s => s.Name,
                s => s.StartDate,
                s => s.Budget,
                s => s.InstructorID,
                s => s.TotalBodyCount,
                s => s.TotalMoneyLaundered
                );

            if (tryUpdate)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "unable to shb your shb. Department gone shb i love blacks");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("StartDate", $"Current value: {databaseValues.StartDate}");
                        }
                        if (databaseValues.Budget != clientValues.Budget)
                        {
                            ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget}");
                        }
                        if (databaseValues.TotalBodyCount != clientValues.TotalBodyCount)
                        {
                            ModelState.AddModelError("TotalBodyCount", $"Current value: {databaseValues.TotalBodyCount}");
                        }
                        if (databaseValues.TotalMoneyLaundered != clientValues.TotalMoneyLaundered)
                        {
                            ModelState.AddModelError("TotalMoneyLaundered", $"Current value: {databaseValues.TotalMoneyLaundered}");
                        }
                        if (databaseValues.InstructorID != clientValues.InstructorID)
                        {
                            Instructor databaseHasThisInstructoir = await _context.Instructors.FirstOrDefaultAsync(i => i.ID == databaseValues.InstructorID);
                            ModelState.AddModelError("Name", $"Current Value: {databaseValues.InstructorID}");
                        }
                        ModelState.AddModelError(string.Empty, "star platinum" + "the world" + "time has stopped");
                        sonoChiNoSadame.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", sonoChiNoSadame.InstructorID);
            return View(sonoChiNoSadame);
        }
        /*
        //Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit([Bind("Name,Budget,StartDate,TotalMoneyLaundered,TotalBodyCount,InstructorID,RowVersion")] Department dpmt,int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var depaato = await _context.Departments.Include(i => i.Administrator).FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (depaato == null)
            {
                return NotFound();
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", depaato.InstructorID);
            return View();
        }
        //𝓯𝓻𝓮𝓪𝓴𝔂 edit, why is you not working?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Budget,StartDate,TotalMoneyLaundered,TotalBodyCount,InstructorID,RowVersion")] Department depaato, byte[] rowVersion)
        {
            ModelState.Remove("RowVersion");
            ModelState.Remove("Courses");
            ModelState.Remove("Body count nigga");
            ModelState.Remove("Money Laundered");
            if (ModelState.IsValid)
            {
                _context.Departments.Update(depaato);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(depaato);
        }
        */
    }
}
