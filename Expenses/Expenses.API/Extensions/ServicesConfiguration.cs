using Expenses.Domain.IRepositories;
using Expenses.Infrastructure.Repositories;

namespace Expenses.Extensions;

public static class ServicesConfiguration
{
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        #region Repositories

        serviceCollection.AddTransient<IBudgetRepository,BudgetRepository>();

        #endregion
    }
}