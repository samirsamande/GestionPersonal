using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<GestionPersonal.Models.Empleado> Empleado { get; set; } = default!;
    public DbSet<GestionPersonal.Models.Departamento> Departamento { get; set; } = default!;
}
