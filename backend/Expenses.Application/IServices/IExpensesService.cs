using Expenses.Application.DTO.Expense.Request;
using Expenses.Domain.Shared;

namespace Expenses.Application.IServices;

public interface IExpensesService
{
    Task<Result> AddAsync(ExpenseRequest request,CancellationToken token=default);
    Task<Result> UpdateAsync(ExpenseRequest request,Guid id,CancellationToken token=default);
    Task<Result> DeleteAsync(Guid id,CancellationToken token=default);
    Task<Result> GetExpensesList(string? userId=null,int? month=null,int? year=null,Guid? categoryId=null,string sortOrder="asc",int pageNumber=0,int pageSize=10,CancellationToken token=default);
    Result GetMonthTotalExpenses(int month,int year,string userId,CancellationToken token=default);
    Result IsAmountAvailable(string userId,decimal amount,CancellationToken token=default);
}