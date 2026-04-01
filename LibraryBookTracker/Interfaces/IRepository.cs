using System;

namespace LibraryBookTracker.Interfaces;

public interface IRepository
{
    Task LoadFromFileAsync();
    Task SaveToFileAsync();
}
