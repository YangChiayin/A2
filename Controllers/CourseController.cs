using Microsoft.AspNetCore.Mvc;
using Chiayin_Yang_Assignment2.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Chiayin_Yang_Assignment2.Models;

namespace Chiayin_Yang_Assignment2.Controllers
{
    public class CourseController : Controller
    {
        private readonly EnrollmentContext _enrollmentContext;

        public CourseController(EnrollmentContext enrollmentContext)
        {
            _enrollmentContext = enrollmentContext;
        }

        // List all courses
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _enrollmentContext.Courses.Include(c => c.Students).ToList();
            return View(courses); // Show all courses
        }

        // Show form to create a new course
        [HttpGet]
        public IActionResult Add()
        {
            return View(new Course());
        }

        // Handle course creation
        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                _enrollmentContext.Courses.Add(course);
                _enrollmentContext.SaveChanges();

                // Redirect to the Manage action for the newly created course
                return RedirectToAction("Manage", new { courseId = course.CourseId });
            }
            return View(course); // Return the view with validation errors
        }

        // Show form to edit an existing course
        [HttpGet]
        public IActionResult Edit(int courseId)
        {
            var course = _enrollmentContext.Courses.Find(courseId);
            if (course == null) return NotFound();
            return View(course);
        }

        // Handle course editing
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _enrollmentContext.Courses.Update(course);
                _enrollmentContext.SaveChanges();

                // Redirect to the Manage action for the newly created course
                return RedirectToAction("Manage", new { courseId = course.CourseId });
            }
            return View(course);
        }

        // Manage page for course details and enrollment
        [HttpGet]
        public IActionResult Manage(int courseId)
        {
            var course = _enrollmentContext.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course == null) return NotFound();

            var viewModel = new CourseManagementViewModel
            {
                Course = course,
                Student = new Student()
            };

            return View(viewModel);
        }
    }
}