using Expenses.Application.DTO.Category.Response;
using Expenses.Application.IServices;
using Expenses.Domain.IRepositories;
using Expenses.Domain.Shared;

namespace Expenses.Application.Services;

public class CategoriesService(ICategoryRepository categoryRepository) : ICategoriesService
{
    public async Task<Result> GetCategoriesList(string name = "", string sortOrder = "asc", int pageNumber = 0, int pageSize = 10,
        CancellationToken token = default)
    {
        try
        {
            var list = await categoryRepository.ReadList(e => e.Name.Contains(name), sortOrder, pageNumber, pageSize,
                token);
            return Result.Success(list.Select(e =>
                new CategoryResponse(e.Id, e.Name, e.Description ?? "", e.Expenses.Sum(ex => ex.Amount))));
        }
        catch (Exception e)
        {
            return new Error($"Categories.{nameof(GetCategoriesList)}.Failure",e.Message);
        }
    }
}