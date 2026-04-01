# Library Book Tracker

A CLI application to manage a library's books, clients, and loans.

## Features

- Add, list, and remove books
- Add, list, and remove clients
- Loan and return books
- View loan history per client

## Architecture

```

├── CLI                 → Strategy pattern contract
│   ├── Commands
│   │   ├── Books       
│   │   │   ├── AddBookCommand.cs
│   │   │   ├── ListBooksCommand.cs
│   │   │   └── RemoveBookCommand.cs
│   │   ├── Clients
│   │   │   ├── AddClientCommand.cs
│   │   │   ├── ListClientsCommand.cs
│   │   │   └── RemoveClientCommand.cs
│   │   └── Loans
│   │       ├── ListClientLoansCommand.cs
│   │       ├── LoanBookCommand.cs
│   │       └── ReturnBookCommand.cs
│   ├── ICommand.cs
│   └── Menus          
│       └── MainMenu.cs         → Command dispatcher 
│          (dictionary-based)
├── Interfaces
│   ├── IBookRepository.cs
│   ├── IBookService.cs
│   ├── IClientRepository.cs
│   ├── IClientService.cs
│   ├── IFileStorageService.cs
│   ├── ILoanRepository.cs
│   ├── ILoanService.cs
│   └── IRepository.cs
├── LibraryBookTracker.csproj
├── Models
│   ├── BaseEntity.cs
│   ├── Book.cs
│   ├── Client.cs
│   └── Loan.cs
├── Program.cs          → Manual dependency wiring + app entry point 
├── Repositories
│   ├── BookRepository.cs
│   ├── ClientRepository.cs
│   └── LoanRepository.cs
└── Services
    ├── BookService.cs
    ├── ClientService.cs
    ├── FileStorageService.cs
    └── LoanService.cs
```

## Design Decisions & Tradeoffs

### Save to file on every mutation
Data is persisted to JSON after every add, remove, loan, or return operation rather than on a manual save or only on exit.

- **Why:** The app is small and the data set is minimal, so the I/O cost is negligible. The gain is reliability — there is no risk of losing data if the app crashes or the user exits without saving.
- **Tradeoff:** Slightly more disk writes. Acceptable for this scale.

### Loan history is never deleted
When a book is returned, the `Loan` record has its `ReturnDate` set but is kept in the repository. Loans are never removed.

- **Why:** Preserves a full audit trail of all past loans per client. `GetByClient` returns the complete history, and active loans are identified by `ReturnDate == null`.
- **Tradeoff:** The loans file grows over time. For a small library this is not a concern.

### Loan as a separate entity
A dedicated `Loan` entity links `Book` and `Client` by Id instead of embedding loan data directly on the `Book` or `Client` models.

- **Why:** Separation of responsibilities — each entity owns only its own data. It also reduces coupling: `BookService` and `ClientService` don't need to know about each other. Adding new loan-related fields (e.g. due date, fine) only affects the `Loan` model and `LoanService`.
- **Tradeoff:** Requires joining across repositories to display full loan details (e.g. book title alongside a loan record).

## Running

```bash
dotnet run --project LibraryBookTracker
```
