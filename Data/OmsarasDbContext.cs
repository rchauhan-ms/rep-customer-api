using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class OmsarasDbContext : DbContext
{
    public OmsarasDbContext(DbContextOptions<OmsarasDbContext> context)
            : base(context)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
}