using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InstructorController : Controller 
    {
        private readonly SchoolContext _context;
        public InstructorController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, int? courseId)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await _context.Instructors.Include(i => i.OfficeAssignment).Include(i => i.CourseAssignments).ThenInclude(i => i.Course).ThenInclude(i => i.Enrollments).ThenInclude(i => i.Student)
                .Include(i => i.CourseAssignments).ThenInclude(i => i.Course).AsNoTracking().OrderBy(i => i.LastName).ToListAsync();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(i => i.ID == id.Value).Single();
                viewModel.Courses = instructor.CourseAssignments.Select(i => i.Course);
            }
            if (courseId != null)
            {
                ViewData["CourseID"] = courseId.Value;
                viewModel.Enrollments = viewModel.Courses.Where(x => x.CourseID == courseId.Value).Single().Enrollments;
            }
            return View(viewModel);
        }
    }
}
