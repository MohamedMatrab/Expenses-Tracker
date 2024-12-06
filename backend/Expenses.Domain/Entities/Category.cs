using System.ComponentModel.DataAnnotations;
using Expenses.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Domain.Entities;

[Index(nameof(Name),IsUnique = true)]
public class Category() : Base<Guid>(Guid.NewGuid())
{
    [Required] [StringLength(100)] public string Name { get; set; }
    public string? Description { get; set; }
    
    public virtual List<Expense> Expenses { get; set; }
}