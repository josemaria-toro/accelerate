using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zetatech.Accelerate.Domain.Abstractions;

public abstract class BaseEntity : IEntity
{
    [Required]
    [Column("c_tsp_created_at")]
    public DateTime CreatedAt { get; set; }
    [Required]
    [Key]
    [Column("c_uid_id")]
    public Guid Id { get; set; }
    [Required]
    [Column("c_tsp_updated_at")]
    public DateTime UpdatedAt { get; set; }
}