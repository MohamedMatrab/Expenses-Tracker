using Expenses.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoriesService categoriesService) : ControllerBase
{
    [HttpGet("get-categories")]
    public async Task<IActionResult> GetCategories(string? name,string? sortOrder,int? pageNumber,int? pageSize,CancellationToken token=default)
    {
        try
        {
            return Ok(await categoriesService.GetCategoriesList(name??"",sortOrder??"asc",pageNumber??0,pageSize??10,token));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}