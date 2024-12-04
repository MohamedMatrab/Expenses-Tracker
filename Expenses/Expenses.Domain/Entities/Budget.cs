using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Expenses.Domain.Entities.Auth;
using Expenses.Domain.Entities.Shared;

namespace Expenses.Domain.Entities;

public class Budget() : Base<Guid>(Guid.NewGuid())
{
    [Required] public string UserId { get; set; }
    
    public int Month { get; set; }
    public int Year { get; set; }
    
    [Column(TypeName = "decimal(18, 4)")] public decimal MonthlyLimit { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    public bool HasExceededBudget(decimal totalExpenses)
    {
        return totalExpenses > MonthlyLimit;
    }
}