using Expenses.Application.DTO.Expense.Request;
using Expenses.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController(IExpensesService expensesService) : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> AddExpense([FromBody] ExpenseRequest request,CancellationToken token=default)
    {
        try
        {
            return Ok(expensesService.AddAsync(request,token));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}