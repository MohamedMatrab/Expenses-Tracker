using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Expenses.Domain.Entities.Auth;
using Expenses.Domain.Enums;

namespace Expenses.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    [Required] public string UserId { get; set; } 
    [Required] public string Message { get; set; }
    
    public NotificationType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    
    #region Foreign Entities

    [ForeignKey(nameof(UserId))]
    public virtual User User { get;set; }
    
    #endregion
}