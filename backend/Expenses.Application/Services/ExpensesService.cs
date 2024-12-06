using AutoMapper;
using Expenses.Application.DTO.Expense.Request;
using Expenses.Application.IServices;
using Expenses.Domain.Entities;
using Expenses.Domain.IRepositories;
using Expenses.Domain.Shared;
using System.Linq.Expressions;
using Expenses.Application.DTO.Expense.Response;

namespace Expenses.Application.Services;

public class ExpensesService(IExpenseRepository expenseRepository,IMapper mapper) : IExpensesService
{
    public Task<Result> AddAsync(ExpenseRequest request, CancellationToken token = default)
    {
        return expenseRepository.Create(mapper.Map<Expense>(request), token);
    }

    public Task<Result> UpdateAsync(ExpenseRequest request, Guid id, CancellationToken token = default)
    {
        return expenseRepository.Update(mapper.Map<Expense>(request),id,token);
    }

    public Task<Result> DeleteAsync(Guid id, CancellationToken token = default)
    {
        return expenseRepository.Delete(id,token);
    }

    public async Task<Result> GetExpensesList(string? userId = null, int? month = null, int? year = null, Guid? categoryId = null,
        string sortOrder = "asc", int pageNumber = 0, int pageSize = 10, CancellationToken token = default)
    {
        Expression<Func<Expense, bool>> filter = e=>e.UserId==userId && (categoryId == null || e.CategoryId == categoryId);

        if (month.HasValue && year.HasValue)
        {
            filter = e=> e.UserId == userId && e.Date.Month ==month.Value && e.Date.Year==year.Value && 
                         (categoryId == null || e.CategoryId==categoryId);
        }

        var list = await expenseRepository.ReadList(filter: filter, sortOrder: sortOrder, pageNumber: pageNumber, 
            pageSize: pageSize, token: token);
        return Result.Success(mapper.Map<IEnumerable<ExpenseResponse>>(list));
    }


    public Result GetMonthTotalExpenses(int month, int year, string userId, CancellationToken token = default)
    {
        try
        {
            return Result.Success(expenseRepository.GetMonthTotalExpenses(month,year,userId,token));
        }
        catch (Exception e)
        {
            return new Error("Expenses.TotalAmount.Failure",e.Message);
        }
    }

    public Result IsAmountAvailable(string userId, decimal amount, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}