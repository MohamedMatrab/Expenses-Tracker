using System.Linq.Expressions;
using Expenses.Domain.Entities;
using Expenses.Domain.IRepositories;
using Expenses.Domain.Shared;
using Expenses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

public class ExpensesRepository(AppDbContext dbContext) : IExpenseRepository
{
    public async Task<Result> Create(Expense obj, CancellationToken token = default)
    {
        try
        {
            await dbContext.Expenses.AddAsync(obj, token);
            return await SaveChangesAsync(token:token,response:obj);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(ExpensesRepository)}.${nameof(Create)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception)
        {
            return new Error($"{nameof(ExpensesRepository)}.${nameof(Create)}.Failure","Error When Trying To Add Expenses !");
        }
    }

    public async Task<IEnumerable<Expense>> ReadList(Expression<Func<Expense, bool>> filter, CancellationToken token = default)
    {
        return await dbContext.Expenses.Where(filter).AsNoTracking().ToListAsync(token);
    }

    public async Task<Expense> GetByIdAsync(Guid key, CancellationToken token = default)
    {
        var expense = await dbContext.Expenses.FirstOrDefaultAsync(e=>e.Id==key,token);
        if (expense is null)
            throw new KeyNotFoundException($"Could Not Find Expense with key {key} !");
        return expense;
    }

    public async Task<Result> Update(Expense obj, Guid key, CancellationToken token = default)
    {
        try
        {
            var expense = await dbContext.Expenses.FirstOrDefaultAsync(e=>e.Id==key,token);
            if(expense is null)
                return new Error($"{nameof(ExpensesRepository)}.${nameof(Update)}.KeyNotFound",$"Could Not Find Expense with key {key} !");
            dbContext.Entry(expense).CurrentValues.SetValues(obj);
            return await SaveChangesAsync(token:token,response:expense);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(ExpensesRepository)}.${nameof(Update)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception)
        {
            return new Error($"{nameof(ExpensesRepository)}.${nameof(Create)}.Failure","Error When Trying To Update Expense !");
        }
    }
    
    public async Task<Result> Delete(Guid key, CancellationToken token = default)
    {
        Expense? existingEntity = null;
        try
        {
            existingEntity = await dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == key, token);
            if (existingEntity == null)
                return new Error($"{nameof(ExpensesRepository)}.${nameof(Delete)}.KeyNotFound",$"Could Not Find Expense with key {key} !");
            dbContext.Expenses.Remove(existingEntity);
            return await SaveChangesAsync(token:token,response:existingEntity);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(ExpensesRepository)}.${nameof(Delete)}.Cancelled","Delete operation is cancelled !");
        }
        catch (Exception)
        {
            if (existingEntity is not null)
                dbContext.Entry(existingEntity).State = EntityState.Unchanged;
            return new Error($"{nameof(ExpensesRepository)}.${nameof(Delete)}.Failure","Error When Trying To Delete Expense !");
        }
    }

    public async Task<Result> SaveChangesAsync(CancellationToken token = default, object? response = null)
    {
        await dbContext.SaveChangesAsync(token);
        return Result.Success(response);
    }

    public async Task<IEnumerable<Expense>> GetExpensesByUserAndMonthAsync(string userId, int month, int year, CancellationToken token = default)
    {
        return await ReadList(e => e.Date.Month == month && e.Date.Month == month && e.UserId == userId,token);
    }

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(Guid categoryId, CancellationToken token = default)
    {
        return await ReadList(e => e.CategoryId == categoryId,token);
    }
}