using Expenses.Domain.Entities;

namespace Expenses.Domain.IRepositories;

public interface IExpenseRepository : ICrudRepository<Expense,Guid>
{
    Task<IEnumerable<Expense>> GetExpensesByUserAndMonthAsync(string userId,int month,int year,CancellationToken token=default);
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(Guid categoryId,CancellationToken token=default);
}