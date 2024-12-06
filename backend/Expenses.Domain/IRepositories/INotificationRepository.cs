using Expenses.Domain.Entities;

namespace Expenses.Domain.IRepositories;

public interface INotificationRepository : ICrudRepository<Notification,Guid>
{
    
}