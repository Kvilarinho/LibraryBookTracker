using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IClientService
{
    Task AddClient(string firstName, string lastName, string phoneNumber);
    Task RemoveClient(Guid id);
    IEnumerable<Client> GetAll();
}
