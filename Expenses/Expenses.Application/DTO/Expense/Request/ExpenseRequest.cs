namespace Expenses.Application.DTO.Expense.Request;

public record ExpenseRequest(string CategoryId,string Description,decimal Amount,DateTime Date);