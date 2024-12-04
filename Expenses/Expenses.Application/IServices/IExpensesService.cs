using Expenses.Application.DTO.Expense.Request;
using Expenses.Domain.Shared;

namespace Expenses.Application.IServices;

public interface IExpensesService
{
    Result AddAsync(ExpenseRequest request,CancellationToken token=default);
    Result UpdateAsync(ExpenseRequest request,Guid id,CancellationToken token=default);
    Result DeleteAsync(Guid id,CancellationToken token=default);
    Result ReadList(string? userId = null,int? month=null,int? year=null,Guid? categoryId=null);
}