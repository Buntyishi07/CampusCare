using CampusCareMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusCareMvc.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Complaint> Complaints => Set<Complaint>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.Property(c => c.TicketNumber).HasMaxLength(24).IsRequired();
            entity.Property(c => c.Title).HasMaxLength(100).IsRequired();
            entity.Property(c => c.Description).HasMaxLength(1200).IsRequired();
            entity.Property(c => c.Location).HasMaxLength(120).IsRequired();
            entity.Property(c => c.ReporterName).HasMaxLength(80).IsRequired();
            entity.Property(c => c.ReporterEmail).HasMaxLength(120).IsRequired();
        });
    }
}
