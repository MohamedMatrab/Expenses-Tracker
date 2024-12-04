using Expenses.Domain.Entities;
using Expenses.Domain.Shared;

namespace Expenses.Domain.IRepositories;

public interface IExpenseRepository : ICrudRepository<Expense,Guid>
{
    Result GetExpensesByUserAndMonthAsync(string userId,int month,int year,CancellationToken token=default);
    Result GetExpensesByCategoryAsync(Guid categoryId,CancellationToken token=default);
}