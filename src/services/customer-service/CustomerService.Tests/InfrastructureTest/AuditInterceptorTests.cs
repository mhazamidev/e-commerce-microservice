using CustomerService.Infrastructure.Persistence.Logging;
using CustomerService.Infrastructure.Persistence.Logging.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Infrastructure.Test;

public class AuditInterceptorTests
{
    private DbContextOptions<AuditTestDbcontext> CreateOptions()
    {
        var contextAccessor = new HttpContextAccessor
        {
            HttpContext = new DefaultHttpContext()
        };
        contextAccessor.HttpContext.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");
        contextAccessor.HttpContext.User = new ClaimsPrincipal(
            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") })
        );

        var interceptor = new AuditSaveChangesInterceptor(contextAccessor);
        return new DbContextOptionsBuilder<AuditTestDbcontext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .AddInterceptors(interceptor)
            .Options;
    }

    [Fact]
    public async Task AuditInterceptor_Should_Log_All_Changes()
    {
        var options = CreateOptions();

        using var context = new AuditTestDbcontext(options);

        // 1️⃣ Add entity
        var product = new Product { Name = "Test Product", Price = 10 };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var auditLogs = await context.AuditLogs.ToListAsync();
        Assert.Single(auditLogs);
        Assert.Equal("Added", auditLogs.First().Action);
        Assert.NotNull(auditLogs.First().New_Values);

        // 2️⃣ Update entity
        product.Price = 20;
        await context.SaveChangesAsync();

        auditLogs = await context.AuditLogs.ToListAsync();
        Assert.Equal(2, auditLogs.Count); // Add + Update
        var updateLog = auditLogs.Last();
        Assert.Equal("Modified", updateLog.Action);
        Assert.NotNull(updateLog.Old_Values);
        Assert.NotNull(updateLog.New_Values);

        // 3️⃣ Delete entity
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        auditLogs = await context.AuditLogs.ToListAsync();
        Assert.Equal(3, auditLogs.Count); // Add + Update + Delete
        var deleteLog = auditLogs.Last();
        Assert.Equal("Deleted", deleteLog.Action);
        Assert.NotNull(deleteLog.Old_Values);
        Assert.Equal("{}", deleteLog.New_Values);
    }


}
