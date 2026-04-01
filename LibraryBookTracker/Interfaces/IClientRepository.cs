using System;
using LibraryBookTracker.Models;

namespace LibraryBookTracker.Interfaces;

public interface IClientRepository :IRepository
{
    void Add(Client client);
    void Remove(Guid id);
    IEnumerable<Client> GetAll();
}
