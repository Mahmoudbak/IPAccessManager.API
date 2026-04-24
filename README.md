# IP Access Manager API

A robust RESTful API built with .NET 9 to manage and validate country-based IP access. The project strictly follows **Clean Architecture** principles and implements an **In-Memory Data Store** using Thread-Safe collections.

## 🏗️ Architecture
The solution is divided into loosely coupled layers:
- **Core:** Contains Domain Entities (`BlockedCountry`, `BlockAttemptLog`), Interfaces, and Specifications.
- **Infrastructure:** Contains the In-Memory Repositories implementation (`ConcurrentDictionary` for thread safety).
- **Services:** Contains Business Logic (IP Validation) and Third-Party API integration (`GeoLocationService`).
- **API:** The presentation layer containing Controllers and Hosted Services.

## ✨ Key Features
- **Clean Architecture & Separation of Concerns.**
- **Thread-Safe In-Memory Storage** (No Database required).
- **GeoLocation API Integration** (via IPGeolocation.io / ipapi.co).
- **Temporal Blocking** with an automated background cleanup service.
- **Specification Pattern** implemented for flexible querying and pagination.

## 🚀 How to Run
1. Clone the repository.
2. Open `appsettings.json` in the API project.
3. Replace the placeholder with your actual API key:
   ```json
   "GeoLocationApi": {
     "BaseUrl": "[https://api.ipgeolocation.io/ipgeo](https://api.ipgeolocation.io/ipgeo)",
     "ApiKey": "YOUR_API_KEY_HERE"
   }
