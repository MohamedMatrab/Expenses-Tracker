using System.Linq.Expressions;
using Expenses.Domain.Entities;
using Expenses.Domain.IRepositories;
using Expenses.Domain.Shared;
using Expenses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<Result> Create(Category obj, CancellationToken token = default)
    {
        try
        {
            await dbContext.Categories.AddAsync(obj, token);
            return await SaveChangesAsync(token:token,response:obj);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(CategoryRepository)}.${nameof(Create)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception)
        {
            return new Error($"{nameof(CategoryRepository)}.${nameof(Create)}.Failure","Error When Trying To Add Category !");
        }
    }

    public async Task<IEnumerable<Category>> ReadList(Expression<Func<Category, bool>> filter, string sortOrder = "asc", int pageNumber = 0, int pageSize = 10,
        CancellationToken token = default)
    {
        var query = dbContext.Categories.Where(filter);
        query = sortOrder switch
        {
            "name" => query.OrderBy(e => e.Name),
            "name_desc" => query.OrderByDescending(e => e.Name),
            "expenses_amount" => query.OrderBy(e => e.Expenses.Sum(exp=>exp.Amount)),
            "expenses_amount_desc" => query.OrderByDescending(e => e.Expenses.Sum(exp=>exp.Amount)),
            "desc" => query.OrderDescending(),
            _ => query
        };
        query = query.Skip(pageNumber * pageSize).Take(pageSize);
        return await query.AsNoTracking().ToListAsync(token);
    }

    public async Task<Category> GetByIdAsync(Guid key, CancellationToken token = default)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(e=>e.Id==key,token);
        if (category is null)
            throw new KeyNotFoundException($"Could Not Find Category with key {key} !");
        return category;
    }

    public async Task<Result> Update(Category obj, Guid key, CancellationToken token = default)
    {
        try
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(e=>e.Id==key,token);
            if(category is null)
                return new Error($"{nameof(CategoryRepository)}.${nameof(Update)}.KeyNotFound",$"Could Not Find Category with key {key} !");
            dbContext.Entry(category).CurrentValues.SetValues(obj);
            return await SaveChangesAsync(token:token,response:category);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(CategoryRepository)}.${nameof(Update)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception)
        {
            return new Error($"{nameof(CategoryRepository)}.${nameof(Create)}.Failure","Error When Trying To Update Budget !");
        }
    }

    public async Task<Result> Delete(Guid key, CancellationToken token = default)
    {
        Category? existingEntity = null;
        try
        {
            existingEntity = await dbContext.Categories.FirstOrDefaultAsync(e => e.Id == key, token);
            if (existingEntity == null)
                return new Error($"{nameof(CategoryRepository)}.${nameof(Delete)}.KeyNotFound",$"Could Not Find Category with key {key} !");
            dbContext.Categories.Remove(existingEntity);
            return await SaveChangesAsync(token:token,response:existingEntity);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(CategoryRepository)}.${nameof(Delete)}.Cancelled","Delete operation is cancelled !");
        }
        catch (Exception)
        {
            if (existingEntity is not null)
                dbContext.Entry(existingEntity).State = EntityState.Unchanged;
            return new Error($"{nameof(CategoryRepository)}.${nameof(Delete)}.Failure","Error When Trying To Delete Category !");
        }
    }

    public async Task<Result> SaveChangesAsync(CancellationToken token = default, object? response = null)
    {
        await dbContext.SaveChangesAsync(token);
        return Result.Success(response);
    }
}