using System.Linq.Expressions;
using Expenses.Domain.Entities;
using Expenses.Domain.IRepositories;
using Expenses.Domain.Shared;
using Expenses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Repositories;

public class NotificationRepository(AppDbContext dbContext) : INotificationRepository
{
    public async Task<Result> Create(Notification obj, CancellationToken token = default)
    {
        try
        {
            await dbContext.Notifications.AddAsync(obj, token);
            return await SaveChangesAsync(token:token,response:obj);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(NotificationRepository)}.${nameof(Create)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception)
        {
            return new Error($"{nameof(NotificationRepository)}.${nameof(Create)}.Failure","Error When Trying To Add Notification !");
        }
    }

    public async Task<IEnumerable<Notification>> ReadList(Expression<Func<Notification, bool>> filter, CancellationToken token = default)
    {
        return await dbContext.Notifications.Where(filter).AsNoTracking().ToListAsync(token);
    }

    public async Task<Notification> GetByIdAsync(Guid key, CancellationToken token = default)
    {
        var notification = await dbContext.Notifications.FirstOrDefaultAsync(e=>e.Id==key,token);
        if (notification is null)
            throw new KeyNotFoundException($"Could Not Find Notification with key {key} !");
        return notification;
    }

    public async Task<Result> Update(Notification obj, Guid key, CancellationToken token = default)
    {
        try
        {
            var notification = await dbContext.Notifications.FirstOrDefaultAsync(e=>e.Id==key,token);
            if(notification is null)
                return new Error($"{nameof(NotificationRepository)}.${nameof(Update)}.KeyNotFound",$"Could Not Find Notification with key {key} !");
            dbContext.Entry(notification).CurrentValues.SetValues(obj);
            return await SaveChangesAsync(token:token,response:notification);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(NotificationRepository)}.${nameof(Update)}.Cancelled","Operation is cancelled !");
        }
        catch (Exception)
        {
            return new Error($"{nameof(NotificationRepository)}.${nameof(Create)}.Failure","Error When Trying To Update Notification !");
        }
    }

    public async Task<Result> Delete(Guid key, CancellationToken token = default)
    {
        Notification? existingEntity = null;
        try
        {
            existingEntity = await dbContext.Notifications.FirstOrDefaultAsync(e => e.Id == key, token);
            if (existingEntity == null)
                return new Error($"{nameof(NotificationRepository)}.${nameof(Delete)}.KeyNotFound",$"Could Not Find Notification with key {key} !");
            dbContext.Notifications.Remove(existingEntity);
            return await SaveChangesAsync(token:token,response:existingEntity);
        }
        catch (OperationCanceledException)
        {
            return new Error($"{nameof(NotificationRepository)}.${nameof(Delete)}.Cancelled","Delete operation is cancelled !");
        }
        catch (Exception)
        {
            if (existingEntity!=null)
                dbContext.Entry(existingEntity).State = EntityState.Unchanged;
            return new Error($"{nameof(NotificationRepository)}.${nameof(Delete)}.Failure","Error When Trying To Delete Notification !");
        }
    }

    public async Task<Result> SaveChangesAsync(CancellationToken token = default, object? response = null)
    {
        await dbContext.SaveChangesAsync(token);
        return Result.Success(response);
    }
}