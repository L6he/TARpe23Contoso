using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignments> CourseAssignments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Delinquent> Delinquents { get; set; }
        //public DbSet<AssignedCourseData> AssignedCourseDatas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Instructor>().ToTable("Instructors");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignments");
            modelBuilder.Entity<CourseAssignments>().ToTable("CourseAssignments");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Delinquent>().ToTable("Delinquents");
            //modelBuilder.Entity<AssignedCourseData>().ToTable("AssignedCourseData");
            /*modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");

            //  /\ alternatiivne tabelite nimetusmeetod
            */
        }
    }
}