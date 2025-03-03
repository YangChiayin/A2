using Microsoft.AspNetCore.Mvc;
using Chiayin_Yang_Assignment2.Entities;

namespace Chiayin_Yang_Assignment2.Controllers
{
    public class ValidationController : Controller
    {
        public ValidationController(EnrollmentContext enrollmentContext)
        {
            _enrollmentContext = enrollmentContext;
        }

        public IActionResult CheckEmail(string emailAddress)
        {
            Console.WriteLine($"In check email action for email: {emailAddress}");

            string msg = CheckIfEmailExistsInDb(emailAddress);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else
            {
                return Json(msg);
            }
        }

        private string CheckIfEmailExistsInDb(string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = _enrollmentContext.Students.Where(s => s.Email.ToLower() == email.ToLower()).FirstOrDefault();
                if (customer != null)
                    msg = $"The email address \"{email}\" is already in use.";
            }

            return msg;
        }

        private EnrollmentContext _enrollmentContext;
    }
}