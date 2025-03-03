using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Chiayin_Yang_Assignment2.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please enter a student name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Remote("CheckEmail", "Validation", ErrorMessage = "Sorry, that email address is already in use")]
        public string Email { get; set; }

        [Required]
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.ConfirmationMessageNotSent; // Initial status

        public int CourseId { get; set; } // Foreign key to Course
        public Course Course { get; set; } // Navigation property to the Course
    }
}
