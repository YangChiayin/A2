# Course Enrollment Management System
![Screenshot 2025-03-03 002930](https://github.com/user-attachments/assets/e3a72c34-90b5-416c-836d-6e54cd79519b)
![Screenshot 2025-03-03 003004](https://github.com/user-attachments/assets/19b1ef48-31e2-46c2-bb33-0fd1ef466ba6)
![Screenshot 2025-03-03 003334](https://github.com/user-attachments/assets/e331e38f-764a-4e27-af7d-1fe7140e515a)

## Overview
This **ASP.NET Core MVC Web App** is designed for educational organizations to manage their courses, students, and enrollment confirmation processes. Administrators can create and manage courses, enroll students, send confirmation emails, and track responses. The system provides an intuitive user interface for easy management of courses and student enrollment statuses.

## Features

### Course Management
- **Create Courses**: Add new courses with essential details such as course name, instructor, start date, and room number.
- **Edit Courses**: Modify existing course details as required.
- **View Courses**: Centralized dashboard to display all courses and their details.

### Student Enrollment
- **Add Students**: Enroll students into specific courses.
- **Track Enrollment Status**: Monitor and update the enrollment status of each student.
- **View Student Counts**: Easily view the number of students enrolled in each course.

### Email Confirmation System
- **Send Enrollment Emails**: Email enrollment confirmation to students after they are added to a course.
- **Student Responses**: Students can click on links in the email to confirm or decline their enrollment.
- **Track Responses**: The system tracks student responses (Confirmed, Declined, Pending).

### User Experience
- **First Visit Greeting**: A cookie-based welcome message is displayed to new users, showing the date of their first visit.
- **Responsive Design**: The app adapts to different screen sizes for a smooth experience on both desktop and mobile devices.
- **Intuitive Navigation**: Seamless navigation between course management and student management pages.

## Technology Stack
- **Framework**: ASP.NET Core MVC (.NET 8.0)
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Razor Views with Bootstrap for responsive design
- **Email Service**: Gmail SMTP integration for sending emails

### Dependencies
- **Microsoft.EntityFrameworkCore** (9.0.0-rc.2.24474.1)
- **Microsoft.EntityFrameworkCore.SqlServer** (9.0.0-rc.2.24474.1)
- **Microsoft.EntityFrameworkCore.Tools** (9.0.0-rc.2.24474.1)

## Data Model

### Course Entity
- **CourseId**: Primary key for the course.
- **Name**: Required field for the course name.
- **Instructor**: Required field for the instructor's name.
- **StartDate**: Required field for the course start date and time.
- **RoomNumber**: Required field for the room number (format: `[1-9][A-Z][0-9]{2}`, e.g., `4G15`).
- **Students**: A collection of students enrolled in the course (one-to-many relationship with the `Student` entity).

### Student Entity
- **StudentId**: Primary key for the student.
- **Name**: Required field for the student's name.
- **Email**: Required field for the student's email address (must be a valid email format).
- **Status**: Enum field representing the enrollment status with the following possible values:
  - **ConfirmationMessageNotSent** (default)
  - **ConfirmationMessageSent**
  - **EnrollmentConfirmed**
  - **EnrollmentDeclined**
- **CourseId**: Foreign key linking the student to the corresponding course.
