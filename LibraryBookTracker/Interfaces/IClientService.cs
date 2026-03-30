using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IClientService
{
    void AddClient(string firstName, string lastName, string phoneNumber);
    void RemoveClient(Guid id);
    IEnumerable<Client> GetAll();
    IEnumerable<Book> GetBooksFromClient(Guid clientId);
}
