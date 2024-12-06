namespace Expenses.Application.DTO.Expense.Response;

public record ExpenseResponse(string Id,Guid CategoryId,DateTime Date,decimal Amount,string Description);