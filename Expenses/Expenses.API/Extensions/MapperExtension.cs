using AutoMapper;
using Expenses.Application.DTO.Expense.Request;
using Expenses.Application.DTO.Expense.Response;
using Expenses.Domain.Entities;

namespace Expenses.Extensions;

public static class MapperExtension
{
    public static void RegisterMapperService(this IServiceCollection services)
    {

        #region Mapper
        
        services.AddSingleton<IMapper>(_ => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ExpenseRequest, Expense>();
            cfg.CreateMap<Expense, ExpenseResponse>();
            cfg.CreateMap<IEnumerable<Expense>, IEnumerable<ExpenseResponse>>();
            
        }).CreateMapper());
        
        #endregion
    }
}