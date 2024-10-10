using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DelinquentController : Controller
    {
        private readonly SchoolContext _context;
        public DelinquentController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delinquents.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["RecentViolation"] = new SelectList(_context.Delinquents);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName, FirstMidName, RecentViolation")] Delinquent delinquent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delinquent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RecentViolation"] = new SelectList(_context.Delinquents);
            return View(delinquent);
        }

        public async Task<IActionResult> Details(int id)
        {
            string query = "SELECT * FROM Delinquents WHERE ID = {0}";
            var delinquent = await _context.Delinquents.FromSqlRaw(query, id)
                .AsNoTracking().FirstOrDefaultAsync();

            if (delinquent == null)
            {
                return NotFound();
            }

            return View(delinquent);
        }

        //Edit GET method that is suspiciously similar to the teacher's edit get method
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var delinquent = await _context.Delinquents.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (delinquent == null)
            {
                return NotFound();
            }
            ViewData["RecentViolation"] = new SelectList(_context.Delinquents);
            return View(delinquent);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var sonoChiNoSadame = await _context.Delinquents
              .FirstOrDefaultAsync(m => m.ID == id);
            if (sonoChiNoSadame == null)
            {
                Delinquent delinquentIsDeleted = new();
                await TryUpdateModelAsync(delinquentIsDeleted);
                ModelState.AddModelError(string.Empty, "JOOOOOOOOOOOOOOOOOOOOJOOO");
                ViewData["RecentViolation"] = new SelectList(_context.Delinquents);
                return View(delinquentIsDeleted);
            }

            var tryUpdate = await TryUpdateModelAsync<Delinquent>(sonoChiNoSadame, "",
                s => s.LastName,
                s => s.FirstMidName,
                s => s.RecentViolation
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
                    var clientValues = (Delinquent)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "faitiin gooorud");
                    }
                    else
                    {
                        var databaseValues = (Delinquent)databaseEntry.ToObject();

                        if (databaseValues.LastName != clientValues.LastName)
                        {
                            ModelState.AddModelError("LastName", $"Current value: {databaseValues.LastName}");
                        }
                        if (databaseValues.FirstMidName != clientValues.FirstMidName)
                        {
                            ModelState.AddModelError("FirstMidName", $"Current value: {databaseValues.FirstMidName}");
                        }
                        if (databaseValues.RecentViolation != clientValues.RecentViolation)
                        {
                            Delinquent databaseHasThisViolation = await _context.Delinquents.FirstOrDefaultAsync(i => i.ID == databaseValues.RecentViolation);
                            ModelState.AddModelError("Name", $"Current Value: {databaseValues.RecentViolation}");
                        }
                        ModelState.AddModelError(string.Empty, "star platinum" + "the world" + "time has stopped");
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["RecentViolation"] = new SelectList(_context.Delinquents);
            return View(sonoChiNoSadame);
        }
    }
}
