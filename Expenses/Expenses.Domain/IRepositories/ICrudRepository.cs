using System.Linq.Expressions;
using Expenses.Domain.Entities.Shared;
using Expenses.Domain.Shared;

namespace Expenses.Domain.IRepositories;

public interface ICrudRepository<TObject,TKey> where TObject:Base<TKey>
{
    Task<Result> Create(TObject obj,CancellationToken token=default);
    Task<IEnumerable<TObject>> ReadList(Expression<Func<TObject,bool>> filter,CancellationToken token=default);
    Task<TObject> GetByIdAsync(TKey key,CancellationToken token=default);
    Task<Result> Update(TObject obj,TKey key,CancellationToken token=default);
    Task<Result> Delete(TKey key,CancellationToken token=default);
    Task<Result> SaveChangesAsync(CancellationToken token = default, object? response = null);
}