# 🍽️ RestoCash: Restaurant Daily Financial Management System

[![Framework](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Database](https://img.shields.io/badge/Database-MongoDB-green)](https://www.mongodb.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Microservices-orange)](https://microservices.io/)
[![Deployment](https://img.shields.io/badge/Deployment-Render-lightgrey)](https://render.com/)

**RestoCash** is a professional-grade, microservices-based financial management platform designed to streamline daily restaurant operations. It provides real-time tracking for income/expenses, automated shift-based reporting, and historical data archiving.

---

## 📖 Project Overview

RestoCash was developed to eliminate manual bookkeeping errors in the hospitality industry. By providing a centralized digital ledger, it allows restaurant managers to digitize their "End of Day" processes. The system ensures data integrity, offering a seamless way to monitor financial health from any device, anywhere.

### 🎯 Key Objectives
- Eliminate paper-based tracking and manual calculation errors.
- Provide instant access to **Day and Night shift** financial summaries.
- Securely store sensitive financial data using modern encryption and NoSQL technology.
- Offer a fully responsive dashboard for on-the-go management.

---

## 🏗️ Technical Architecture & Tech Stack

The project follows a modern **Clean Architecture** and **Microservices** pattern, ensuring high scalability and maintainability.

### 🔹 Backend & API
- **.NET 8 Web API:** High-performance core service handling complex business logic.
- **MongoDB (NoSQL):** Flexible data storage for rapid development and high-speed queries.
- **JWT (JSON Web Token):** Secure, stateless authentication and authorization.
- **AutoMapper & FluentValidation:** For clean data mapping and robust input validation.

### 🔹 Frontend & UI
- **ASP.NET Core MVC:** Robust server-side rendering for optimal performance.
- **Matrix Admin Template:** A clean, professional dashboard UI for enhanced user experience.
- **Bootstrap 5 & JavaScript:** Fully responsive design that adapts to all screen sizes.

### 🔹 Cloud & DevOps
- **Docker:** Containerized environment for consistent deployment.
- **Render:** Automated CI/CD pipeline linked directly to the GitHub repository.

---

## ✨ Core Features

| Feature | Description |
| :--- | :--- |
| **Shift Management** | Toggle between Day/Night shifts. Each shift tracks its own independent cash flow. |
| **Financial Analysis** | Instant Profit & Loss (P&L) summaries based on custom date ranges. |
| **Expense Tracking** | Categorized tracking for personnel, inventory, and utility expenses. |
| **Report Archive** | A searchable database of all past reports with detailed breakdown views. |
| **Responsive Design** | Optimized UI that works perfectly on smartphones, tablets, and desktops. |

---

🚀 Getting Started (Local Setup)
Prerequisites
.NET 8 SDK

Visual Studio 2022

MongoDB Atlas Account

Installation Steps
Clone the Repository:

Bash
git clone [https://github.com/YunusKucukDev/RestaurantManagement.git](https://github.com/YunusKucukDev/RestaurantManagement.git)
Configure Database:
Update your appsettings.json in the CatalogMicroservice project:

JSON
"DatabaseSettings": {
  "ConnectionString": "mongodb+srv://<user>:<pass>@cluster.mongodb.net/",
  "DatabaseName": "RestaurantDb"
}
Set API Endpoint:
In the WebUI project, ensure ApiSettings__BaseUrl points to your local API address.

Run the Application:
Set both WebUI and CatalogMicroservice as Startup Projects and press F5.

🌐 Deployment Note
This project is optimized for Render.

The CI/CD pipeline automatically deploys the latest version upon every push to the master branch.

Ensure all environment variables (API URLs and DB strings) are correctly configured in the Render Dashboard.

## 📁 Repository Structure

```text
├── RestaaurantManagement.DtoLayer        # Shared Data Transfer Objects (DTOs)
├── RestaurantManagement.CatalogMicroservice # Backend API Service (MongoDB Integration)
├── RestaurantManagement.WebUI           # Frontend MVC Application (User Interface)
└── RestaurantManagement.sln             # Visual Studio Solution File
