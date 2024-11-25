# 📱 **Device Management System**

Welcome to the **Device Management System**, an intuitive platform designed to streamline the process of lending, tracking, and managing devices across your organization. Whether for students, employees, or departmental needs, this system ensures effective oversight of valuable equipment while simplifying device borrow operations.

---

## 📜 Table of Contents

1. [Overview](#-overview)
2. [Key Features](#-key-features)
3. [System Architecture](#-system-architecture)
4. [Getting Started](#-getting-started)
5. [Installation Guide](#-installation-guide)
6. [Managing Device](#-managing-device)
7. [Troubleshooting](#-troubleshooting)
8. [Contributing](#-contributing)
9. [License](#-license)

---

## 📘 Overview

The **Device Borrow Management System** is built with **ASP.NET Core** and **MongoDB** to provide a powerful yet easy-to-use interface for lending and tracking devices. This system is ideal for institutions, corporate environments, and schools where multiple devices need to be issued and tracked effectively.

### 🎯 Goal

Our mission is to provide a **comprehensive, user-friendly, and highly scalable** platform that facilitates seamless device lending, minimizes loss, and reduces the administrative burden on the organization.

---

## 🚀 Key Features

- **🔒 Secure User Authentication**: Role-based access for administrators, users, and staff.
- **📋 Device Inventory Management**: Track device details, availability, and borrow status.
- **⏰ Borrow Scheduling**: Set borrow periods, return reminders, and extend borrow durations.
- **📊 Data Visualization**: Dashboards for monitoring borrow activities, device health, and usage statistics.
- **🛠️ Admin Control Panel**: Manage users, configure device categories, and customize system settings.

---

## 🏗️ System Architecture

```plaintext
Device-Borrow-Management/
├── Controllers/
│   └── DeviceController.cs
│   └── SpecificationsController.cs
│   └── ModelController.cs
│   └── DepartmentController.cs
│   └── DeviceTypeController.cs
│   └── EmployeeController.cs
├── Models/
│   └── BorrowedDevice.cs
│   └── BorrowHistory.cs
│   └── ChartViewModel.cs
│   └── Department.cs
│   └── Device.cs
│   └── DeviceLocation.cs
│   └── DeviceType.cs
│   └── Employee.cs
│   └── ErrorViewModel.cs
│   └── MaintenanceLog.cs
│   └── Manufacturer.cs
│   └── ManufacturerContactInfo.cs
│   └── Model.cs
│   └── Specifications.cs
├── Views/
│   └── BorrowHistory/
│   └── Device/
│   └── Model/
│   └── Specifications/
│   └── Manufacturer/
│   └── DeviceType/
│   └── Employee/
├── Services/
│   └── MongoDBService.cs
├── wwwroot/
│   └── css/
│   └── js/
├── appsettings.json
├── Program.cs
├── startup.cs
└── README.md
```

- **Controllers/**: Handles API requests and business logic.
- **Models/**: Represents MongoDB collection schemas, including details about borrowed devices, borrow history, device specifications, and more.
- **Views/**: Razor views for UI components.
- **Services/**: Handles interactions with MongoDB.
- **wwwroot/**: Static assets for frontend.
- **appsettings.json**: Stores database connection configurations.
- **Program.cs** and **Startup.cs**: Configure and initialize the ASP.NET Core app.

---

## 🛠️ Getting Started

To begin using the system, ensure the following prerequisites are installed:

- **ASP.NET Core SDK** (5.0+)
- **MongoDB** (4.0+)
- **.NET Core Runtime**
- **Code Editor** (e.g., Visual Studio, Visual Studio Code)

---

## 📝 Installation Guide

### 1. Clone the Repository

```bash
git clone https://github.com/qoucname2202/project-management-hutech-master.git
cd Device-Borrow-Management
```

### 2. Set Up MongoDB

Follow the [MongoDB Installation Guide](https://docs.mongodb.com/manual/installation/) to install and run MongoDB locally or connect to a remote MongoDB cluster.

### 3. Configure Database Connection

Update `appsettings.json` with your MongoDB connection details:

```json
{
	"ConnectionStrings": {
		"MongoDB": "mongodb://localhost:27017/db_hutech"
	},
	"DatabaseName": "db_hutech"
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

## 📡 Managing Device

1. **Launch the Application**: Open your browser and go to `http://localhost:5000`.
2. **Log In**: Access the system with user credentials to view or manage devices.
3. **Borrow Management**: Check out, renew, or return devices using the intuitive borrow interface.
4. **Admin Features**: Administrators can add new devices, track current, and manage user permissions.

---

## 🐞 Troubleshooting

- **Connection Errors**: Ensure MongoDB is running and verify the connection string in `appsettings.json`.
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
      <td align="center"><a href="https://github.com/tuyennt12"><img src="https://avatars.githubusercontent.com/tuyennt12" width="100px;" alt="Nguyễn Trung Tuyến"/><br/><sub><b>Trung Tuyến</b></sub></a><br/><a href="https://github.com/tuyennt12" title="Document">📝</a><a href="https://github.com/tuyennt12" title="Code">💻</a></td>
      <td align="center"><a href="https://github.com/qoucname2202"><img src="https://avatars.githubusercontent.com/qoucname2202" width="100px;" alt="Dương Quốc Nam"/><br/><sub><b>Quốc Nam</b></sub></a><br/><a href="https://github.com/qoucname2202" title="Document">📝</a><a href="https://github.com/qoucname2202" title="Code">💻</a></td>
    </tr>
  </tbody>
</table>

---

## 📜 License

This project is licensed under the MIT License.
