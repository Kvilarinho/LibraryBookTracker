using System;

namespace LibraryBookTracker.Models;

public class Book(string title, string author) : BaseEntity
{

    public string Title { get; } = title;
    public string Author { get; } = author;
    public bool IsAvailable { get; set; } =  true;

}
