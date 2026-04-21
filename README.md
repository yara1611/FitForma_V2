# FitForma V2

**FitForma V2** is a backend fitness management system built with **.NET 9.0+**. It is designed to provide a high-performance foundation for personalized nutrition tracking, workout routine management, and biometric optimization.

The system leverages a modern, layered architecture to ensure scalability and seamless integration with relational databases like **PostgreSQL** for now.

---

## Architecture

The project adheres to a strict **Separation of Concerns (SoC)** using the **Controller-Service-Repository** pattern:

* **Controllers**: Manage HTTP requests and expose clean RESTful API endpoints.
* **Services**: Encapsulate core business logic, such as nutrition target calculations and validation rules.
* **Repositories**: Abstract the data access layer, providing an asynchronous interface for database operations to ensure high concurrency.
* **Models**: Define domain entities and the relational schema using Data Annotations for EF Core mapping.

---

## Domain Models & Schema

The database schema is engineered with deep relational integrity to support complex fitness data:

### **Identity & Biometrics**
A `User` holds a **1-to-1** relationship with `PersonalInformation`. This extension stores physical metrics like weight and height, alongside persistent, calculated nutrition targets.

### **Workouts**
A `User` can own multiple `ExerciseRoutine` entities (**1-to-N**). Each routine acts as a container for a list of specific `Exercise` movements, tracking volume (sets/reps) and rest intervals.

### **Nutrition**
A `User` manages multiple `MealPlan` entities (**1-to-N**). Each plan aggregates individual `Meal` records, which store precise macronutrient data including protein, carbs, and fats.

---

## Key Features

* **Personalized Nutrition**: Automatic calculation and storage of daily target calories and macros based on biometric profiles.
* **Workout Management**: Structured routines with granular tracking of sets, reps, and rest periods.
* **Asynchronous Data Access**: All data operations utilize `Task` and `async/await` to handle high-concurrency loads efficiently.
* **Dependency Injection**: Optimized resource management through `Scoped` lifecycles registered in `Program.cs`.
* **Future AI Integration**: Architecture is pre-configured to support an **AI Coach** for automated routine generation and feedback.

---

## Tech Stack

| Component | Technology |
| :--- | :--- |
| **Framework** | .NET 9.0+ |
| **ORM** | Entity Framework Core |
| **Database** | PostgreSQL / MS SQL Server |
| **Architecture** | Service-Repository Pattern |

---

## Getting Started

### **1. Clone the Repository**
```bash
git clone [https://github.com/yara1611/FitForma_V2.git](https://github.com/yara1611/FitForma_V2.git)
```
### **2. Configure Database**
```bash
git clone [https://github.com/yara1611/FitForma_V2.git](https://github.com/yara1611/FitForma_V2.git)
```
### **3. Run Migrations**
```bash
dotnet ef database update
```
### **4. Launch the API**
```bash
dotnet run
``` 
