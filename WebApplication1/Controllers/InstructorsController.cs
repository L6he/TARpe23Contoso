using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InstructorsController : Controller 
    {
        private readonly SchoolContext _context;
        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, int? courseId)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Enrollments)
                .ThenInclude(i => i.Student)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

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
        [HttpGet]
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignments>();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor/*, string selectedCourses*/)
        {
            /*if (selectedCourses == null)
            {
                instructor.CourseAssignments = new List<CourseAssignments>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignments
                    {
                        InstructorID = instructor.ID,
                        CourseID = course
                    };
                    instructor.CourseAssignments.Add(courseToAdd);
                }
            }*/
            //ModelState.Remove(selectedCourses);
            
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //PopulateAssignedCourseData(instructor); //updates courses near instructor
            return View(instructor);
        }
        
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses; //finds all courses
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID)); //picks courses based on what courses teachers have
            var viewModel = new List<AssignedCourseData>(); //new list for the viewmodel
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData 
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = viewModel;
        }

        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        //delete POST method, teostab muudatusi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //EDIT GET!!!!!!!!!!
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null)
            {
                return NotFound();
            }
            return View();
        }
        //𝓯𝓻𝓮𝓪𝓴𝔂 EDIT!!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Instructors.Update(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        //"Clone"
        public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

            var duplicateInstructor = new Instructor
            {
                FirstMidName = instructor.FirstMidName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate,
                SocialCredits = instructor.SocialCredits,
                NextPaycheck = instructor.NextPaycheck,
                CorpsesResurrected = instructor.CorpsesResurrected
            };

            if (instructor == null)
            {
                return NotFound();
            }

            if (duplicateInstructor != null)
            {
                _context.Instructors.Add(duplicateInstructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}


