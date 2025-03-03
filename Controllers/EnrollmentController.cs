using Microsoft.AspNetCore.Mvc;
using Chiayin_Yang_Assignment2.Entities;
using Chiayin_Yang_Assignment2.Services;
using Microsoft.EntityFrameworkCore;
using Chiayin_Yang_Assignment2.Models;

namespace Chiayin_Yang_Assignment2.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly EnrollmentContext _enrollmentContext;
        private readonly IEmailService _emailService;

        public EnrollmentController(
            EnrollmentContext enrollmentContext,
            IEmailService emailService)
        {
            _enrollmentContext = enrollmentContext;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(CourseManagementViewModel viewModel)
        {
            if (viewModel.Course == null || viewModel.Course.CourseId == 0)
            {
                return BadRequest("Course ID is missing.");
            }

            var course = _enrollmentContext.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.CourseId == viewModel.Course.CourseId);

            if (course == null)
            {
                return NotFound();
            }

            // Server-side duplicate email check
            if (_enrollmentContext.Students.Any(s => s.Email == viewModel.Student.Email && s.CourseId == course.CourseId))
            {
                ModelState.AddModelError("Student.Email", "The email address is already in use.");

                return View("/Views/Course/Manage.cshtml", new CourseManagementViewModel { Course = course, Student = viewModel.Student });
            }


            var newStudent = new Student
            {
                Name = viewModel.Student.Name,
                Email = viewModel.Student.Email,
                Status = EnrollmentStatus.ConfirmationMessageNotSent,
                CourseId = course.CourseId
            };

            course.Students.Add(newStudent);
            _enrollmentContext.SaveChanges();

            return RedirectToAction("Manage", "Course", new { courseId = course.CourseId });
        }



        [HttpPost]
        public async Task<IActionResult> SendConfirmationMessages(int courseId)
        {

            try
            {
                var course = await _enrollmentContext.Courses
                    .Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (course == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction("Manage", "Course", new { courseId });
                }

                var studentsToConfirm = course.Students
                    .Where(s => s.Status == EnrollmentStatus.ConfirmationMessageNotSent)
                    .ToList();


                foreach (var student in studentsToConfirm)
                {
                    try
                    {
                        var confirmationLink = Url.Action("ConfirmEnrollment", "Enrollment",
                            new { studentId = student.StudentId }, Request.Scheme);

                        await _emailService.SendEnrollmentConfirmationAsync(student, course, confirmationLink);
                        student.Status = EnrollmentStatus.ConfirmationMessageSent;
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"Error sending email to {student.Email}: {ex.Message}";
                    }
                }

                await _enrollmentContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Confirmation messages sent successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error processing confirmation messages: {ex.Message}";
            }

            return RedirectToAction("Manage", "Course", new { courseId });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEnrollment(int studentId)
        {

            var student = await _enrollmentContext.Students
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View("RespondToEnrollment", student);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitResponse(int studentId, bool confirmed)
        {

            try
            {
                var student = await _enrollmentContext.Students.FindAsync(studentId);
                if (student == null)
                {
                    return NotFound();
                }

                student.Status = confirmed ? EnrollmentStatus.EnrollmentConfirmed : EnrollmentStatus.EnrollmentDeclined;
                await _enrollmentContext.SaveChangesAsync();

                return View("ResponseConfirmation");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your response.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}