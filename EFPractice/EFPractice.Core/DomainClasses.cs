using System;
using System.Collections.Generic;

namespace EFPractice.Core
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public virtual List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual List<Instructor> Instructors { get; private set; }


        public Course()
        {
            Instructors = new List<Instructor>();
        }
    }

    public class OnlineCourse : Course
    {
        public string Url { get; set; }
    }

    public class OnSiteCourse : Course
    {
        public Details Details { get; set; }
    }

    public class Details
    {
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public int DurationDays { get; set; }
    }

    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EGender Gender { get; set; }

        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }

        public int? AssignedParkingId { get; set; }
        public AssignedParking AssignedParking { get; set; }

        public virtual List<Course> Courses { get; private set; }

        public Instructor()
        {
            Courses = new List<Course>();
        }
    }

    public enum EGender
    {
        Male = 1,
        Female
    }

    public class Office
    {
        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }

        public string Location { get; set; }
        public Byte[] Timestamp { get; set; }
    }

    public class AssignedParking
    {
        public string SpaceNumber { get; set; }

        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}