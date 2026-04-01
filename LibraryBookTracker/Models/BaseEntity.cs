using System;

namespace LibraryBookTracker.Models;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
