using Microsoft.EntityFrameworkCore;

namespace Chiayin_Yang_Assignment2.Entities
{
    public class EnrollmentContext : DbContext
    {
        public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure base configurations are applied
            base.OnModelCreating(modelBuilder);

            // Configure the enum property to be stored as a string
            modelBuilder.Entity<Student>()
                .Property(s => s.Status)
                .HasConversion<string>(); // Store enum as string

            // Configure the one-to-many relationship between Course and Student
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students) // A Course has many Students
                .WithOne(s => s.Course) // Each Student belongs to one Course
                .HasForeignKey(s => s.CourseId) // Foreign key in Student
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete behavior

            // Seed data
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = 1,
                    Name = "Programming MS Web Tech",
                    Instructor = "Jasveen Kaur Taneja",
                    StartDate = new DateTime(2024, 9, 4),
                    RoomNumber = "1C09"
                },
                new Course
                {
                    CourseId = 2,
                    Name = "Game Prog Data Structures",
                    Instructor = "Rohit Kumar Singh",
                    StartDate = new DateTime(2024, 9, 4),
                    RoomNumber = "3G21"
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    Name = "Chiayin Yang",
                    Email = "Cyang4164@conestogac.on.ca",
                    Status = EnrollmentStatus.ConfirmationMessageNotSent,
                    CourseId = 1
                },
                new Student
                {
                    StudentId = 2,
                    Name = "Chiayin Yang",
                    Email = "Cyang4164@conestogac.on.ca",
                    Status = EnrollmentStatus.ConfirmationMessageNotSent,
                    CourseId = 2
                }
            );
        }
    }
}