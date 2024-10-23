using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Migrations;
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
            if (id == null)
            {
                return NotFound();
            }
            var delinquent = await _context.Delinquents.FirstOrDefaultAsync(m => m.ID == id);

            if (delinquent == null)
            {
                return NotFound();
            }
            return View(delinquent);
        }

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
        public async Task<IActionResult> Edit([Bind("ID,LastName,FirstMidName,RecentViolation")] Delinquent delinquent)
        {
            if (ModelState.IsValid)
            {
                _context.Delinquents.Update(delinquent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RecentViolation"] = new SelectList(_context.Delinquents);
            return View(delinquent);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delinquent = await _context.Delinquents.FirstOrDefaultAsync(x => x.ID == id);

            if (delinquent == null)
            {
                return NotFound();
            }
            return View(delinquent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delinquent = await _context.Delinquents.FindAsync(id);

            _context.Delinquents.Remove(delinquent);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Honor(int id)
        {
            if (id == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                
                var delinquentToChange = await _context.Delinquents.FirstOrDefaultAsync(d => d.ID == id);
                delinquentToChange.RecentViolation = Violation.Clean;
                _context.Delinquents.Update(delinquentToChange);
                await _context.SaveChangesAsync();
                return View(delinquentToChange);
            }
            return RedirectToAction("Index");
        }
    }
}
