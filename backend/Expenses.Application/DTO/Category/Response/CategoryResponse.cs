namespace Expenses.Application.DTO.Category.Response;

public record CategoryResponse(Guid Id,string Name,string Description,decimal MonthTotal);