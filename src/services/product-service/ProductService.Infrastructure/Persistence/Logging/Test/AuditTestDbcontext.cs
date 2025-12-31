using Domain.SeedWork.Tracking;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Infrastructure.Persistence.Logging.Test;

public class AuditTestDbcontext: DbContext
{
    private readonly AuditSaveChangesInterceptor _auditInterceptor;

    public AuditTestDbcontext(DbContextOptions<AuditTestDbcontext> options, AuditSaveChangesInterceptor auditInterceptor = null)
        : base(options)
    {
        _auditInterceptor = auditInterceptor;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (_auditInterceptor != null)
        {
            optionsBuilder.AddInterceptors(_auditInterceptor);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(builder =>
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                   .HasMaxLength(200)
                   .IsRequired();
            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        });

        modelBuilder.Entity<AuditLog>(builder =>
        {
            builder.ToTable("AUDITLOG");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.Table_Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(a => a.Key_Values)
                   .IsUnicode(true)
                   .IsRequired();

            builder.Property(a => a.Old_Values)
                   .IsUnicode(true)
                   .IsRequired(false);

            builder.Property(a => a.New_Values)
                   .IsUnicode(true)
                   .IsRequired();

            builder.Property(a => a.Action)
                   .IsUnicode(true)
                   .HasMaxLength(50);

            builder.Property(a => a.User_Name)
                   .IsUnicode(true)
                   .HasMaxLength(100);

            builder.Property(a => a.Created_At)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();
        });
    }
}
