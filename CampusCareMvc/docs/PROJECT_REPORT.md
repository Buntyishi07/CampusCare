# Mini-Project Report: CampusCare

## 1. Introduction

CampusCare is a web application for submitting and tracking campus maintenance complaints. In a typical college, faults such as damaged furniture, leaking taps, poor Wi-Fi, and malfunctioning classroom equipment may be reported informally and become difficult to follow up on. CampusCare centralizes these reports so that they can be recorded, searched, updated, and resolved.

## 2. Problem statement

Manual or chat-based reporting does not provide a consistent record of the issue, reporter, location, urgency, or progress. A small, database-backed web application is needed to organize the reporting process and preserve a history of each ticket.

## 3. Objectives

- Build a real-world application with ASP.NET Core MVC.
- Store complaints persistently in MySQL through Entity Framework Core.
- Demonstrate create, read, update, and delete operations.
- Validate user input both on the server and in the browser.
- Demonstrate Session and TempData state management.
- Provide a responsive interface for desktop and mobile users.

## 4. Technologies used

| Technology | Purpose |
|---|---|
| ASP.NET Core MVC (.NET 10) / C# | Application routing, controllers, and server-side logic |
| Razor | Dynamic HTML views |
| Entity Framework Core 10 | Object-relational mapping and database queries |
| MySQL | Persistent complaint storage |
| Bootstrap 5 / CSS | Responsive layout and visual styling |
| Session / TempData | Short-lived user state and one-time feedback messages |

## 5. MVC architecture

**Model:** `Complaint` represents a report. `IssueCategory`, `IssuePriority`, and `IssueStatus` define controlled values. Data annotations define validation rules. View models provide dashboard statistics and search results.

**View:** Razor pages in `Views/Home` and `Views/Complaints` render the dashboard, forms, list, details, edit, and delete confirmation pages.

**Controller:** `HomeController` supplies dashboard data. `ComplaintsController` handles incoming requests, applies search/filter logic, validates form submissions, and calls the EF Core context.

**Data access:** `ApplicationDbContext` configures the `Complaints` table. EF Core translates LINQ queries into MySQL operations. The initial schema and fictional demonstration rows are created on first startup.

## 6. Main database entity

The `Complaints` table contains:

| Field | Purpose |
|---|---|
| `Id` | Primary key |
| `TicketNumber` | Unique human-readable ticket reference |
| `Title` | Short issue summary |
| `Description` | Details of the problem |
| `Category` | Electrical, Plumbing, Cleaning, Furniture, Internet/Wi-Fi, Classroom Equipment, or Other |
| `Priority` | Low, Normal, High, or Urgent |
| `Status` | Submitted, In progress, Resolved, or Closed |
| `Location` | Campus building / room / area |
| `ReporterName` | Name of the person who reported the issue |
| `ReporterEmail` | Contact email, validated as an email address |
| `DateReported` | Time at which the ticket was created |
| `UpdatedAt` | Most recent update timestamp |

`TicketNumber` is unique. Enum values constrain the supported choices in the application, while data annotations enforce field-level validation.

## 7. CRUD and form flow

- **Create:** A POST form is validated using `ModelState.IsValid`. The controller generates a ticket number, sets the initial status and timestamps, then saves the entity.
- **Read:** The register lists database records and supports search by title, ticket number, location, or reporter, plus status/category filters. Details pages show the full ticket.
- **Update:** The edit form supports changes to report fields and status. The controller updates the existing entity and records `UpdatedAt`.
- **Delete:** A confirmation page is shown before the POST action removes the row.

POST forms use anti-forgery validation. ModelState errors are shown next to the relevant inputs, with a summary for model-level errors.

## 8. Validation rules

- Title is required and must contain 5–100 characters.
- Description is required and must contain 15–1,200 characters.
- Location is required and must contain 2–120 characters.
- Reporter name is required and must contain 2–80 characters.
- Reporter email is required and must be a valid email address.
- Category, priority, and status must be valid enum values.

## 9. State management

**Session:** The app records up to five recently viewed complaint IDs in ASP.NET Core Session. The session has a 30-minute idle timeout. This demonstrates per-browser-session state that is not stored as permanent complaint data.

**TempData:** After a successful create, update, or delete operation, TempData carries a one-time confirmation message through the redirect.

## 10. User interface

The interface uses a dark teal navigation and dashboard header, light neutral backgrounds, mint accent colors, responsive Bootstrap grid utilities, horizontally scrollable tables on small screens, readable status/priority labels, and mobile breakpoints in custom CSS.

## 11. Test cases

| Test | Input/action | Expected result |
|---|---|---|
| Valid create | Submit all required fields with valid email | Ticket is saved and detail page opens |
| Required validation | Leave required inputs blank | Form is redisplayed with validation errors |
| Length validation | Enter a title shorter than 5 characters | Submission is rejected |
| Email validation | Enter `not-an-email` | Validation error appears |
| Search | Search for a known ticket number or title | Matching row appears |
| Status filter | Select In progress | Only in-progress reports appear |
| Edit | Update title or status | Saved values appear on detail page and dashboard/list |
| Delete | Confirm deletion | Ticket no longer appears in register |
| Session state | Open multiple ticket detail pages, then return to list | Recently viewed tickets are displayed in order |
| Responsive UI | Resize browser to mobile width | Navigation collapses and content remains usable |

## 12. Limitations and future improvements

This mini-project has no authentication, role-based access control, email notifications, file attachments, or production migration workflow. Future work could introduce student/staff login, staff-only status updates, image uploads, assignment to maintenance staff, notification emails, reports by category, and EF Core migrations.

## 13. Conclusion

CampusCare demonstrates the required ASP.NET Core MVC concepts in a practical campus-maintenance scenario. Its MySQL persistence, CRUD flows, model validation, Session/TempData state management, and responsive Razor UI cover the listed mini-project criteria.
