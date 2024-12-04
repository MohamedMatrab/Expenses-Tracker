using Expenses.Domain.Entities;
using Expenses.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User,Role,string>
{
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Notification> Notifications { get; set; }
}