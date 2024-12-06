using Expenses.Domain.Shared;

namespace Expenses.Application.IServices;

public interface ICategoriesService
{
    Task<Result> GetCategoriesList(string name="",string sortOrder="asc",int pageNumber=0,int pageSize=10,CancellationToken token=default);
}