using Chiayin_Yang_Assignment2.Entities;

namespace Chiayin_Yang_Assignment2.Services
{
    public interface IEmailService
    {
        Task SendEnrollmentConfirmationAsync(Student student, Course course, string confirmationLink);
    }
}
