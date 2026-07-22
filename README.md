# 🎓 Alumni Network - Backend API

[![.NET 10.0](https://img.shields.io/badge/Framework-.NET%2010.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL%20(Neon)-4169E1?logo=postgresql&logoColor=white)](https://neon.tech)
[![Entity Framework Core](https://img.shields.io/badge/ORM-EF%20Core-6B21A8?logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![SignalR](https://img.shields.io/badge/Real--Time-SignalR-orange?logo=signalr&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/signalr)
[![Deployment](https://img.shields.io/badge/Hosted%20On-Render-46E3B7?logo=render&logoColor=white)](https://render.com)

A robust, scalable, and production-ready **RESTful API & Real-time WebSockets Server** for the Alumni Network Application. Built with **ASP.NET Core (.NET 10)**, **Entity Framework Core**, and **PostgreSQL (Neon Cloud DB)**.

---

## 🏗 Architecture & Engineering Highlights

- **Clean & Modular Architecture:** Structured with Controllers, Services, DTOs, and Repositories for maintainability and testability.
- **Real-Time Communication:** Bi-directional messaging and live status via **ASP.NET Core SignalR**.
- **Performance Optimized:** N+1 query optimization, EF Core projection, and connection pooling tuning for low-latency database reads.
- **Secure Authentication:** Stateless **JWT (JSON Web Tokens)** authentication with custom authorization policies.
- **Cloud Media Storage:** Direct media upload handling powered by **Cloudinary SDK**.

---

## 🛠 Tech Stack

- **Framework:** ASP.NET Core Web API (.NET 10.0)
- **Database:** PostgreSQL (Cloud hosted on Neon)
- **ORM:** Entity Framework Core (Code-First Migration)
- **Real-time Engine:** ASP.NET Core SignalR (`/chatHub`)
- **Authentication:** JWT Bearer Authentication
- **Media Management:** Cloudinary DotNet SDK
- **API Documentation:** Swagger / OpenAPI

---

## 🗄 Database Schema & Entities

The system uses PostgreSQL relational model managed via EF Core Code-First migrations:

* **Users:** Credentials, Role, JWT Identity, Online Status & Last Seen.
* **Profiles:** Graduation Year, Department, Job Title, Company, University, Avatar.
* **Posts & Comments:** Social feed posts with image URLs, nested comments, and like/share metrics.
* **FriendRequests:** Connection handling with statuses (`Pending`, `Accepted`, `Rejected`).
* **Conversations & Messages:** One-on-one chat history tracking `LastMessageId` for fast retrieval.

---

## 🔌 API Endpoints Summary

### 🔐 Authentication
- `POST /api/auth/register` - Account creation
- `POST /api/auth/login` - Authenticate & receive JWT token

### 👤 Alumni Profiles
- `GET /api/profile/me` - Fetch authenticated user profile
- `GET /api/profile/{userId}` - View specific alumni details
- `PUT /api/profile/update` - Update profile details & avatar

### 📰 Posts & Feed
- `GET /api/posts` - Fetch paginated social feed
- `POST /api/posts` - Create post (Supports photo attachments)
- `POST /api/posts/{id}/like` - Toggle like on post
- `POST /api/posts/{id}/comments` - Add comment to post

### 💬 Messaging & Real-Time
- `GET /api/conversations` - Fetch chat thread list
- `GET /api/messages/{conversationId}` - Fetch message history
- `WS  /chatHub` - SignalR WebSocket connection endpoint for live messaging

---

## 🚧 Roadmap & Upcoming Features

> *Note: This backend is actively maintained and improved in my off-work hours.*

- [x] JWT Authentication & Protected Endpoints
- [x] CRUD Operations for Posts, Comments & Likes
- [x] SignalR WebSocket integration for Chat
- [ ] 🔐 **Password Reset & Change Endpoint** (In Progress)
- [ ] ↪️ **Post Sharing & Feed Re-sharing API** (In Progress)
- [ ] 🔔 **Background Job Queue (Hangfire/Quartz)** for Email/Push Notifications

---

## 🚀 Local Development Setup

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL Server (Local or Neon DB instance)

### 1. Clone & Configure
```bash
# Clone the repository
git clone [https://github.com/YOUR_GITHUB_USERNAME/alumni_network_backend.git](https://github.com/YOUR_GITHUB_USERNAME/alumni_network_backend.git)
cd alumni_network_backend
