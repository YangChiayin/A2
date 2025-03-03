using Chiayin_Yang_Assignment2.Entities;
using System.Net;
using System.Net.Mail;
namespace Chiayin_Yang_Assignment2.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _fromAddress = "joy851026@gmail.com"; // Your Gmail address
        private readonly string _appPassword = "fjda scca oksg xevp"; // Your app-specific password

        public async Task SendEnrollmentConfirmationAsync(Student student, Course course, string confirmationLink)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_fromAddress, _appPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromAddress),
                Subject = $"Enrollment confirmation for \"{course.Name}\" required",
                Body = $@"
                    <h1>Hello {student.Name}:</h1>
                    <p>Your request to enroll in the course &quot;{course.Name}&quot; in room {course.RoomNumber} starting {course.StartDate.ToString("M/d/yyyy")} with instructor {course.Instructor}.</p>
                    <p>We are pleased to have you in the course so if you could <a href='{confirmationLink}'>confirm your enrollment</a> as soon as possible that would be appreciated!</p>
                    <p>Sincerely,</p>
                    <p>The Course Manager App</p>",
                IsBodyHtml = true
            };

            mailMessage.To.Add(student.Email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }
    }
}