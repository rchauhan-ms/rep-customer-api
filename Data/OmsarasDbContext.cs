using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class OmsarasDbContext : DbContext
{
    public OmsarasDbContext(DbContextOptions<OmsarasDbContext> context)
            : base(context)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Omsaras.db");
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
}