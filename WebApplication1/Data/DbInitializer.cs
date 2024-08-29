﻿using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            //teeb kindlaks, et andmebass tehaks, või oleks olemas
            context.Database.EnsureCreated();
            //kui õpilaste tabelis juba on õpilasi, väljub funktsioonist
            if (context.Students.Any())
            {
                return;
            }

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
            context.SaveChanges();
        }
    }
}
