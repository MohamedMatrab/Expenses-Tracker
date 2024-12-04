using Expenses.Domain.Entities;

namespace Expenses.Domain.IRepositories;

public interface ICategoryRepository : ICrudRepository<Category,Guid>
{
    
}