using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {/*
            //teeb kindlaks, et andmebass tehaks, või oleks olemas
            context.Database.EnsureCreated();
            //kui õpilaste tabelis juba on õpilasi, väljub funktsioonist*/
            if (context.Students.Any())
            {
                return;
            }
            var students = new Student[] {
                new Student { FirstMidName = "Artjom", LastName = "Skatškov", EnrollmentDate = DateTime.Parse("3024-08-20") },
                new Student { FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2057-06-20") },
                new Student { FirstMidName = "Mel", LastName = "Kosk", EnrollmentDate = DateTime.Parse("2024-08-26") },
                new Student { FirstMidName = "Gordon", LastName = "Freeman", EnrollmentDate = DateTime.Parse("1998-11-19") },
                new Student { FirstMidName = "Vanja", LastName = "Vajnšenker", EnrollmentDate = DateTime.Parse("2024-08-20") },
                new Student { FirstMidName = "Jason", LastName = "Angove", EnrollmentDate = DateTime.Parse("2012-02-02") },
                new Student { FirstMidName = "Marko", LastName = "Maripuu", EnrollmentDate = DateTime.Parse("2023-08-20") },
                new Student { FirstMidName = "Tanno", LastName = "Valk", EnrollmentDate = DateTime.Parse("1817-06-20") },
                new Student { FirstMidName = "Shb Shsb", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2057-06-20") }
                };
            context.Students.AddRange(students);
            context.SaveChanges();

            if (context.Students.Any())
            {
                return;
            }
            var courses = new Course[]
            {
                new Course {CourseID=1050, Title="Chemistry", Credits=3},
                new Course {CourseID=20, Title="Biology", Credits=5},
                new Course {CourseID=420, Title="Methemetecs", Credits=500000},
                new Course {CourseID=682, Title="Literature", Credits=28},
                new Course {CourseID=348, Title="Physics", Credits=6000},
                new Course {CourseID=700, Title="Necromancy", Credits=50},
                new Course {CourseID=42069, Title="Basic Programming", Credits=9695},
                new Course {CourseID=0, Title="sh", Credits=1},
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            /*if (context.Enrollments.Any())
            {
                return;
            }
            var enrollments = new Enrollment[]
            {
                new Enrollment {StudentID=1, CourseID=1050, Grade=Grade.F},
                new Enrollment {StudentID=2, CourseID=20, Grade=Grade.A},
                new Enrollment {StudentID=3, CourseID=420, Grade=Grade.A},
                new Enrollment {StudentID=4, CourseID=682, Grade=Grade.A},
                new Enrollment {StudentID=5, CourseID=348, Grade=Grade.F},
                new Enrollment {StudentID=6, CourseID=700, Grade=Grade.A},
                new Enrollment {StudentID=7, CourseID=42069, Grade=Grade.A},
                new Enrollment {StudentID=8, CourseID=0, Grade=Grade.C},
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();*/

            if (context.Instructors.Any() )
            {
                return;
            }
            var instructors = new Instructor[] {
                new Instructor { FirstMidName = "Dio", LastName = "Brando", HireDate = DateTime.Parse("3024-08-20"), SocialCredits = -5000000 },
                new Instructor { FirstMidName = "Jotaro", LastName = "Kujo", HireDate = DateTime.Parse("2057-06-20"), SocialCredits = 5000000 },
                new Instructor { FirstMidName = "Jean Pierre", LastName = "Polnareff", HireDate = DateTime.Parse("2024-08-26"), SocialCredits = 200 },
                new Instructor { FirstMidName = "Gordon", LastName = "Freeman", HireDate = DateTime.Parse("1998-11-19"), SocialCredits = -1000 },
                new Instructor { FirstMidName = "Vanja", LastName = "Vajnšenker", HireDate = DateTime.Parse("2024-08-20"), SocialCredits = -100000000 },
                new Instructor { FirstMidName = "Jason", LastName = "Angove", HireDate = DateTime.Parse("2012-02-02"), SocialCredits = 500 },
                new Instructor { FirstMidName = "I'm out of", LastName = "Ideas", HireDate = DateTime.Parse("2023-08-20"), SocialCredits = -5000000 },
                new Instructor { FirstMidName = "John Dark", LastName = "Souls", HireDate = DateTime.Parse("1817-06-20"), SocialCredits = -5000000 },
                new Instructor { FirstMidName = "Joseph", LastName = "Joestar", HireDate = DateTime.Parse("2057-06-20"), SocialCredits = -5000000 }
                };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            if (context.Departments.Any()) { return; }
            var departments = new Department[]
            {
                new Department
                {
                    Name = "Retardism",
                    Budget = 200,
                    StartDate = DateTime.Parse("2001-03-03"),
                    TotalMoneyLaundered = 500,
                    TotalBodyCount = 24,
                },
                new Department
                {
                    Name = "Theoretical Physics",
                    Budget = 5000000000,
                    StartDate = DateTime.Parse("1998-11-18"),
                    TotalMoneyLaundered = 0,
                    TotalBodyCount = 800000000
                },
                new Department
                {
                    Name = "Honey mustardless burgering",
                    Budget = 1,
                    StartDate = DateTime.Parse("2022-03-04"),
                    TotalMoneyLaundered = 0,
                    TotalBodyCount = 0
                }
            }; 
            context.Departments.AddRange(departments);
            context.SaveChanges();

            if (context.Delinquents.Any())
            {
                return;
            }
            var delinquents = new Delinquent[] {
                new Delinquent { LastName = "Belušić", FirstMidName = "Adam", RecentViolation = Violation.Expelled },
                new Delinquent { LastName = "Vajnšenker", FirstMidName = "Vanja", RecentViolation = Violation.Suspended },
                new Delinquent { LastName = "Angove", FirstMidName = "Jason", RecentViolation = Violation.Nerdy },
                };
            context.Delinquents.AddRange(delinquents);
            context.SaveChanges(); 

            /*
            //objekt õpilastega, mis lisatakse siis, kui õpilasi sisestatud ei ole
            var students = new Student[]
            {
                new Student {FirstMidName="Artjom", LastName="Skatškov", EnrollmentDate=DateTime.Parse("3024-08-29")},
                new Student {FirstMidName="Meredith", LastName="Alonso", EnrollmentDate=DateTime.Parse("2057-06-31")},
                new Student {FirstMidName="Mel", LastName="Kosk", EnrollmentDate=DateTime.Parse("2024-08-26")},
                new Student {FirstMidName="Gordon", LastName="Freeman", EnrollmentDate=DateTime.Parse("1998-11-19")},
                new Student {FirstMidName="Vanja", LastName="Vajnšenker", EnrollmentDate=DateTime.Parse("2024-08-29")},
                new Student {FirstMidName="Jason", LastName="Angove", EnrollmentDate=DateTime.Parse("2012-02-02")},
                new Student {FirstMidName="Marko", LastName="Maripuu", EnrollmentDate=DateTime.Parse("2023-08-27")},
                new Student {FirstMidName="Tanno", LastName="Valk", EnrollmentDate=DateTime.Parse("1817-06-31")},
                new Student {FirstMidName="Shb Shsb", LastName="Alonso", EnrollmentDate=DateTime.Parse("2057-06-31")},
            };

            //iga õpilane lisatakse ükshaaval läbi foreach tsükli
            foreach (Student student in students)
            {
                context.Students.Add(student);

            }
            //ja andmebaasi muudataused salvestatakse
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course {CourseID=1050, Title="Chemistry", Credits=3},
                new Course {CourseID=20, Title="Biology", Credits=5},
                new Course {CourseID=420, Title="Methemetecs", Credits=500000},
                new Course {CourseID=682, Title="Literature", Credits=28},
                new Course {CourseID=348, Title="Physics", Credits=6000},
                new Course {CourseID=700, Title="Necromancy", Credits=50},
                new Course {CourseID=42069, Title="Basic Programming", Credits=9695},
                new Course {CourseID=0, Title="sh", Credits=1},
            };
            foreach (Course course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {StudentID=1, CourseID=1050, Grade=Grade.F},
                new Enrollment {StudentID=2, CourseID=20, Grade=Grade.A},
                new Enrollment {StudentID=3, CourseID=420, Grade=Grade.A},
                new Enrollment {StudentID=4, CourseID=682, Grade=Grade.A},
                new Enrollment {StudentID=5, CourseID=348, Grade=Grade.F},
                new Enrollment {StudentID=6, CourseID=700, Grade=Grade.A},
                new Enrollment {StudentID=7, CourseID=42069, Grade=Grade.A},
                new Enrollment {StudentID=8, CourseID=0, Grade=Grade.C},
            };
            foreach ( Enrollment enrollment in enrollments)
            {
                context.Enrollments.Add(enrollment);
            }
            context.SaveChanges();*/
        }
    }
}
