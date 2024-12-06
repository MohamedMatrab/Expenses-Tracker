using Expenses.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Domain.Entities.Shared;

[PrimaryKey(nameof(Id))]
public abstract class Base<TKey>(TKey id)
{
    public TKey Id { get; set; } = id;
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? UpdatedById { get; set; }
    
    public virtual User? CreatedBy { get; set; }
    public virtual User? UpdatedBy { get; set; }
}