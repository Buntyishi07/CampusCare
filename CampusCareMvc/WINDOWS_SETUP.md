# CampusCare — Run on Windows

This guide is for running the CampusCare ASP.NET Core MVC source project on a separate Windows laptop.

## What must be installed

1. **.NET 10 SDK (x64)** — download from https://dotnet.microsoft.com/en-us/download/dotnet/10.0. Choose the **SDK** installer for Windows x64, not only the Runtime.
2. **MySQL Server** — download MySQL Installer from https://dev.mysql.com/downloads/installer/. Install MySQL Server 8.0 and, if useful, MySQL Workbench. Remember the password set for the `root` user.
3. **Optional: Visual Studio Code** — https://code.visualstudio.com/ and the Microsoft C# Dev Kit extension. The app can also be run from PowerShell without VS Code.

## 1. Extract the project

Extract `CampusCareMvc-Project.zip` to a folder such as:

`C:\Projects\CampusCareMvc-Project`

The project file should then be at:

`C:\Projects\CampusCareMvc-Project\CampusCareMvc\CampusCareMvc.csproj`

## 2. Create the database

Start the MySQL Server service if it is not already running. Open MySQL Workbench, connect to your local server, open a SQL tab, and run:

```sql
CREATE DATABASE campuscare_db
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
```

The application creates its `Complaints` table and inserts four demo records on first startup.

## 3. Set the database password

Open `CampusCareMvc\appsettings.json` in Notepad or VS Code. In the `DefaultConnection` line, replace `CHANGE_THIS_PASSWORD` with the password you chose for MySQL's `root` user. For example, if your MySQL root password is `MyLocalPassword123`, the line becomes:

```json
"DefaultConnection": "server=localhost;port=3306;database=campuscare_db;user=root;password=MyLocalPassword123;"
```

Use your actual password, not the example. Keep the double quotes and trailing semicolon inside the connection string. Save the file. Do not share a copy containing your real database password publicly.

If the MySQL root account is configured differently, change `user` and `password` to a MySQL account that has privileges on `campuscare_db`.

## 4. Run from PowerShell

Open PowerShell (or the VS Code terminal) and run these commands, adjusting the path if you extracted elsewhere:

```powershell
cd "C:\Projects\CampusCareMvc-Project\CampusCareMvc"
dotnet --version
dotnet restore
dotnet run
```

`dotnet --version` should print a version beginning with `10.`. If Windows says `dotnet` is not recognized after installing the SDK, close and reopen PowerShell.

Wait for the terminal to print the local address. Open the displayed URL in your browser. If an HTTPS development certificate warning appears, run:

```powershell
dotnet dev-certs https --trust
```

Approve the Windows prompt if shown, stop the app with `Ctrl+C`, and run `dotnet run` again.

## Notes

- Each laptop gets its own local MySQL database. The recipient starts with the four seeded demo complaints, not the records from the sender's computer.
- They do not need to install EF Core manually; `dotnet restore` downloads the packages declared in `CampusCareMvc.csproj`.
- An internet connection is needed when restoring NuGet packages and for the Bootstrap/validation assets loaded by the browser from CDNs.
