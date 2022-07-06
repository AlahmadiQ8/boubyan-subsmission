using BoubyanWallet.Web.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;

namespace BoubyanWallet.Web.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<Customer>
{
    public DbSet<Payment> Payments { get; set; }
    
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Customer>().HasMany<Payment>(c => c.Payments).WithOne(p => p.Payer);
        builder.Entity<Payment>().HasKey(p => p.Id);
    }
}