namespace Expenses.Application.DTO.Expense.Request;

public record ExpenseRequest(Guid CategoryId,string Description,decimal Amount,DateTime Date);