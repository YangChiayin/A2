using System.ComponentModel.DataAnnotations;

namespace Chiayin_Yang_Assignment2.Entities
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please enter a course name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Instructor name")]
        public string Instructor { get; set; }

        [Required(ErrorMessage = "Please enter a start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter a room number")]
        [RegularExpression(@"^\d[A-Z]\d{2}$", ErrorMessage = "Room number must be in the format '4G15' or '1C01'.")]
        public string RoomNumber { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();  // List of students enrolled in the course

    }
}
