using NghiemHuuHoaiBTH2.Models;
using Microsoft.EntityFrameworkCore;

namespace NghiemHuuHoaiBTH2.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
  }
}