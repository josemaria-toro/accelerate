using System;

namespace Zetatech.Accelerate.Domain;

public interface IEntity
{
    DateTime CreatedAt { get; set; }
    Guid Id { get; set; }
    DateTime UpdatedAt { get; set; }
}