using Microsoft.EntityFrameworkCore;

public class ContextoAppVenta : DbContext
{
    public DbSet<DetalleVenta> DetallesVenta { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySQL("server=localhost;database=ventasus;user=root;password=xxmlgswegxx22");
    }
}