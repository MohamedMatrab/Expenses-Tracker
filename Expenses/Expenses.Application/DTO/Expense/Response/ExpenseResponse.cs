namespace Expenses.Application.DTO.Expense.Response;

public record ExpenseResponse(string CategoryId,DateTime Date,decimal Amount,string Description);