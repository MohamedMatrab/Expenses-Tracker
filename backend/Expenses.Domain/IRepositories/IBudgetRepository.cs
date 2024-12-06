using Expenses.Domain.Entities;

namespace Expenses.Domain.IRepositories;

public interface IBudgetRepository : ICrudRepository<Budget,Guid>
{
    
}