# 🌐 **Project Management System**

Welcome to the **Project Management System** for **HUTECH (Ho Chi Minh City University of Technology)**! This platform is specifically designed to support Master's level Database Systems projects, providing students and educators with an effective tool for managing and exploring complex database projects.

---

## 📜 Table of Contents

1. [Project Overview](#-project-overview)
2. [Features](#-features)
3. [Project Structure](#-project-structure)
4. [Prerequisites](#-prerequisites)
5. [Installation Guide](#-installation-guide)
6. [Connecting to the MongoDB Database](#-connecting-to-the-mongodb-database)
7. [Usage](#-usage)
8. [Troubleshooting](#-troubleshooting)
9. [Contributing](#-contributing)
10. [License](#-license)

---

## 📘 Project Overview

This **ASP.NET Core** and **MongoDB**-powered web application serves as a learning and management tool for students and professors at HUTECH, making database system education more accessible and interactive.

### 🎯 Goal

Our mission is to create a **functional, scalable, and user-friendly** platform that helps users master database management concepts while providing a seamless management experience.

---

## 🚀 Features

- **🔒 User Authentication**: Secure login for students and faculty.
- **📚 Course Management**: Access and organize course materials and assignments.
- **💻 Database Exercises**: Interactive labs for hands-on database experience.
- **🛠️ Admin Interface**: Control panel for managing users, projects, and settings.
- **📊 Data Visualization**: Visual tools for understanding queries and data.

---

## 📂 Project Structure

```plaintext
Project-Management-Hutech-Master/
├── Controllers/
│   └── TaskController.cs
│   └── ProjectController.cs
│   └── EmployeeController.cs
├── Models/
│   └── Task.cs
│   └── Project.cs
│   └── Employee.cs
├── Views/
│   └── Home/
│   └── Task/
│   └── Project/
│   └── Employee/
├── Services/
│   └── MongoDBService.cs
├── wwwroot/
│   └── css/
│   └── js/
├── appsettings.json
├── Program.cs
├── Startup.cs
└── README.md
```

- **Controllers/**: Manages API requests and logic.
- **Models/**: Contains MongoDB collection schemas.
- **Views/**: Razor views for the frontend.
- **Services/**: MongoDB service layer.
- **wwwroot/**: Static assets like CSS and JavaScript.
- **appsettings.json**: Stores configuration for database and other settings.
- **Program.cs** and **Startup.cs**: Configure and initialize the ASP.NET Core app.

---

## 🛠️ Prerequisites

Ensure you have these installed before starting:

- **ASP.NET Core SDK** (5.0+)
- **MongoDB** (4.0+)
- **.NET Core Runtime**
- **Code Editor** (e.g., Visual Studio, Visual Studio Code)

---

## 📝 Installation Guide

### 1. Clone the Repository

```bash
git clone https://github.com/username/Project-Management.git
cd Project-Management
```

### 2. Set Up MongoDB

Follow the [MongoDB Installation Guide](https://docs.mongodb.com/manual/installation/) to install and run MongoDB locally or connect to a remote MongoDB cluster.

### 3. Configure Database Connection

Update `appsettings.json` with your MongoDB connection details:

```json
{
	"ConnectionStrings": {
		"MongoDB": "mongodb://localhost:27017/ProjectManagementDB"
	},
	"DatabaseName": "ProjectManagementDB"
}
```

### 4. Install Dependencies

```bash
dotnet restore
```

### 5. Run the Application

Start the application, which will run at `http://localhost:5000`.

```bash
dotnet run
```

---

## 📡 Connecting to the MongoDB Database

1. **Run MongoDB** on your local or remote server.
2. Confirm the connection string in `appsettings.json`.
3. Use [MongoDB Compass](https://www.mongodb.com/products/compass) for a visual interface to interact with your MongoDB data.

---

## 🔧 Usage

1. **Launch the App**: Open your browser and navigate to `http://localhost:5000`.
2. **Student & Professor Access**: Log in to access course features.
3. **Admin Dashboard**: Log in with an admin account to manage users, projects, and system settings.
4. **Database Labs**: Perform exercises and view real-time MongoDB queries.

---

## 🐞 Troubleshooting

- **Connection Errors**: Ensure MongoDB is running and verify your `appsettings.json` connection string.
- **Dependency Issues**: Run `dotnet restore` to reinstall packages.

---

## 🤝 Contributing

- **[Nguyễn Trung Tuyến](https://github.com/tuyennt12)** - Team Leader & Mentor
  - 📖 **Role:** Guides the team in project selection and management. Provides mentorship in software development best practices.
- **[Dương Quốc Nam](https://github.com/qoucname2202)** - Frontend Developer
  - ⚙️ **Role:** Specializes in Node, Nestjs works on server-side logic, databases, and deploy application.

<table align="center">
  <tbody>
	<tr>
			<td align="center"><a href="https://github.com/tuyennt12"><img src="https://avatars.githubusercontent.com/tuyennt12" width="100px;" alt="Nguyễn Trung Tuyến"/><br/><sub><b>Trung Tuyến</b></sub></a><br/><a href="https://github.com/tuyennt12" title="Document">📝</a><a  href="https://github.com/tuyennt12" title="Code">💻</a></td>
		<td align="center"><a href="https://github.com/qoucname2202"><img src="https://avatars.githubusercontent.com/qoucname2202" width="100px;" alt="Dương Quốc Nam"/><br/><sub><b>Quốc Nam</b></sub></a><br/><a href="https://github.com/qoucname2202" title="Document">📝</a><a href="https://github.com/qoucname2202" title="Code">💻</a></td>
    </tr>
    </tbody>

</table>

---

## 📜 License

This project is licensed under the MIT License.
