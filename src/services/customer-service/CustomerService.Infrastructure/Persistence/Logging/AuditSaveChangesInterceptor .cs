using Domain.SeedWork.Tracking;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace CustomerService.Infrastructure.Persistence.Logging;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var auditEntries = OnBeforeSaveChanges(context);

        if (auditEntries.Any())
        {
            await context.Set<AuditLog>().AddRangeAsync(auditEntries);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChanges(eventData, result);

        var auditEntries = OnBeforeSaveChanges(context);

        if (auditEntries.Any())
        {
            context.Set<AuditLog>().AddRange(auditEntries);
        }

        return base.SavingChanges(eventData, result);
    }

    private List<AuditLog> OnBeforeSaveChanges(DbContext context)
    {
        var auditEntries = new List<AuditLog>();
        var entries = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted);

        var user = _httpContextAccessor.HttpContext?.User?.FindFirst("username")?.Value ?? "System";

        foreach (var entry in entries)
        {
            var audit = new AuditLog
            {
                Table_Name = entry.Metadata.GetTableName(),
                Action = entry.State.ToString(),
                User_Name = user,
                Created_At = DateTime.UtcNow
            };

            audit.Key_Values = JsonSerializer.Serialize(
                entry.Properties.Where(p => p.Metadata.IsPrimaryKey())
                                .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
            );

            if (entry.State == EntityState.Added)
            {
                audit.New_Values = JsonSerializer.Serialize(
                    entry.Properties.ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                );
            }
            else if (entry.State == EntityState.Modified)
            {
                audit.Old_Values = JsonSerializer.Serialize(
                    entry.Properties.Where(p => p.IsModified)
                                    .ToDictionary(p => p.Metadata.Name, p => p.OriginalValue)
                );
                audit.New_Values = JsonSerializer.Serialize(
                    entry.Properties.Where(p => p.IsModified)
                                    .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                );
            }
            else if (entry.State == EntityState.Deleted)
            {
                audit.Old_Values = JsonSerializer.Serialize(
                    entry.Properties.ToDictionary(p => p.Metadata.Name, p => p.OriginalValue)
                );
                audit.New_Values = "{}";
            }

            auditEntries.Add(audit);
        }

        return auditEntries;
    }
}
