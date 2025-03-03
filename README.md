# Conestoga College - ACS & IT - PROG2230 Assignment #2: More Data-driven ASP.NET Core MVC Web Apps
![Screenshot 2025-03-03 002930](https://github.com/user-attachments/assets/e3a72c34-90b5-416c-836d-6e54cd79519b)
![Screenshot 2025-03-03 003004](https://github.com/user-attachments/assets/19b1ef48-31e2-46c2-bb33-0fd1ef466ba6)
![Screenshot 2025-03-03 003334](https://github.com/user-attachments/assets/e331e38f-764a-4e27-af7d-1fe7140e515a)

## Overview
This project is an **ASP.NET Core MVC Web App** designed to help an organization manage their courses, students, and confirmation of enrollment messages. The system allows users to create and manage courses, send enrollment confirmation emails to students, and track student responses. This app leverages key concepts like **EF Core relationships**, **radio buttons in forms**, **cookies**, and **sending emails**.

## Features
- **Course Management**: Users can create and edit courses, including details like course name, instructor, start date, and room number.
- **Student Enrollment**: Users can create students for each course and send enrollment confirmation emails.
- **Email Confirmation**: Students receive an email to confirm their enrollment, with a link that allows them to accept or decline their enrollment.
- **Student Responses**: The system tracks whether students have confirmed, declined, or not yet responded to the enrollment confirmation.
- **Cookie-Based Greeting**: A cookie stores the date the user first visited the app, showing a welcome message on the footer based on the first visit.

## Technology Stack
- **Framework**: ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server with EF Core migrations
- **Frontend**: Razor Views, Bootstrap
- **Emailing**: Gmail for sending confirmation emails
- **Form Handling**: Radio Buttons for student responses

## Data Model
### Entities
- **Course**:
  - `Name`: Required.
  - `Instructor`: Required.
  - `StartDate`: Required.
  - `RoomNumber`: Required, must follow the format `[1-9][A-Z][0-9]{2}`, e.g., `4G15`.
  
- **Student**:
  - `Name`: Required.
  - `Email`: Required, must be a valid email.
  - `Status`: Required, with the following possible values (stored as enums):
    - `ConfirmationMessageNotSent` (default)
    - `ConfirmationMessageSent`
    - `EnrollmentConfirmed`
    - `EnrollmentDeclined`

### Relationships
- **Course** to **Student**: One-to-many (A course can have multiple students, each student belongs to one course).
