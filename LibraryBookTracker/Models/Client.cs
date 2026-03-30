using System;

namespace LibraryBookTracker.Models;

public class Client(string firstName, string lastName, string phoneNumber) : BaseEntity
{

    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string PhoneNumber { get; set; } = phoneNumber;

    private readonly List<Book> books = new();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

}
