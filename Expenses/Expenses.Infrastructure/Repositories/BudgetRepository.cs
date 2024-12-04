using System.Linq.Expressions;
using Expenses.Domain.Entities;
using Expenses.Domain.IRepositories;
using Expenses.Domain.Shared;
using Expenses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

public class BudgetRepository(AppDbContext dbContext) : IBudgetRepository
{
    public async Task<Result> Create(Budget obj, CancellationToken token = default)
    {
        try
        {
            await dbContext.Budgets.AddAsync(obj, token);
            return await SaveChangesAsync(token:token,response:obj);
        }
        catch (OperationCanceledException e)
        {
            return new Error($"{nameof(BudgetRepository)}.${nameof(Create)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception e)
        {
            return new Error($"{nameof(BudgetRepository)}.${nameof(Create)}.Failure","Error When Trying To Add Budget !");
        }
    }
    
    public async Task<IEnumerable<Budget>> ReadList(Expression<Func<Budget, bool>> filter, CancellationToken token = default)
    {
        return await dbContext.Budgets.Where(filter).AsNoTracking().ToListAsync(token);
    }
    
    public async Task<Budget> GetByIdAsync(Guid key, CancellationToken token = default)
    {
        var budget = await dbContext.Budgets.FirstOrDefaultAsync(e=>e.Id==key,token);
        if (budget is null)
            throw new KeyNotFoundException($"Could Not Find Budget with key {key} !");
        return budget;
    }

    public async Task<Result> Update(Budget obj, Guid key, CancellationToken token = default)
    {
        try
        {
            var budget = await dbContext.Budgets.FirstOrDefaultAsync(e=>e.Id==key,token);
            if(budget is null)
                return new Error($"{nameof(BudgetRepository)}.${nameof(Update)}.KeyNotFound",$"Could Not Find Budget with key {key} !");
            dbContext.Entry(budget).CurrentValues.SetValues(obj);
            return await SaveChangesAsync(token:token,response:budget);
        }
        catch (OperationCanceledException e)
        {
            return new Error($"{nameof(BudgetRepository)}.${nameof(Update)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception e)
        {
            return new Error($"{nameof(BudgetRepository)}.${nameof(Create)}.Failure","Error When Trying To Update Budget !");
        }
    }

    public async Task<Result> Delete(Guid key, CancellationToken token = default)
    {
        Budget? existingEntity = null;
        try
        {
            existingEntity = await dbContext.Budgets.FirstOrDefaultAsync(e => e.Id == key, token);
            if (existingEntity == null)
                return new Error($"{nameof(BudgetRepository)}.${nameof(Delete)}.KeyNotFound",$"Could Not Find Budget with key {key} !");
            dbContext.Budgets.Remove(existingEntity);
            return await SaveChangesAsync(token:token,response:existingEntity);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(BudgetRepository)}.${nameof(Delete)}.Cancelled","Delete operation is cancelled !");
        }
        catch (Exception)
        {
            if (existingEntity!=null)
                dbContext.Entry(existingEntity).State = EntityState.Unchanged;
            return new Error($"{nameof(BudgetRepository)}.${nameof(Delete)}.Failure","Error When Trying To Delete Budget !");
        }
    }

    public async Task<Result> SaveChangesAsync(CancellationToken token = default,object? response=null)
    {
        await dbContext.SaveChangesAsync(token);
        return Result.Success(response);
    }
}