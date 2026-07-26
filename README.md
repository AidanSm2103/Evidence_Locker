# 🔒 Evidence Locker

**A cold case management system built as a C# console application.**

Evidence Locker simulates the backend of a detective's case file system — open cases, log evidence, track chain of custody, and manage the lifecycle of a case from Open, to Cold, to Reopened, to Closed. Built as a portfolio project to demonstrate clean architecture, SOLID principles, and testable business logic in C#.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/language-C%23-239120)
![Tests](https://img.shields.io/badge/tests-xUnit-25A162)
![License](https://img.shields.io/badge/license-MIT-blue)

---

## 📖 Overview

Evidence Locker is a menu-driven console application for managing cold case investigations. It's intentionally built as a **layered, testable solution** rather than a single `Program.cs` file — the goal was to demonstrate the same architectural discipline you'd bring to a production system, in the context of a small, approachable domain.

## ✨ Features

- **Case management** — open, view, close, mark cold, and reopen cases, with a validated state machine governing every transition
- **Evidence tracking** — log evidence against a case, with full chain-of-custody history per item
- **Search & reporting** — keyword search, case counts by status, and date-range filtering, all LINQ-driven
- **Persistent storage** — cases and evidence persist to local JSON files between runs
- **Full unit test coverage** on business logic, using in-memory fakes — no file I/O required to run the test suite

## 🏗️ Architecture

The solution is split into independently testable layers, with dependencies flowing one direction only:

```
Evidence_Locker.Core        → Domain models, interfaces, enums, custom exceptions (no dependencies)
Evidence_Locker.Data        → Repository implementations (JSON persistence)
Evidence_Locker.Services    → Business logic — case state machine, evidence rules, reporting
Evidence_Locker.UI          → Console menus, input handling, presentation
Evidence_Locker.Tests       → xUnit test suite (services tested against in-memory fakes)
Evidence_Locker             → Composition root — wires everything together in Program.cs
```

**Core → Data / Services → UI → (console entry point)**

This structure follows the **Repository Pattern** combined with a light **service layer**, so business rules never leak into persistence code, and the UI never talks to a repository directly. All cross-layer communication happens through interfaces (`ICaseRepository`, `ICaseService`, etc.), which is what makes the service layer unit-testable without touching disk.

### Case Status State Machine

| From | Can transition to |
|---|---|
| Open | Cold, Closed |
| Cold | Reopened, Closed |
| Reopened | Cold, Closed |
| Closed | Reopened |

Invalid transitions (e.g. reopening a case that's already open) throw a custom `InvalidCaseTransitionException` rather than failing silently.

## 🛠️ Tech Stack

- **C# / .NET 8**
- **System.Text.Json** for persistence
- **xUnit** for unit testing
- Manual dependency injection (composition root pattern — no DI container, kept deliberately visible in `Program.cs`)

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 (or any C# IDE / `dotnet` CLI)

### Run it

```bash
git clone https://github.com/AidanSm2103/Evidence-Locker.git
cd Evidence-Locker
dotnet run --project Evidence_Locker
```

Or open `Evidence_Locker.sln` in Visual Studio, set `Evidence_Locker` as the startup project, and press F5.

### Run the tests

```bash
dotnet test
```

## 🗂️ Project Structure

```
Evidence-Locker/
├── Evidence_Locker/                  # Console entry point
├── Evidence_Locker.Core/             # Models, interfaces, enums, exceptions
├── Evidence_Locker.Data/             # Repository implementations
├── Evidence_Locker.Services/         # Business logic
├── Evidence_Locker.UI/               # Console menus & presentation
├── Evidence_Locker.Tests/            # xUnit test suite
└── README.md
```

## 🔮 Possible Future Improvements

- Swap JSON persistence for a real database (SQLite via EF Core) behind the same `ICaseRepository` interface
- Introduce a proper DI container as the project grows
- Split the shared `EvidenceNotFoundException` into more specific exception types
- Add a dedicated `IEvidenceService` unit test suite for chain-of-custody edge cases
