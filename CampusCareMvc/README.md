# CampusCare — Campus Complaint & Maintenance Tracker

A student mini-project demonstrating ASP.NET Core MVC, Entity Framework Core with MySQL, CRUD operations, validated form handling, session state, and a responsive interface.

## 1. Project overview

CampusCare gives a college a simple place to record and track maintenance problems such as broken classroom equipment, plumbing issues, cleaning requests, furniture damage, and Wi-Fi issues.

### Features

- **Dashboard:** totals for all reports, submitted tickets, in-progress tickets, and resolved/closed tickets; latest submissions.
- **Create:** students can submit an issue, choose a category and priority, describe the location, and provide contact details.
- **Read:** view a searchable/filterable register and a full detail page for each ticket.
- **Update:** edit a ticket and change its status to Submitted, In progress, Resolved, or Closed.
- **Delete:** confirmation page followed by deletion from MySQL.
- **Validation:** required fields, minimum and maximum lengths, email validation, enum validation, and server-side `ModelState` checks. Client-side validation scripts are included.
- **State management:** ASP.NET Core Session remembers up to five recently viewed tickets for 30 minutes; `TempData` displays one-time success messages after create/edit/delete actions.
- **Responsive UI:** Bootstrap 5 with custom CSS for desktop, tablet, and mobile layouts.
- **Demo data:** the app adds four example tickets on first startup when the database table is empty.

## 2. Tech stack

- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core 10
- MySQL 8.x
- `MySql.EntityFrameworkCore` 10.0.12 (official MySQL provider)
- Bootstrap 5.3 and custom CSS

## 3. Requirements before running

Install:

1. **.NET 10 SDK**
2. **MySQL Server 8.x** (MySQL Workbench is optional)
3. **Visual Studio Code** with the C# Dev Kit extension, or Visual Studio

The NuGet packages restore automatically when you run `dotnet restore`.

## 4. Set up the MySQL database

Start MySQL, then run the following statement in MySQL Workbench or the MySQL command line:

```sql
CREATE DATABASE campuscare_db
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
```

Open `appsettings.json`. Update `DefaultConnection` with your actual MySQL username and password:

```json
"DefaultConnection": "server=localhost;port=3306;database=campuscare_db;user=root;password=YOUR_REAL_PASSWORD;"
```

Replace `YOUR_REAL_PASSWORD`; do not leave the placeholder. If your MySQL account uses a different username, update `user` too.

**Important:** do not commit your real database password to a public GitHub repository. For a safer local setup, you can use .NET User Secrets instead of storing the password in `appsettings.json`.

> This mini-project calls `Database.EnsureCreatedAsync()` at startup to create its tables automatically. Use a **new, empty database** for the first run. `EnsureCreated` is convenient for a classroom demo but it does not upgrade an existing schema; use EF Core migrations for a production application.

## 5. Run the project

In VS Code, open the `CampusCareMvc` folder. Open the integrated terminal in the folder that contains `CampusCareMvc.csproj`, then run:

```bash
dotnet restore
dotnet run
```

The terminal prints the local address, usually something like `https://localhost:7xxx` or `http://localhost:5xxx`. Open that address in your browser. On the first run, CampusCare creates the `Complaints` table and inserts sample tickets.

If HTTPS development certificate warnings appear, run:

```bash
dotnet dev-certs https --trust
```

Restart the application afterwards.

## 6. Folder structure

```text
CampusCareMvc/
├── Controllers/
│   ├── HomeController.cs
│   └── ComplaintsController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Models/
│   ├── Complaint.cs
│   ├── IssueCategory.cs
│   ├── IssuePriority.cs
│   ├── IssueStatus.cs
│   └── EnumDisplayExtensions.cs
├── ViewModels/
│   ├── DashboardViewModel.cs
│   └── ComplaintIndexViewModel.cs
├── Views/
│   ├── Home/
│   ├── Complaints/
│   └── Shared/
├── wwwroot/css/site.css
├── appsettings.json
├── Program.cs
└── CampusCareMvc.csproj
```

## 7. Requirement checklist

| Assignment requirement | Where it is demonstrated |
|---|---|
| MVC architecture | Controllers route requests; `Complaint` and view models hold data; Razor views render UI. |
| EF Core + MySQL connectivity | `ApplicationDbContext`, `UseMySQL(...)`, and the connection string in `appsettings.json`. |
| CRUD operations | `ComplaintsController`: `Create`, `Index`/`Details`, `Edit`, and `Delete`. |
| Form handling | Create and Edit Razor forms post to controller actions with anti-forgery protection. |
| Model validation | Data annotations in `Models/Complaint.cs`, validation spans/summary in views, and `ModelState.IsValid` checks in POST actions. |
| State management | Session stores recently viewed ticket IDs; `TempData` stores post/redirect feedback. |
| Responsive interface | Bootstrap grid/table classes and responsive breakpoints in `wwwroot/css/site.css`. |

## 8. Suggested demo for your evaluator

1. Open the dashboard and explain the summary values.
2. Click **Report an issue**, enter valid details, and submit it.
3. Try submitting an invalid email or a too-short description to show validation.
4. Open **All complaints** and search by a ticket number, title, location, or reporter.
5. Filter by status and category.
6. Open one ticket, edit the details, and change its status to **In progress** or **Resolved**.
7. Return to the register and point out **Recently viewed by you** to demonstrate Session state.
8. Delete a sample ticket after reviewing the confirmation page.
9. Show the MySQL `campuscare_db` database and the `Complaints` table to demonstrate persistence.

## 9. Notes

- The four demo reports use fictional names and example.edu addresses.
- Authentication and role-based permissions are intentionally outside this mini-project's scope; it is a demonstration app, not a production ticketing platform.
- Bootstrap and validation libraries are loaded from CDNs, so the browser needs internet access for those assets. The core MVC and database logic runs locally.
