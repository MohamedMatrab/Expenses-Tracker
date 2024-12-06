using Expenses.Application.IServices;
using Expenses.Application.Services;
using Expenses.Domain.IRepositories;
using Expenses.Infrastructure.Repositories;

namespace Expenses.Extensions;

public static class ServicesConfiguration
{
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        #region Repositories

        serviceCollection.AddTransient<IBudgetRepository,BudgetRepository>();
        serviceCollection.AddTransient<IExpenseRepository,ExpensesRepository>();
        serviceCollection.AddTransient<IBudgetRepository,BudgetRepository>();
        serviceCollection.AddTransient<ICategoryRepository,CategoryRepository>();

        #endregion

        #region Services

        serviceCollection.AddScoped<IExpensesService,ExpensesService>();
        serviceCollection.AddScoped<ICategoriesService,CategoriesService>();

        #endregion
    }
}