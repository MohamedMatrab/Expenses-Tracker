using System.ComponentModel.DataAnnotations.Schema;
using Expenses.Domain.Entities.Auth;
using Expenses.Domain.Entities.Shared;

namespace Expenses.Domain.Entities;

public class Expense() : Base<Guid>(Guid.NewGuid())
{
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;

    #region Foreign Entities

    [ForeignKey(nameof(UserId))]
    public virtual User User { get;set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get;set; }
    
    #endregion
}