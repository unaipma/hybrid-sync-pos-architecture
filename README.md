#  Retail POS System (Hybrid Cloud Architecture)

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)

> **Capstone Project:** An enterprise-grade Point of Sale (POS) solution featuring **Offline-First architecture** and hybrid data synchronization.

##  Overview

This project is a comprehensive desktop application designed for retail and hospitality environments where internet connectivity can be intermittent. Unlike traditional web-based POS systems, this solution guarantees **100% uptime** by implementing a local-first database strategy with background cloud synchronization.

Built with **C#** and **WinForms** on the .NET Framework, it demonstrates a robust implementation of standard software engineering patterns, including Layered Architecture (N-Tier) and secure cryptographic practices.

---

##  Technical Architecture & Key Features

###  1. Hybrid Data Synchronization (Offline-First)
The core engineering challenge solved in this project is data consistency in unstable network environments.
* **Local Persistence:** Uses **SQLite** to store transactions locally, ensuring zero latency during sales.
* **Cloud Sync:** Automatically synchronizes data with a remote **PostgreSQL cluster (Neon.tech)** when connectivity is restored.
* **Conflict Resolution:** Implements logic to handle data merging between local and cloud states.

###  2. Security & RBAC
* **Role-Based Access Control:** Distinct privileges for *Administrators* (Inventory, Users, Analytics) and *Staff* (Sales, Daily Closure).
* **Cryptography:** User credentials and sensitive data are protected using **SHA-256 hashing** via the **BouncyCastle** library, ensuring industry-standard security.

###  3. Reporting & Analytics
* Integrated **iText7** engine to generate dynamic PDF invoices and daily sales reports.
* Real-time dashboard for sales tracking and inventory levels.

---

##  Tech Stack

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Language** | **C#** | Core logic and Object-Oriented design. |
| **Framework** | **.NET Framework** | Windows Forms (WinForms) for the UI. |
| **Cloud DB** | **PostgreSQL** | Hosted on **Neon.tech** for centralized data storage. |
| **Local DB** | **SQLite** | Embedded database for offline capability. |
| **Security** | **BouncyCastle** | Cryptographic APIs for password hashing. |
| **Deployment** | **Advanced Installer** | CI/CD pipeline for generating MSI installers. |
| **Reporting** | **iText7** | PDF generation library. |

---

##  Installation & Deployment

This project includes a production-ready installer generated via **Advanced Installer**.

1.  Navigate to the **Releases** section (or the `Instaladores` folder).
2.  Download the latest `.msi` package.
3.  Run the installer as Administrator.
4.  *Note: Initial setup requires an internet connection to fetch the initial product catalog from the cloud database.*

---

##  Project Structure

* `/TPV` - Source code (Forms, Business Logic Layer, Data Access Layer).
* `/Instaladores` - Compiled MSI packages for deployment.
* `/Docs` - Technical documentation and user manuals.

---

##  Author

**Unai Pastor**
*Software Engineer specializing in .NET & Modern Web Architectures.*

---
*Disclaimer: This software was developed as a Capstone Project to demonstrate proficiency in full-stack desktop development and database architecture.*
